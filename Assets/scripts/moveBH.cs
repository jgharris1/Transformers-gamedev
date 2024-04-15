using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBH : MonoBehaviour
{
    public GameObject playerdata;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(-100, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerdata.transform.position + offset;
    }
}
