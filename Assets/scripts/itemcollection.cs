using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemcollection : MonoBehaviour
{
    public GameObject keycard;
    public GameObject hudcard;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.tag == "Player")
        {
            //Debug.Log("Trigger Entered");
            if (keycard.tag == "Green")
            {
                KeycardBools.green = true;
                hudcard.SetActive(true);
                Debug.Log("You got green card");
                Destroy(keycard.gameObject);
            }
            else if (keycard.tag == "Yellow")
            {
                KeycardBools.yellow = true;
                hudcard.SetActive(true);
                Debug.Log("You got yellow card");
                Destroy(keycard.gameObject);

            }
            else if (keycard.tag == "Red")
            {

                KeycardBools.red = true;
                hudcard.SetActive(true);
                Debug.Log("You got red card");
                Destroy(keycard.gameObject);
            }
        }//end on trigger enter
    }
}
