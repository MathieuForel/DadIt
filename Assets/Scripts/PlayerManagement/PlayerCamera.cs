using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform cameraPos;
    [SerializeField] public float playerSens;
    public float maxSens;

    private float xRotation, yRotation;

    void Start()
    {
        //Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //Set Framerate
        Application.targetFrameRate = 144;

        maxSens = playerSens;
    }

    // Update is called once per frame
    void Update()
    {
        //Gets mouse movement inputs
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * playerSens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * playerSens;
        
        //Define rotation
        yRotation += mouseX;
        xRotation -= mouseY;
        
        //Clamp max vertical movement 
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        //Rotate camera & player orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
        
        //Move camera to player's camera position
        transform.position = cameraPos.position;
    }
}
