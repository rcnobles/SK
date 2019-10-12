using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Should be parent function of Enter
public class Exit : MonoBehaviour
{
    // the name for the scene
    public string sceneName;
    // insert the value for the scene changing password
    [SerializeField] public string newScenePassword;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerMovement.instance.scenePassword = newScenePassword;
            SceneManager.LoadScene(sceneName);

        }
    }
}
