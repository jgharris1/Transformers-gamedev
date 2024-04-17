using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravrecv : MonoBehaviour
{
    private Rigidbody rigid; 
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void applyGrav(bool sphere, Vector3 vec, float grav)
    {
        if (sphere)
        {
            rigid.velocity += Vector3.Normalize(vec - transform.position) * Time.deltaTime * grav;
            if (tag == "Player")
            {
                GetComponent<fpscontroller2>().rotGround(Vector3.Normalize(vec - transform.position));
            }
        }
        else
        {
            rigid.velocity += Vector3.Normalize(vec) * Time.deltaTime * grav;
            if (tag == "Player")
            {
                GetComponent<fpscontroller2>().rotGround(vec);
            }
        }
    }


    public void leaveGrav()
    {
        if (tag == "Player")
        {
            GetComponent<fpscontroller2>().leaveGrav();
        }
    }
}
