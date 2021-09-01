using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public float movementSpeed = 4f;
    public float smoothMovementTime = 0.1f;
    public float turnSpeed = 4f;
    float angle;
    float smoothInputMagnitude;
    float smoothMoveVelocity;
    float animMoveSpeed;
    Vector3 velocity;

    public float health = 100f;

    private Rigidbody rb;
    public Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 inputDirection =
            new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        float inputMagnitude = inputDirection.magnitude;

        // here smoothdamp is used to smoothen the velocity change gradually in given time specified by smoothmovement time
        smoothInputMagnitude = 
            Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, 
            ref smoothMoveVelocity, smoothMovementTime);

        // atan2 use kiya to get the tan of x direction ki movement and
        // z direction ki movement. Usne ek angle return kar diya x axis ke saath jiska tan is the same as x/z, this is done to get the turning angle
        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;

        // lerp angle use kiya turing rotation angle se leke target angle tak restrict karne ke liye. 
        //Input ke hisaab se player apne aap do values ke beech mei turn karega
        angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * turnSpeed * inputMagnitude);

       
        velocity = transform.forward * movementSpeed * smoothInputMagnitude; // smooth input magnitude used to smoothly increase the velocity
       // animMoveSpeed = Mathf.Clamp(velocity.magnitude, 0, 1);
        anim.SetFloat("moveSpeed", velocity.magnitude);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            anim.SetBool("isShooting", true);
        }
        else anim.SetBool("isShooting", false);

    }
    //fixed update, frame rate independent, 50 calls per second irrespective of the frame rate,

    private void FixedUpdate()
    {

        rb.MoveRotation(Quaternion.Euler(Vector3.up * angle)); // used to rotate the object AROUND THE Y AXIS only, used to turn the player
        rb.MovePosition(rb.position + velocity * Time.deltaTime);
    }
}
