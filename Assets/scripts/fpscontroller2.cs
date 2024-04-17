using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class fpscontroller2 : MonoBehaviour
{
    public float runningSpeed = 3f;
    public float jetSpeed = 3f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 90f;
    public float gravityRotation = 0f;
    public Vector3 walkvelocity;
    public Vector3 momentum;
    public Vector3 Dir;
    public float jumpStrength;

    Rigidbody rigid;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;
    public bool isgrounded = true;
    public bool NoJet = false;

    void Start()
    {
        Dir = new Vector3(0f, 0f, 0f);
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
            walkvelocity = Vector3.Normalize(walkvelocity);
            rigid.velocity = Vector3.Project(rigid.velocity, transform.rotation * Vector3.up) + walkvelocity * runningSpeed;
            if (Input.GetKeyDown("space"))
            {
                rigid.velocity += transform.rotation * Vector3.up * jumpStrength;
            }
            momentum = rigid.velocity;
        }
        else if (!NoJet)
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
            rigid.velocity += walkvelocity * Time.deltaTime * jetSpeed;
            if (Input.GetKey(KeyCode.LeftControl))
            {
                rigid.velocity -= Vector3.Normalize(rigid.velocity) / 10;
            }
            momentum = rigid.velocity;
        }
        
        if (Physics.Raycast(transform.position, transform.rotation * Vector3.down, 1.5f))
        {
            isgrounded = true;
        }
        else
        {
            isgrounded = false;
        }

        if (!isgrounded && !NoJet)
        {
            //rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            //playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(-Input.GetAxis("Mouse Y") * lookSpeed, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        else
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    public void applyGrav(bool sphere, Vector3 vec, float grav)
    {
        if (sphere)
        {
            rigid.velocity += Vector3.Normalize(vec - transform.position) * Time.deltaTime * grav;
            rotGround(Vector3.Normalize(vec - transform.position));
        }
        else
        {
            rigid.velocity += Vector3.Normalize(vec) * Time.deltaTime * grav;
            rotGround(vec);
        }
    }

    private void rotGround(Vector3 vec)
    {
        Quaternion orientation = Quaternion.FromToRotation(-transform.up, vec) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, orientation, gravityRotation * Time.deltaTime);
    }

    public void leaveGrav()
    {
        transform.rotation = playerCamera.transform.rotation;
        playerCamera.transform.localRotation = Quaternion.identity;
    }
}
