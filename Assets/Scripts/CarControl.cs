using UnityEngine;

public class CarControl : MonoBehaviour
{
    public Rigidbody rb;

    public bool grounded = false;
    public float groundCheckDistance;
    private float bufferCheckDistance = 0.1f;
    public float jumpForce;
    public float boostForce;
    public Vector3 velocity;

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
        
    }

    public void NotGrounded()
    {
        if (!grounded)
        {
            //Double Jump
            if (Input.GetKeyDown(KeyCode.Space) && doubleJump )
            {
                rb.AddRelativeForce(Vector3.up * jumpForce);
                doubleJump = false;
            }

            //ariel control turning horizontal
            if(Input.GetAxis("Horizontal") != 0)
            {
                carAngleVelocityA = new Vector3(0, 50 * Input.GetAxis("Horizontal"), 0);
                Quaternion deltaRotation = Quaternion.Euler(carAngleVelocityA * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotation);
            }

            //ariel control turning horizontal
            if (Input.GetAxis("Vertical") != 0)
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
        }
    }

    public void Grounded()
    {
        if (grounded)
        {
            doubleJump = true;
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
}
