using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLightColour : MonoBehaviour
{
    public Material HighLight;
    // public GameObject highLightToken;
    Renderer rend;
    bool kms = true;

    void OnTriggerEnter(Collider target)
    {
        rend = GetComponent<Renderer>();
        // print("trigger");
        if(target.tag == "Piece")
        {   
            rend.sharedMaterial = HighLight;
            rend.enabled = true;
            kms = false;
        }
        if(target.tag == "Floor"){
            kms = false;
        }
    }
    void update (){
        rend = GetComponent<Renderer>();
        if(kms){
            Destroy(rend);
        }
    }

}
