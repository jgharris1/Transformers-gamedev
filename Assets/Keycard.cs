using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the fpscontroller2 component from the player
            fpscontroller2 playerController = collision.gameObject.GetComponent<fpscontroller2>();

            // If the playerController is not null, update hasKeycard
            if (playerController != null)
            {
                playerController.hasKeycard = true;
                // You can also disable the keycard object here if needed
                gameObject.SetActive(false);
            }
        }
    }
}
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the fpscontroller2 component from the player
            fpscontroller2 playerController = other.GetComponent<fpscontroller2>();

            // If the playerController is not null, update hasKeycard
            if (playerController != null)
            {
                playerController.hasKeycard = true;
                // You can also disable the keycard object here if needed
                gameObject.SetActive(false);
            }
        }
    }
}*/
