using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravitysphere : MonoBehaviour
{
    private fpscontroller2 playerdata;
    public Vector3 vec;
    public bool sphere;
    public float power;
    private gravrecv receiver;
    void OnTriggerStay(Collider collision)
    {
        if (sphere)
        {
            receiver = collision.transform.GetComponent<gravrecv>();
            if (receiver != null)
            {
                receiver.applyGrav(sphere, transform.parent.transform.position, power);
            }
            
        }
        else
        {
            receiver = collision.transform.GetComponent<gravrecv>();
            if (receiver != null)
            {
                receiver.applyGrav(sphere, vec, power);
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        receiver = collision.transform.GetComponent<gravrecv>();
        if (receiver != null)
        {
            receiver.leaveGrav();
        }
    }
}
