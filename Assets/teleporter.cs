/*using System.Collections;
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
}*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class teleporter : MonoBehaviour
{
	//teleport delayed?
	public bool delayedTeleport;
	//time it takes to teleport, if not instant
	public float teleportTime = 3;
	//only allow specific tag object? if left empty, any object can teleport

	private float curTeleportTime;
	//private bool checking if you entered the trigger
	private bool inside = false;
    public Fpscontroller3 playerController;

	//check to wait for arrived object to leave before enabling teleporation again
	[HideInInspector]
	private Transform subject;
	//add a sound component if you want the teleport playing a sound when teleporting
	public AudioSource teleportSound;
	//add a sound component for the teleport pad, vibrating for example, or music if you want :D
	//also to make it more apparent when the pad is off, stop this component playing with "teleportPadSound.Stop();"
	//PS the distance is set to 10 units, so you only hear it standing close, not from the other side of the map
	public AudioSource teleportPadSound;
	//simple enable/disable function in case you want the teleport not working at some point
	//without disabling the entire script, so receiving objects still works
	public bool teleportPadOn = true;



    void Start ()
	{
		//Set the countdown ready to the time you chose
        inside = false;
		curTeleportTime = teleportTime;
	}


	void Update ()
	{
		//check if theres something/someone inside
		if(inside)
		{
			//if that object hasnt just arrived from another pad, teleport it
			Teleport();
		}
	}


    private void OnTriggerEnter(Collider other)
    {
		Debug.Log("Player entered");
		Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            inside = true;
            Debug.Log("Player entered");
            // Get the fpscontroller2 component from the player
            //fpscontroller2 
            playerController = other.GetComponent<Fpscontroller3>();
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inside = false;
            
            // Get the fpscontroller2 component from the player
            //fpscontroller2 
            playerController = other.GetComponent<Fpscontroller3>();
            
        }
    }
    

    void Teleport()
	{

        if (playerController != null && playerController.hasKeycard)
        {
            SceneManager.LoadScene(2);
        }
		//if you chose to teleport instantly
		if(delayedTeleport) //if its a delayed teleport
		{
			//start the countdown
			curTeleportTime-=1 * Time.deltaTime;
			//if the countdown reaches zero
			if(curTeleportTime <= 0)
			{
				//reset the countdown
				curTeleportTime = teleportTime;
				teleportSound.Play();
                if (playerController != null && playerController.hasKeycard)
                {
                    SceneManager.LoadScene(2);
                }
			}
		}
	}
}


