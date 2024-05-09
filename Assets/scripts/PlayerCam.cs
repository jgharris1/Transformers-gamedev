using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class PlayerCam : MonoBehaviour
{
    public float sensX;// = 4f;
    //public static float sensX = 4f;
    public float sensY;// = 4f;

    public float sensMultiplier = 100f;

    
    public Transform orientation;
    public Transform cameraPosition;

    float xRotation;
    float yRotation;

    private static PlayerCam instance;

    public static PlayerCam Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<PlayerCam>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }


    // Start is called before the first frame update
    /*void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }*/

    
    // Update is called once per frame
    public void setSensX(float value)
    {
        sensX = value;
    }
    public void setSensY(float value)
    {
        sensY = value;
    }
    public string getSensX()
    {
        return sensX.ToString();
    }
    public string getSensY()
    {
        return sensY.ToString();
    }
    public float getFloatSensX()
    {
        return sensX;
    }
    public float getFloatSensY()
    {
        return sensY;
    }
    

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX * sensMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY * sensMultiplier;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.localRotation = Quaternion.Euler(0, yRotation, transform.rotation.z);
        //orientation.rotation = Quaternion.Euler(0, 0, 0);

    // Set camera's world rotation to match the player's orientation
        transform.parent.rotation =  Fpscontroller3.Instance.transform.rotation;
}
    //     transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);//* Fpscontroller3.Instance.transform.rotation;
    //     orientation.rotation = Quaternion.Euler(0, yRotation, 0);//* Fpscontroller3.Instance.transform.rotation;

    //     //cameraPosition.rotation = Quaternion.Euler(cameraPosition.rotation.eulerAngles.x, cameraPosition.rotation.eulerAngles.y, Fpscontroller3.Instance.transform.rotation.eulerAngles.z);


    // }
}
