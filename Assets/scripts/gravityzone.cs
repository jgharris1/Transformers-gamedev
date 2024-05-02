using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravitysphere : MonoBehaviour
{
    private Fpscontroller3 playerdata;

    [Header("Gravity Settings")]
    public bool alternateGravity;
    public float alternateTime;
    public Vector3 vec1;
    public Vector3 vec2;//leave blank if you don't want to alternate 
    public bool sphere;

    public bool alternatePower;
    public float power1;
    public float power2;
    private gravrecv receiver;
    private gravrecv itemreceiver;
    private bool isalternateVec;
    private bool isalternatePower;
    private float timer = 0f;

    void Update()
    {
        if (alternateGravity && vec1 != Vector3.zero && vec2 != Vector3.zero && alternateTime > 0)
        {
            timer += Time.deltaTime;
            if (timer >= alternateTime)
            {
                timer = 0;
                isalternateVec = !isalternateVec;//vec1 to vec2
            }
        }
        if (alternatePower && power1 != 0 && power2 != 0 && alternateTime > 0)
        {
            timer += Time.deltaTime;
            if (timer >= alternateTime)
            {
                timer = 0;
                isalternatePower = !isalternatePower;//power1 to power2
            }
        }
        
    }

    void OnTriggerStay(Collider collision)
    {
        if (sphere)
        {
            receiver = collision.transform.GetComponent<gravrecv>();
            if (receiver != null)
            {
                receiver.applyGrav(sphere, transform.parent.transform.position, power1);
            }
            
        }
        else
        {
            receiver = collision.transform.GetComponent<gravrecv>();
            if (receiver != null)
            {
                if(alternateGravity && isalternateVec)
                {
                    receiver.applyGrav(sphere, vec2, power1);
                }
                else
                {
                    receiver.applyGrav(sphere, vec1, power1);
                }
                if(alternatePower && isalternatePower)
                {
                    receiver.applyGrav(sphere, vec1, power2);
                }
                else
                {
                    receiver.applyGrav(sphere, vec1, power1);
                }
                //receiver.applyGrav(sphere, vec1, power);
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
