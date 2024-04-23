using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
//using UnityEngine.Rendering;
using TMPro;

public class Fpscontroller3 : MonoBehaviour
{   

    [Header("Gravity Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float JumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("NoGravity Movement")]
    public float nogravSpeed;
    public float jetForce;
    public float jetCooldown;

    bool readyToJet;
    public bool JetPack;
    public bool noGrav;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode DescendKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Other")]
    public bool hasKeycard = false;
    public bool teleporterReached = false;
    public TextMeshProUGUI Keycardtext;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        readyToJet = true;
        hasKeycard = false;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        speedControl();

        if(grounded){
            rb.drag = groundDrag;
        }else{
            rb.drag = 0;
        }

        if(hasKeycard == true){
            if(teleporterReached == false)
            {
                Keycardtext.text = "Keycard Gathered get to teleporter";
            }
            else
            {
                Keycardtext.text = "teleporter unlocked!";
            }
            
        }
        if(hasKeycard == false){
            Keycardtext.text = "Keycard needed";

        }

    }
    void FixedUpdate(){
        MovePlayer();
    }
    
    private void MyInput(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Jump
        if(Input.GetKeyDown(jumpKey) && readyToJump && grounded && !noGrav){
            readyToJump = false;
            Jump(jumpForce);
            Invoke(nameof(ResetJump), JumpCooldown);
        }
        else if(JetPack){
            if(Input.GetKeyDown(jumpKey) && readyToJet && noGrav){
                readyToJet = false;
                Jet();
                Invoke(nameof(ResetJet), jetCooldown);
            }
            //do we want jets for descending?
            if(Input.GetKeyDown(DescendKey) && readyToJet && noGrav){
                readyToJet = false;
                Descend();
                Invoke(nameof(ResetJet), jetCooldown);
            }
        }
        else if(Input.GetKeyDown(jumpKey) && readyToJet && grounded && noGrav){
            readyToJump = false;
            Jump(jumpForce/ 2f);
            Invoke(nameof(ResetJet), JumpCooldown);

        }
    }

    private void MovePlayer(){
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else if(!grounded && !noGrav)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        else if(!grounded && noGrav)
            rb.AddForce(moveDirection.normalized * nogravSpeed * 10f, ForceMode.Force);
    }

    private void speedControl(){
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //if we are moving faster than the moveSpeed, limit the speed to moveSpeed when gravity is on
        if(!noGrav && flatVel.magnitude > moveSpeed){
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
        //if we are moving faster than the nogravSpeed, limit the speed to nogravSpeed when gravity is off
        else if(noGrav && flatVel.magnitude > nogravSpeed){
            Vector3 limitedVel = flatVel.normalized * nogravSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump(float tempJumpForce){
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * tempJumpForce, ForceMode.Impulse);
    }
    private void Jet(){
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jetForce, ForceMode.Impulse);
    }

    private void Descend(){
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(-transform.up * jetForce, ForceMode.Impulse);
    }

    private void ResetJump(){
        readyToJump = true;
    }
    private void ResetJet(){
        readyToJet = true;
    }
}
