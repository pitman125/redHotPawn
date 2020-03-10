using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenPiece : MonoBehaviour
{
    public GameObject preFab;
    public string pieceType;
    public string mapCoords;
    private float gridConstant = -3.5f;
    public float gridHeight = 1.25f;

    // Start is called before the first frame update
    void Start()
    {
        if (mapCoords != ""){
            // Break string of coords in single coords
            HashSet<string> coordList = new HashSet<string>(mapCoords.Split(','));
            int index = 0;
            foreach(string coord in coordList){
                
                // Check coord has 2 valid values and convert to Vector3
                if (coord.Length == 2){
                    // Convert string coord into vector3 and then gerate it on the board
                    Generate(convertCoordToArrayValues(coord), index);
                } else{
                    print ("sent incorrect length coords");
                }
                index++;
            }
        } else {
            print("No valid coords to create");
        }
    }

    void Generate(Vector3 vector, int number){
        GameObject piece = Instantiate (preFab, vector, Quaternion.Euler(-90,0,0));
        piece.name = pieceType + " " + number;
    }

    Vector3 convertCoordToArrayValues(string coord){
        char[] brokenCoords = coord.ToCharArray();
        float xCoord = -gridConstant; // If no reuslt is found the vector will have values of 0 for x and z
        float zCoord = -gridConstant;

        // Convert x value to number (should be a char between a-h)
        switch (brokenCoords[0])
            {
            case 'a':
                xCoord = 0;
                break;
            case 'b':
                xCoord = 1;
                break;
            case 'c':
                xCoord = 2;
                break;
            case 'd':
                xCoord = 3;
                break;
            case 'e':
                xCoord = 4;
                break;
            case 'f':
                xCoord = 5;
                break;
            case 'g':
                xCoord = 6;
                break;
            case 'h':
                xCoord = 7;
                break;
            default:
                print("Not a valid char (a-h)");
                break;
        }

        // Convert z value to number (should be a char between 1-8)
        switch (brokenCoords[1])
            {
            case '1':
                zCoord = 0;
                break;
            case '2':
                zCoord = 1;
                break;
            case '3':
                zCoord = 2;
                break;
            case '4':
                zCoord = 3;
                break;
            case '5':
                zCoord = 4;
                break;
            case '6':
                zCoord = 5;
                break;
            case '7':
                zCoord = 6;
                break;
            case '8':
                zCoord = 7;
                break;
            default:
                print("Not a valid char (1-8)");
                break;
        }
        return new Vector3 (xCoord+gridConstant, gridHeight, zCoord+gridConstant);

    }
}
