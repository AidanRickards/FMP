using UnityEngine;
using System.Collections;

public class CarControl : MonoBehaviour
{
    public Rigidbody rb;

    public bool grounded = false;
    public float groundCheckDistance;
    private float bufferCheckDistance = 0.1f;
    public float jumpForce;
    public float boostForce;
    public Vector3 velocity;
    public float flipForce;

    public float boostCount = 33;
    private int boostCap = 100;
    private int boostFloor = 0;

    public ParticleSystem pSystem1;
    public ParticleSystem pSystem2;

    public bool doubleJump = false;

    Vector3 carAngleVelocityA;
    Vector3 carAngleVelocityB;
    Vector3 carAngleVelocityC;
    Vector3 carAngleVelocityD;
    Vector3 carAngleVelocityE;
    Vector3 carAngleVelocityF;

    public float rotationSpeed = 360f;

    public JumpTimer jt;
    private void Start()
    {
        carAngleVelocityA = new Vector3(0, 50, 0);
        carAngleVelocityB = new Vector3(0, 75, 0);
    }
    private void Update()
    {
        if (boostCount > boostCap)
            boostCount = boostCap;

        velocity = rb.linearVelocity;

        /*if (rb.linearVelocity.x > 30.1f)
        {
            rb.linearVelocity = new Vector3(30, rb.linearVelocity.y, rb.linearVelocity.z);
        }
        if (rb.linearVelocity.x < -30.1f)
        {
            rb.linearVelocity = new Vector3(-30, rb.linearVelocity.y, rb.linearVelocity.z);
        }*/
        
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance))
        {
                Debug.DrawRay(hit.point, hit.normal, Color.yellow);
                grounded = true;
        }
        else
        {
                grounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddRelativeForce(Vector3.up * jumpForce);
        }

        if (Input.GetKey(KeyCode.LeftShift) && boostCount > 0)
        {
            rb.AddRelativeForce(Vector3.forward * boostForce);
            pSystem1.Emit(1);
            pSystem2.Emit(1);
            boostCount = boostCount - 0.1f;
        }

        NotGrounded();
        Grounded();

    }

    private void FixedUpdate()
    {
        if (!grounded) ;
            
    }

    public void NotGrounded()
    {
        if (!grounded)
        {
            //Double Jump
            if (Input.GetKeyDown(KeyCode.Space) && doubleJump && Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") ==0)
            {
                rb.AddRelativeForce(Vector3.up * jumpForce);
                doubleJump = false;
            }

            //ariel control turning horizontal
            if (Input.GetAxis("Horizontal") != 0 && Input.GetKey(KeyCode.Space) == false)
            {
                carAngleVelocityA = new Vector3(0, 50 * Input.GetAxis("Horizontal"), 0);
                Quaternion deltaRotation = Quaternion.Euler(carAngleVelocityA * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotation);
            }

            //ariel control turning horizontal
            if (Input.GetAxis("Vertical") != 0 && Input.GetKey(KeyCode.Space) == false)
            {
                carAngleVelocityB = new Vector3(75 * Input.GetAxis("Vertical"), 0, 0);
                Quaternion deltaRotationB = Quaternion.Euler(carAngleVelocityB * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotationB);
            }

            //air roll left
            if (Input.GetKey(KeyCode.J))
            {
                carAngleVelocityC = new Vector3(0, 0, 140);
                Quaternion deltaRotationC = Quaternion.Euler(carAngleVelocityC * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotationC);
            }

            //air roll right
            if (Input.GetKey(KeyCode.L))
            {
                carAngleVelocityD = new Vector3(0, 0, -140);
                Quaternion deltaRotationD = Quaternion.Euler(carAngleVelocityD * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotationD);
            }

            //flip
            CheckForFlip();
        }
    }

    public void Grounded()
    {
        if (grounded)
        {
            doubleJump = true;

            StopAllCoroutines();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "MiniBoost")
        {
            if(boostCount < boostCap)
                boostCount = boostCount + 6;
        }
        if(other.gameObject.tag == "FullBoost")
        {
            if (boostCount < boostCap)
                boostCount = boostCap;
        }
    }

    public void CheckForFlip()
    {
        if ((Input.GetAxis("Vertical") != 0) || (Input.GetAxis("Horizontal") != 0))
        {
            print("debug");
            if (Input.GetKeyDown(KeyCode.Space) == true && doubleJump)
            {
                print("Flipping");
                Flip();
            }
        }
    }

    /*public void Flip()
    {
        //Vector3 FlipDirection = new Vector3(flipForce * Input.GetAxis("Vertical"), 0, flipForce * Input.GetAxis("Horizontal"));
        Input.GetAxis("Vertical") ;
        doubleJump = false;
        //rb.linearVelocity = new Vector4 (flipForce * Input.GetAxis("Vertical"), 0, flipForce * Input.GetAxis("Horizontal"), Time.deltaTime);
        //rb.AddForce(FlipDirection);
        rb.AddRelativeForce(Vector3.right * (flipForce * Input.GetAxis("Horizontal")), ForceMode.Acceleration);
        rb.AddRelativeForce(Vector3.forward * (flipForce * Input.GetAxis("Vertical")), ForceMode.Acceleration);
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.y);

        /*carAngleVelocityE = new Vector3(360 * Input.GetAxis("Vertical"), 0, 0);
        Quaternion deltaRotationE = Quaternion.Euler(carAngleVelocityE * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotationE);

        Rotate();
    }

    IEnumerator Rotate()
    {
        float elapsedTime = 0f;
        float duration = 0.5f; // Adjust duration as needed
        float rotationAmount = 360f * Input.GetAxis("Vertical");
        Quaternion startRotation = rb.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(rotationAmount, 0, 0);

        while (elapsedTime < duration)
        {
            rb.MoveRotation(Quaternion.Slerp(startRotation, endRotation, elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.MoveRotation(endRotation);

    }*/

    public void Flip()
    {
        float verticalInput = Input.GetAxis("Vertical"); // Store input before coroutine
        float horizontalInput = Input.GetAxis("Horizontal");

        doubleJump = false;

        rb.AddRelativeForce(Vector3.right * (flipForce * horizontalInput), ForceMode.Acceleration);
        rb.AddRelativeForce(Vector3.forward * (flipForce * verticalInput), ForceMode.Acceleration);
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        StartCoroutine(Rotate(verticalInput)); // Pass stored input
    }

    IEnumerator Rotate(float verticalInput)
    {
        float elapsedTime = 0f;
        float duration = 0.5f; // Adjust duration as needed
        float rotationAmount = 360f * verticalInput;

        Quaternion startRotation = rb.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(rotationAmount, 0, 0);

        while (elapsedTime < duration)
        {
            rb.MoveRotation(Quaternion.Slerp(startRotation, endRotation, elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.MoveRotation(endRotation);
    }
}
