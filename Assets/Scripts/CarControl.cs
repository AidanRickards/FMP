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

    private void Update()
    {
        

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
            if (Input.GetKeyDown(KeyCode.Space) && doubleJump !=(Input.GetAxis("Vertical") != 0))
            {
                rb.AddRelativeForce(Vector3.up * jumpForce);
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
}
