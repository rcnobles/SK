using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Should be child function of Exit
public class Enter : MonoBehaviour
{
    private string enterPassword;

    private void Start()
    {
        // scenePassword is same as enterPassword
        if (PlayerMovement.instance.scenePassword == enterPassword)
        {
            // entrance position
            PlayerMovement.instance.transform.position = transform.position;
            Debug.Log("Room Entered");
        }
        else
        {
            Debug.Log("Could not Enter Room");
        }
    }
}
