// using System.Collections;
// using System.Collections.Generic;
// using System.Runtime.CompilerServices;
// using UnityEngine;

// public class Rotate : MonoBehaviour
// {

//     public float playerPanSpeed;
//     public KeyCode PanLeft = KeyCode.Q;
//     public KeyCode PanRight = KeyCode.E;
//     //Fpscontroller3 fpscontroller3;

//     public Transform playerObject;
//     public Transform cameraHolderObject;
//     // Start is called before the first frame update
//     private static Rotate instance;

//     public static Rotate Instance
//     {
//         get
//         {
//             if (instance == null)
//             {
//                 instance = FindObjectOfType<Rotate>();
//             }

//             return instance;
//         }
//     }

//     private void Awake()
//     {
//         if (instance != null && instance != this)
//         {
//             Destroy(gameObject);
//             return;
//         }
//         instance = this;
//         DontDestroyOnLoad(gameObject);
//         //fpscontroller3 = Fpscontroller3.Instance;

//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (Input.GetKey(PanLeft))
//         {    
//             RotateCharacterAndCamera(0, 0, playerPanSpeed * Time.deltaTime);
//         }
//         if (Input.GetKey(PanRight))
//         {
//             RotateCharacterAndCamera(0, 0, -playerPanSpeed * Time.deltaTime);
//         }
//     }

//     private void RotateCharacterAndCamera(float angleX, float angleY, float angleZ)
//     {
//         Quaternion targetCharacterRotation = Quaternion.Euler(angleX, angleY, angleZ);
//         Quaternion targetCameraRotation = Quaternion.Euler(-angleX, angleY, 0f);
//         // Apply the rotation to the character and camera
//         playerObject.rotation = targetCharacterRotation;
//         cameraHolderObject.rotation = targetCameraRotation;
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float playerPanSpeed = 100f; // Set a reasonable speed here
    public KeyCode PanLeft = KeyCode.Q;
    public KeyCode PanRight = KeyCode.E;
    public Transform playerObject; // Reference to the player object
    public Transform cameraHolderObject; // Reference to the camera holder object



    void Start()
    {
        // fpscontroller3 = playerObject.GetComponent<Fpscontroller3>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(PanLeft))
        {
            Debug.Log("Rotating Left");
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z + playerPanSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(PanRight))
        {
            Debug.Log("Rotating Right");
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z + -playerPanSpeed * Time.deltaTime);
        }
    }

    private bool isRotating = false;


private void RotateCharacterAndCamera(float angleX, float angleY, float angleZ)

{

    if (isRotating)

    {

        return;

    }

    // rb.freezeRotation = false;
    isRotating = true;


    transform.Rotate(angleX, angleY, angleZ);

    // orientation.localRotation = transform.rotation;
    // camerapos.rotation = transform.rotation;

    // PlayerCam.Instance.transform.localRotation = transform.rotation;

    // rb.MoveRotation(transform.rotation);


    Invoke(nameof(ResetRotationFlag), 0.2f);

}


private void ResetRotationFlag()

{
    // rb.freezeRotation = true;
    isRotating = false;

}
}