using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class teleporter : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the fpscontroller2 component from the player
            fpscontroller2 playerController = collision.gameObject.GetComponent<fpscontroller2>();

            if (playerController != null && playerController.hasKeycard)
            {
                SceneManager.LoadScene(2);
                
            }
        }
    }
}

    /*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class teleporter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the fpscontroller2 component from the player
            fpscontroller2 playerController = other.GetComponent<fpscontroller2>();

            if (playerController != null && playerController.hasKeycard)
            {
                SceneManager.LoadScene("EndScene");
            }
        }
    }
}
*/   

