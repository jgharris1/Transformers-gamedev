using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Callbacks;
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

    [Header("LowGravity Movement")]
    public float lowgravSpeed;
    public float jetForce;
    public float jetCooldown;
    bool readyToJet;
    public bool JetPack;
    public bool lowGrav;

    [Header("Leave Gravity")]
    // public bool leaveGravBool;
    public float nogravSpeed;
    public float gravityRotation = 5f;

    public float playerPanSpeed;

    public bool noGrav;




    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode DescendKey = KeyCode.LeftShift;

    public KeyCode PanLeft = KeyCode.Q;
    public KeyCode PanRight = KeyCode.E;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Other")]
    public bool hasKeycard = false;
    public bool speedLimit = true;
    public bool teleporterReached = false;
    public TextMeshProUGUI Keycardtext;

    public Transform orientation;
    public Transform camerapos;
    public Transform capsule;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private static Fpscontroller3 instance;

    public static Fpscontroller3 Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<Fpscontroller3>();
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
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        readyToJet = true;
        hasKeycard = false;
    }
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, -orientation.up, playerHeight * 0.5f + 0.2f, whatIsGround);

        Debug.Log(grounded);
        MyInput();
        if (speedLimit)
        {
            speedControl();
        }
        if(grounded){
            rb.drag = groundDrag;
        }else{
            rb.drag = 0;
        }

        if(hasKeycard == true){
            if(teleporterReached == false)
            {
                Keycardtext.text = "Keycard gathered get to exit teleporter NOOOW!";
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

        orientation.localRotation = transform.rotation;
        //camerapos.localRotation = transform.rotation;
        if(isRotating==false){
            rb.freezeRotation = true;
        }
    }
    
    private void MyInput(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Vector3 forward = orientation.forward;
        // Vector3 right = orientation.right;

        

        // Adjust input directions based on the player's orientation
        



        //Pan
        if (Input.GetKey(PanLeft)&& noGrav)
        {    
            RotateCharacterAndCamera(0, 0, playerPanSpeed * Time.deltaTime);
        }
        if (Input.GetKey(PanRight)&& noGrav)
        {
            RotateCharacterAndCamera(0, 0, -playerPanSpeed * Time.deltaTime);
        }
        //Jump
        if(Input.GetKeyDown(jumpKey) && readyToJump && grounded && !lowGrav && !noGrav){
            readyToJump = false;
            Jump(jumpForce);
            Invoke(nameof(ResetJump), JumpCooldown);
        }
        else if(JetPack){
            if(Input.GetKeyDown(jumpKey) && readyToJet && (lowGrav || noGrav) ){
                readyToJet = false;
                Jet();
                Invoke(nameof(ResetJet), jetCooldown);
            }
            //do we want jets for descending?
            if(Input.GetKeyDown(DescendKey) && readyToJet && (lowGrav || noGrav)){
                readyToJet = false;
                Descend();
                Invoke(nameof(ResetJet), jetCooldown);
            }
        }
        else if(Input.GetKeyDown(jumpKey) && readyToJet && grounded && lowGrav){
            readyToJump = false;
            Jump(jumpForce/ 2f);
            Invoke(nameof(ResetJet), JumpCooldown);

        }
        else if(Input.GetKeyDown(jumpKey) && readyToJet && grounded && noGrav){
            readyToJump = false;
            Jump(jumpForce/ 4f);
            Invoke(nameof(ResetJet), JumpCooldown);

        }
    }

    private void MovePlayer()
    {
        Vector3 forward = orientation.forward;//transform.forward;//PlayerCam.Instance.transform.forward;
        Vector3 right = orientation.right;//transform.right;//PlayerCam.Instance.transform.right;
        //moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        //moveDirection = (orientation.forward * verticalInput + orientation.right * horizontalInput).normalized;
        moveDirection = (forward * verticalInput + right * horizontalInput);//.normalized;

        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else if(!grounded && !lowGrav && !noGrav)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        else if(!grounded && lowGrav && !noGrav)
            rb.AddForce(moveDirection.normalized * lowgravSpeed * 10f, ForceMode.Force);
        else if(!grounded && !lowGrav && noGrav)
            rb.AddForce(moveDirection.normalized * nogravSpeed * 10f, ForceMode.Force);

    }

    private void speedControl(){
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //if we are moving faster than the moveSpeed, limit the speed to moveSpeed when gravity is on
        if(!lowGrav && flatVel.magnitude > moveSpeed){
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
        //if we are moving faster than the lowGravSpeed, limit the speed to lowGravSpeed when gravity is off
        else if(lowGrav && flatVel.magnitude > lowgravSpeed){
            Vector3 limitedVel = flatVel.normalized * lowgravSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
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
    public void rotGround(Vector3 vec,bool gravitytype = true)
    {
        // Tested
        Quaternion orientation2 = Quaternion.FromToRotation(-transform.up, vec) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, orientation2, gravityRotation * Time.deltaTime);
        if(gravitytype == true){
            lowGrav = true;
        }
        
    }

    public void leaveGrav()
    {
        // Tested
        transform.rotation = PlayerCam.Instance.transform.rotation;
        PlayerCam.Instance.transform.localRotation = Quaternion.identity;
        noGrav = true;
    }
    


private bool isRotating = false;


private void RotateCharacterAndCamera(float angleX, float angleY, float angleZ)

{

    if (isRotating)

    {

        return;

    }

    //rb.freezeRotation = false;
    isRotating = true;


    transform.Rotate(0, 0, angleZ);

    

    // PlayerCam.Instance.transform.localRotation = transform.rotation;

    rb.MoveRotation(transform.rotation);


    Invoke(nameof(ResetRotationFlag), 0.2f);

}


private void ResetRotationFlag()

{
    
    isRotating = false;
    //transform.rotation = Quaternion.Euler(0,0, transform.rotation.z);

}
}
