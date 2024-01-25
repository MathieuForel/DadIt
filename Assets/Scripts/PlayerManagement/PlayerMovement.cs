using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
        private float desiredMoveSpeed;
        private float lastDesiredMoveSpeed;
        [SerializeField] private float moveSpeed;
        public float[] playerSpeeds = new float[6];
        public bool isMoving;

    [Header("GroundCheck")]
        public float groundDrag;
        [SerializeField] private float playerHeight;
        [SerializeField] public LayerMask groundMask;
        public bool isGrounded;
        
    [Header("Movement State")]
        private PlayerBaseState currentState;
        public StateMachine playerSM;
    
    [Header("References")]
        [SerializeField] public Transform orientation;
        public Rigidbody rb;

    [Header("Mojoke")] 
        public bool isMojo;
        public float mojoDuration;

    [Header("Miscs")] 
        public float verticalInput;
        public float horizontalInput;
        public Vector3 moveDirection;

    void Awake()
    {
        playerSM = new StateMachine(this);
        playerSM.Initialize(playerSM.groundedState);
    }
        
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        //Ground check by raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f, groundMask);
        
        PlayerInput();
        playerSM.Update();
        
        playerSpeeds[0] = isMojo ? 12f : 8f;
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
}