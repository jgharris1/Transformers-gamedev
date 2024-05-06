using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalTrigger : MonoBehaviour
{
    public GameObject door;
    public GameObject terminal;
    public GameObject inserted;
    private AudioSource slam;
    // Start is called before the first frame update
    void Start()
    {
        slam = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider trigger)
    {
        Debug.Log("Trigger Entered");
        Debug.Log("Green = " + KeycardBools.green);
        Debug.Log("Yellow = " + KeycardBools.yellow);
        Debug.Log("Red = " + KeycardBools.red);

        if (KeycardBools.green == true && terminal.tag == "Green" && Input.GetKeyDown(KeyCode.E))
        {
            inserted.SetActive(true);
            Debug.Log("Trigger Activate");
            Destroy(door.gameObject);
            slam.Play();

        }
        else if (KeycardBools.yellow == true && terminal.tag == "Yellow" && Input.GetKeyDown(KeyCode.E))
        {
            inserted.SetActive(true);
            Debug.Log("Trigger Activate");
            Destroy(door.gameObject);
            slam.Play();
        }
        else if (KeycardBools.red == true && terminal.tag == "Red" && Input.GetKeyDown(KeyCode.E))
        {
            inserted.SetActive(true);
            Debug.Log("Trigger Activate");
            Destroy(door.gameObject);
            slam.Play();
        }//end else if
    }//end ontriggerstay

}//end class


