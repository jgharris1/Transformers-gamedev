using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class teleporterSceneTransfer : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the Fpscontroller3 component from the player
            Fpscontroller3 playerController = collision.gameObject.GetComponent<Fpscontroller3>();

            if (playerController != null)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                //testing
               //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                
            }
        }
    }
}

