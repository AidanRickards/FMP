using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class CarControl : MonoBehaviour
{
    public Rigidbody rb;

    public GameObject carBody;
    public GameObject player;

    public WheelController wc;

    public bool grounded = false;
    public float groundCheckDistance;
    private float bufferCheckDistance = 0.1f;
    public float jumpForce;
    public float boostForce;
    public Vector3 velocity;
    public float flipForce;
    private bool flipping = false; // Prevents mid-air spamming
    public float flipDuration = 0.5f; // Controls how fast the flip completes

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

    bool flipStarted;

    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider backRight;
    public WheelCollider backLeft;

    //public GameObject wheelsPrefab;
    public GameObject wheelSpawnPoint;
    GameObject wheels;

    public GameObject carCamera;
    public GameObject ballCamera;
    bool ballCam = false;
    int ballCamNum = -1;
    

    public JumpTimer jt;
    private void Start()
    {
        carAngleVelocityA = new Vector3(0, 50, 0);
        carAngleVelocityB = new Vector3(0, 75, 0);
        flipStarted = false;

        wheels = GameObject.FindGameObjectWithTag("Wheel");
    }
    private void Update()
    {

        ReadController();

        /*if (GameController.gameAwake)
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
            }

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

            if (Input.GetButtonDown("Fire1") && grounded)
            {
                rb.AddRelativeForce(Vector3.up * jumpForce);
            }

            if (Input.GetButton("Fire2") && boostCount > 0)
            {
                rb.AddRelativeForce(Vector3.forward * boostForce);
                pSystem1.Emit(1);
                pSystem2.Emit(1);
                if (SettingsController.unlimitedBoost == false)
                    boostCount = boostCount - 0.1f;
            }

            NotGrounded();
            Grounded();


            if (Input.GetKeyDown(KeyCode.U))
            {
                /*Destroy(this);
                Instantiate(player, wheelSpawnPoint.transform.position,Quaternion.Euler(0,this.transform.rotation.y, 0), this.gameObject.transform);
            }
        }*/

        if (ballCamNum == 1)
        {
            carCamera.SetActive(false);
            ballCamera.SetActive(true);
        }
        if (ballCamNum == -1)
        {
            carCamera.SetActive(true);
            ballCamera.SetActive(false);
        }

        if (Input.GetButtonDown("Fire4"))
        {
            ballCamNum = ballCamNum * -1;
        }




        //ground check
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
    }

    private void FixedUpdate()
    {
        if (GameController.gameAwake)
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

            

            if (Input.GetButtonDown("Fire1") && grounded)
            {
                rb.AddRelativeForce(Vector3.up * jumpForce);
            }

            if (Input.GetButton("Fire2") && boostCount > 0)
            {
                rb.AddRelativeForce(Vector3.forward * boostForce);
                pSystem1.Emit(1);
                pSystem2.Emit(1);
                if (SettingsController.unlimitedBoost == false)
                    boostCount = boostCount - 0.1f;
            }

            NotGrounded();
            Grounded();


            if (Input.GetKeyDown(KeyCode.U))
            {
                /*Destroy(this);
                Instantiate(player, wheelSpawnPoint.transform.position,Quaternion.Euler(0,this.transform.rotation.y, 0), this.gameObject.transform);*/
            }
        }

        if (ballCamNum == 1)
        {
            carCamera.SetActive(false);
            ballCamera.SetActive(true);
        }
        if (ballCamNum == -1)
        {
            carCamera.SetActive(true);
            ballCamera.SetActive(false);
        }

        if (Input.GetButtonDown("Fire4"))
        {
            ballCamNum = ballCamNum * -1;
        }
    }

    public void NotGrounded()
    {
        if (!grounded)
        {
            //Double Jump
            if (Input.GetButtonDown("Fire1") && doubleJump && Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") ==0)
            {
                rb.AddRelativeForce(Vector3.up * jumpForce);
                doubleJump = false;
            }

            //ariel control turning horizontal
            if (Input.GetAxis("Horizontal") != 0 && Input.GetKey(KeyCode.Space) == false)
            {
                carAngleVelocityA = new Vector3(0, 150 * Input.GetAxis("Horizontal"), 0);
                Quaternion deltaRotation = Quaternion.Euler(carAngleVelocityA * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotation);
            }

            //ariel control turning vertical
            if (Input.GetAxis("Vertical") != 0 && Input.GetKey(KeyCode.Space) == false)
            {
                carAngleVelocityB = new Vector3(200 * Input.GetAxis("Vertical"), 0, 0);
                Quaternion deltaRotationB = Quaternion.Euler(carAngleVelocityB * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotationB);
            }

            //air roll left
            if (Input.GetButton("LeftShoulder"))
            {
                carAngleVelocityC = new Vector3(0, 0, 240);
                Quaternion deltaRotationC = Quaternion.Euler(carAngleVelocityC * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotationC);

                //frontLeft.transform.rotation = backLeft.transform.rotation;
                //frontRight.transform.rotation = backRight.transform.rotation;

                /*Destroy(wheels);
                wheels = Instantiate (wheelsPrefab, wheelSpawnPoint.transform.position, wheelSpawnPoint.transform.rotation, this.gameObject.transform);
                */

                //frontLeft.localRotation = Quaternion.Euler(0, frontLeft.steerAngle, 0);
            }

            //air roll right
            if (Input.GetButton("RightShoulder"))
            {
                carAngleVelocityD = new Vector3(0, 0, -240);
                Quaternion deltaRotationD = Quaternion.Euler(carAngleVelocityD * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotationD);

                //frontLeft.transform.rotation = backLeft.transform.rotation;
                //frontRight.transform.rotation = backRight.transform.rotation;

                //wheelTransform.localRotation = Quaternion.Euler(0, wheelCollider.steerAngle, 0);
            }

            //flip

            if (flipStarted == false)
            {
                CheckForFlip();
            }
            else
            {
                StartCoroutine("Test");

                /*float currentRotationZ = transform.localEulerAngles.z;
                float currentRotationX = transform.localEulerAngles.x;

                float currentlyHeldZ = Input.GetAxis("Horizontal");
                float currentlyHeldX = Input.GetAxis("Vertical");

                //
                for (int i = 0; i < 360; i++)
                {
                    transform.Rotate(1 * currentlyHeldX, 0, 1 * currentlyHeldZ);
                    flipStarted = false;
                }
                //check for ground

                //check for flip finishing

                print("zr=" + transform.localEulerAngles.z);*/
            }

        }
    }

    public void Grounded()
    {
        if (grounded)
        {
            doubleJump = true;
            //carBody.transform..z = (carBody.transform.rotation.z * 0);
            carBody.transform.localEulerAngles = Vector3.zero; 
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
            if (Input.GetButtonDown("Fire1") == true && doubleJump)
            {
                flipStarted = true;
                //print("Flipping");
                //Flip();

            }
        }
    }

    public void Flip()
    {
        //Vector3 FlipDirection = new Vector3(flipForce * Input.GetAxis("Vertical"), 0, flipForce * Input.GetAxis("Horizontal"));
        Input.GetAxis("Vertical") ;
        doubleJump = false;
        //rb.linearVelocity = new Vector4 (flipForce * Input.GetAxis("Vertical"), 0, flipForce * Input.GetAxis("Horizontal"), Time.deltaTime);
        //rb.AddForce(FlipDirection);
        rb.AddRelativeForce(Vector3.right * (flipForce * Input.GetAxis("Horizontal")), ForceMode.Acceleration);
        rb.AddRelativeForce(Vector3.forward * (flipForce * Input.GetAxis("Vertical")), ForceMode.Acceleration);
        //rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.y);

        carAngleVelocityE = new Vector3(360 * Input.GetAxis("Vertical"), 0, 0);
        Quaternion deltaRotationE = Quaternion.Euler(carAngleVelocityE * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotationE);

        //Rotate();
    }

    IEnumerator Test()
    {
        WaitForSeconds wait = new WaitForSeconds(0.0068f);

        Flip();

        float currentRotationZ = transform.localEulerAngles.z;
        float currentRotationX = transform.localEulerAngles.x;

        float currentlyHeldZ = -Input.GetAxis("Horizontal");
        float currentlyHeldX = Input.GetAxis("Vertical");

        if(currentlyHeldX > 0)
        {
            currentlyHeldX = 1;
        }
        if (currentlyHeldX < 0)
        {
            currentlyHeldX = -1;
        }
        if(currentlyHeldZ > 0)
        {
            currentlyHeldZ = 1;
        }
        if (currentlyHeldZ < 0)
        {
            currentlyHeldZ = -1;
        }

        //
        for (int i = 0; i < 60; i++)
        {
            transform.Rotate(6 * currentlyHeldX, 0, 0);
            carBody.transform.Rotate(0, 0, 6 * currentlyHeldZ);
            flipStarted = false;
            yield return wait;
        }
        //check for ground

        //check for flip finishing

        
        print("zr=" + transform.localEulerAngles.z);
        
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

    }


    void ReadController()
    {
        if (Input.GetButton("Esc"))
        {
            print("Esc pressed");
        }

        if (Input.GetButton("RightShoulder"))
        {
            print("right shoulder pressed");
        }

        if (Input.GetButton("Fire1"))
        {
            print("Fire 1");
        }

        if (Input.GetButton("Fire2"))
        {
            print("Fire 2");
        }

        if (Input.GetButton("Fire3"))
        {
            print("Fire 3");
        }
        if (Input.GetButton("Fire4"))
        {
            print("Fire 4");
        }


    }

    /*
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
    }*/

    /*public void Flip()
    {
        {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");

            if (verticalInput == 0 && horizontalInput == 0) return; // No input, no flip

            flipping = true;
            doubleJump = false;

            // Cancel Y velocity to avoid weird physics
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

            // Determine force direction
            Vector3 flipForceDirection = transform.forward * verticalInput + transform.right * horizontalInput;
            rb.AddForce(flipForceDirection * flipForce, ForceMode.Impulse);

            // Determine rotation axis
            Vector3 rotationAxis = Vector3.right * verticalInput + Vector3.forward * horizontalInput;

            // Apply controlled torque
            rb.AddTorque(rotationAxis * flipForce * 10f, ForceMode.Impulse);

            StartCoroutine(ResetFlip());
        }
    }

    private IEnumerator ResetFlip()
    {
        yield return new WaitForSeconds(flipDuration);
        flipping = false; // Allow another flip after cooldown
    }*/
}
