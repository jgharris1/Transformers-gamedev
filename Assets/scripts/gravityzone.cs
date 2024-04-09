using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravitysphere : MonoBehaviour
{
    private fpscontroller2 playerdata;
    public Vector3 vec;
    public bool sphere;
    public float power;
    void Start()
    {
        playerdata = GameObject.FindGameObjectWithTag("Player").GetComponent<fpscontroller2>();
    }
    void OnTriggerStay(Collider collision)
    {
        if (sphere)
        {
            playerdata.applyGrav(sphere, transform.parent.transform.position, power);
        }
        else
        {
            playerdata.applyGrav(sphere, vec, power);
        }
    }
}