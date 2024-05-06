using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycardterminal : MonoBehaviour
{
    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Raycastinteract();
    }

    void Raycastinteract()
    {
        Vector3 ppos = transform.position;
        Vector3 forwardDir = transform.forward;

        Ray interactionRay = new Ray(ppos, forwardDir);
        RaycastHit interactionRayHit;
        float interactionRayLength = 5.0f;

        Vector3 interactionRayEndpoint = forwardDir * interactionRayLength;
        Debug.DrawLine(ppos, interactionRayEndpoint);

        bool hitFound = Physics.Raycast(interactionRay, out interactionRayHit, interactionRayLength);
        if (hitFound)
        {
            GameObject hitobj = interactionRayHit.transform.gameObject;
                if (hitobj.name == "terminal" && Input.GetKeyDown("e") && KeycardBools.green == true)
            {
                door.SetActive(false);
            }//end if
        }//end if hitfound
    }//end raycastInteract
}
