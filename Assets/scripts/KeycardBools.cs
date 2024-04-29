using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardBools : MonoBehaviour
{
    public static bool green;
    public static bool red;
    public static bool yellow;
    // Start is called before the first frame update
    void Start()
    {
        green = false;
        red = false;
        yellow = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("green = " + green);
        Debug.Log("yellow = " + yellow);
        Debug.Log("red = " + red);

    }
}
