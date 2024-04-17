using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class fpscontroller2 : MonoBehaviour
{
    public float runningSpeed = 3f;
    public float jetSpeed = 3f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 90f;
    public float gravityRotation = 5f;
    public Vector3 walkvelocity;
    public Vector3 momentum;
    public Vector3 Dir;
    public float jumpStrength;
    public bool underGrav;

    public TextMeshProUGUI Keycardtext;

    Rigidbody rigid;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;
    public bool isgrounded = false;
    public bool NoJet = false;
    public bool hasKeycard = false;

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
        if(hasKeycard == true){
            Keycardtext.text = "Keycard Gathered get to teleporter";
        }
        if(hasKeycard == false){
            Keycardtext.text = "Keycard needed";

        }
        /*walkvelocity.Set(0f, 0f, 0f);
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

        if (!isgrounded && !NoJet  && underGrav)
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
        underGrav = false;*/
        // Calculate movement direction based on player input
        Vector3 moveDirection = Vector3.zero;
        if (isgrounded)
        {
            moveDirection = GetInputDirection(true);
            rigid.velocity = Vector3.Project(rigid.velocity, transform.rotation * Vector3.up) + moveDirection * runningSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigid.velocity += transform.rotation * Vector3.up * jumpStrength;
            }
        }
        else if (!NoJet)
        {
            moveDirection = GetInputDirection(false);
            rigid.velocity += moveDirection * Time.deltaTime * jetSpeed;

            if (Input.GetKey(KeyCode.LeftControl))
            {
                rigid.velocity -= Vector3.Normalize(rigid.velocity) / 10;
            }
        }

        // Check if the player is grounded
        isgrounded = Physics.Raycast(transform.position, transform.rotation * Vector3.down, 1.5f);
        if (!isgrounded && !NoJet)
        {
            float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
            float mouseY = -Input.GetAxis("Mouse Y") * lookSpeed;

            // Rotate the player horizontally
            transform.Rotate(Vector3.up * mouseX);

            // Rotate the player camera vertically
            rotationX += mouseY;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        }
        else
        {
            // Rotate the player and camera based on mouse input
            float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
            float mouseY = -Input.GetAxis("Mouse Y") * lookSpeed;

            transform.Rotate(Vector3.up * mouseX);

            rotationX += mouseY;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        }
    }

    Vector3 GetInputDirection(bool grnd)
    {
        Vector3 inputDirection = Vector3.zero;

        if (Input.GetKey("w"))
        {
            inputDirection += transform.forward;
        }
        if (Input.GetKey("a"))
        {
            inputDirection -= transform.right;
        }
        if (Input.GetKey("s"))
        {
            inputDirection -= transform.forward;
        }
        if (Input.GetKey("d"))
        {
            inputDirection += transform.right;
        }
        if (!grnd)
        {

            if (Input.GetKey("space"))
            {
                inputDirection += transform.up;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                inputDirection -= transform.up;
            }
        }
        return Vector3.Normalize(inputDirection);
    }

    public void rotGround(Vector3 vec)
    {
        Quaternion orientation = Quaternion.FromToRotation(-transform.up, vec) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, orientation, gravityRotation * Time.deltaTime);
        underGrav = true;
    }

    public void leaveGrav()
    {
        transform.rotation = playerCamera.transform.rotation;
        playerCamera.transform.localRotation = Quaternion.identity;
    }
}
