using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    public GameObject highLightToken;
    private GameObject movingPiece;
    private float tileHeight = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            DestoryHighlighting();
            if(Physics.Raycast(ray, out hit, 100f)){
                if (hit.transform != null){
                    
                    GameObject hitObject = hit.transform.gameObject;
                    if(hitObject.tag == "HighLight"){
                        // Move object to highlighted postition
                        Move(hitObject);
                    }else if(hitObject.tag == "Piece"){
                        // Find all places object can go
                        PossibleMoves(hitObject.tag, hitObject);

                        movingPiece = hitObject;
                    }
                    
                }
            }
        }

    }

    private void PossibleMoves(string pieceType, GameObject piece){
        HashSet<Vector3> places = GetPieceMoveSet(piece, pieceType);
        foreach (Vector3 place in places)
        {
            Instantiate(highLightToken, place, Quaternion.identity);
        }
    }

    void Move(GameObject token){
        // Move selected object to token location
        movingPiece.transform.position =  new Vector3(token.transform.position.x, movingPiece.transform.position.y,  token.transform.position.z);
    }

    void DestoryHighlighting(){
        // Find all game object with said tag
        GameObject[] gameObjects =  GameObject.FindGameObjectsWithTag ("HighLight");

        // Loop through game objects deleting them one at a time
        foreach(GameObject gameObject in gameObjects){
            Destroy(gameObject);
        }
    }

    HashSet<Vector3> GetPieceMoveSet(GameObject piece, string pieceType){
        HashSet<Vector3> places = new HashSet<Vector3>();
        // Looking at the name and taking the first char (as they is unique for chess pieces)
        switch(piece.name.Split(' ')[0]){
            case "pawn":
                places.Add(new Vector3(GetXPos(piece) -1f, tileHeight, GetZPos(piece) +1f));
                places.Add(new Vector3(GetXPos(piece) +1f, tileHeight, GetZPos(piece) +1f));
                places.Add(new Vector3(GetXPos(piece), tileHeight, GetZPos(piece) +1f));
            break;
            case "knight":
                places.Add(new Vector3(GetXPos(piece) +2f, tileHeight, GetZPos(piece) +1f));
                places.Add(new Vector3(GetXPos(piece) +2f, tileHeight, GetZPos(piece) -1f));
                places.Add(new Vector3(GetXPos(piece) -2f, tileHeight, GetZPos(piece) +1f));
                places.Add(new Vector3(GetXPos(piece) -2f, tileHeight, GetZPos(piece) -1f));
                places.Add(new Vector3(GetXPos(piece) +1f, tileHeight, GetZPos(piece) +2f));
                places.Add(new Vector3(GetXPos(piece) +1f, tileHeight, GetZPos(piece) -2f));
                places.Add(new Vector3(GetXPos(piece) -1f, tileHeight, GetZPos(piece) +2f));
                places.Add(new Vector3(GetXPos(piece) -1f, tileHeight, GetZPos(piece) -2f));
            break;            
        }
        return places;
    }

    float GetXPos(GameObject go){
        return go.transform.position.x;
    }
    float GetZPos(GameObject go){
        return go.transform.position.z;
    }
}
