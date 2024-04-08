using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class fpscontroller2 : MonoBehaviour
{
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 75.0f;//boo
    public Vector3 walkvelocity;
    public Vector3 momentum;

    Rigidbody rigid;
    Vector3 moveDirection = Vector3.zero;
    //float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;
    public bool isgrounded = false;

    void Start()
    {
        walkvelocity = new Vector3(0f, 0f, 0f);
        momentum = new Vector3(0f, 0f, 0f);
        rigid = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        walkvelocity.Set(0f, 0f, 0f);
        if (isgrounded)
        {
            if (Input.GetKey("w"))
            {
                walkvelocity += transform.rotation * Vector3.forward;
            }
            walkvelocity = Vector3.Normalize(walkvelocity);

        }
        else
        {
            if (Input.GetKey("q"))
            {
                transform.Rotate(Vector3.forward * 100 * Time.deltaTime);
            }
            if (Input.GetKey("e"))
            {
                transform.Rotate(Vector3.forward * -100 * Time.deltaTime);
            }
            if (Input.GetKey("w"))
            {
                walkvelocity += transform.rotation * Vector3.forward;
            }
            if (Input.GetKey("a"))
            {
                walkvelocity += transform.rotation * -Vector3.right;
            }
            if (Input.GetKey("s"))
            {
                walkvelocity += transform.rotation * -Vector3.forward;
            }
            if (Input.GetKey("d"))
            {
                walkvelocity += transform.rotation * Vector3.right;
            }
            if (Input.GetKey("space"))
            {
                walkvelocity += transform.rotation * Vector3.up;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                walkvelocity += transform.rotation * -Vector3.up;
            }
            walkvelocity = Vector3.Normalize(walkvelocity);
            rigid.velocity += walkvelocity * Time.deltaTime * 10;
        }

        if (canMove)
        {
            //rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            //playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(-Input.GetAxis("Mouse Y") * lookSpeed, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    public void applyGrav(bool sphere, Vector3 vec)
    {
        if (sphere)
        {
            rigid.velocity += Vector3.Normalize(vec - transform.position) * Time.deltaTime * 9.81f;
        }
    }


}
