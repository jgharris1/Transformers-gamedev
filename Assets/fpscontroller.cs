using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class fpscontroller : MonoBehaviour
{
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 75.0f;
    public Vector3 walkvelocity;
    public Vector3 momentum;

    Rigidbody rigid;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

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
            if (Input.GetKey("a"))
            {
                walkvelocity += transform.rotation * Vector3.left;
            }
            if (Input.GetKey("s"))
            {
                walkvelocity += transform.rotation * Vector3.back;
            }
            if (Input.GetKey("d"))
            {
                walkvelocity += transform.rotation * Vector3.right;
            }
            walkvelocity = Vector3.Normalize(walkvelocity);
            if (Input.GetKey("space"))
            {
                walkvelocity.y = .5f;
                isgrounded = false;
            }
            rigid.velocity = walkvelocity * runningSpeed;

        }
        else
        {

            if (Input.GetKey("w"))
            {
                walkvelocity += transform.rotation * Vector3.forward;
            }
            if (Input.GetKey("a"))
            {
                walkvelocity += transform.rotation * Vector3.left;
            }
            if (Input.GetKey("s"))
            {
                walkvelocity += transform.rotation * Vector3.back;
            }
            if (Input.GetKey("d"))
            {
                walkvelocity += transform.rotation * Vector3.right;
            }
            if (Vector3.Project(rigid.velocity, walkvelocity.normalized).magnitude < runningSpeed)
            {
                rigid.velocity += walkvelocity * 5 * Time.deltaTime;
            }
            if (Physics.Raycast(transform.position, Vector3.down, 1.1f))
            {
                isgrounded = true;
            }
            momentum = rigid.velocity;


        }
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }


}