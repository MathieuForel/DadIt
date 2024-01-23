using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
        private float desiredMoveSpeed;
        private float lastDesiredMoveSpeed;
        [SerializeField] private float moveSpeed;
        public float[] playerSpeeds = new float[6];
        public bool isMoving = false;
    
    [Header("Dash Ability")]
        public float dashSpeed;
        public float dashTimeOut;
        public bool canDash = true;
    
    [Header("Slide Ability")]
        public float slideTimeOut;
        public bool canSlide = true;
        public float slideTime;
        public float maxSlideTime;
        
    [Header("Wall Running Parameters")]
        [Header("Movement")]
            public float wallRunForce;
            public float wallJumpUpForce;
            public float wallJumpSideForce;
            public float wallRunMaxDuration;
            public float wallRunDuration;
            public bool isWallRunning;
            
        [Header("Detection")]
            public float wallCheckDistance;
            public float minJumpHeight;
            public RaycastHit wallHitLeft;
            public RaycastHit wallHitRight;
            public bool wallLeft;
            public bool wallRight;
            public bool canWallRun;

    [Header("Crouch Settings")]
        public float playerCrouchHeight;
    
    [Header("Jump Settings")]
        public float jumpSpeed;
        public float jumpTimeOut;
        public bool canJump = true;

    [Header("Slope Parameters")]
        [SerializeField] private float maxSlopeAngle;
        private RaycastHit slopeHit;
        
    [Header("GroundCheck")]
        public float groundDrag;
        [SerializeField] private float playerHeight;
        [SerializeField] public LayerMask groundMask;
        public bool isGrounded;
    
    [Header("KeyBinds")]
        public KeyCode jumpKey = KeyCode.Space;
        public KeyCode sprintKey = KeyCode.LeftShift;
        public KeyCode crouchKey = KeyCode.C;
        public KeyCode slideKey = KeyCode.LeftControl;
        public KeyCode dashKey = KeyCode.CapsLock;
        
    [Header("Movement State")]
        private PlayerBaseState currentState;
        public StateMachine playerSM;
    
    [Header("References")]
        [SerializeField] public Transform orientation;
        public Rigidbody rb;

    [Header("Miscs")] 
        public float verticalInput;
        public float horizontalInput;
        public Vector3 moveDirection;
        public Vector3 scale;

    void Awake()
    {
        playerSM = new StateMachine(this);
        playerSM.Initialize(playerSM.groundedState);
    }
        
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        scale = transform.localScale;
        maxSlideTime = slideTime;
        wallRunDuration = wallRunMaxDuration;
    }
    
    void Update()
    {
        //Ground check by raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f, groundMask);
        
        PlayerInput();
        playerSM.Update();
    }

    void FixedUpdate()
    {
        //Function which move the player
        MovePlayer();
        playerSM.FixedUpdate();
    }

    //Coroutine to handle the bool value of an ability
    public IEnumerator ResetTrue(System.Action<bool> myBool, float timer)
    {
        myBool (false);
        yield return new WaitForSeconds(timer);
        myBool (true);
    }

    private void PlayerInput()
    {
        //Sets the player inputs
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        isMoving = horizontalInput != 0 || verticalInput != 0;
    }

    private void MovePlayer()
    {
        //Set the moveDirection of the player by his orientation
        moveDirection = (orientation.forward * verticalInput + orientation.right * horizontalInput).normalized;
        moveDirection *= 6f;

        //rb.useGravity = !OnSlope() && !isWallRunning;
    }
    
    //Function that return a bool to tell if player is on a slope
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }
    
    //Function that checks if the player is close enough to a wall to start a wall run
    public bool CheckWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out wallHitRight, wallCheckDistance, groundMask);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out wallHitLeft, wallCheckDistance, groundMask);
        
        return wallRight || wallLeft;
    }
    
    public bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, groundMask);
    }
}