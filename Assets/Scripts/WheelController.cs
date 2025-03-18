using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;

    [SerializeField] Transform frontRightTransform;
    [SerializeField] Transform frontLeftTransform;

    public float acceleration = 500f;
    public float breakingForce = 300f;
    public float maxTurnAngle = 15f;

    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentTurnAngle = 0f;

    public CarControl controller;

    private void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            if (controller.grounded == true)
            {
                //get forward/reverse acceleration from the vertical axis (W and s keys)

                currentAcceleration = acceleration * Input.GetAxis("Vertical");
            }
            
           
                /*if (!controller.boosting)
                {
                    if (controller.rb.linearVelocity.x > 15.1f)
                    {
                        controller.rb.linearVelocity = new Vector3(15, controller.rb.linearVelocity.y, controller.rb.linearVelocity.z);
                    }
                    if (controller.rb.linearVelocity.x < -15.1f)
                    {
                        controller.rb.linearVelocity = new Vector3(-15, controller.rb.linearVelocity.y, controller.rb.linearVelocity.z);
                    }

                if (controller.rb.linearVelocity.z > 15.1f)
                {
                    controller.rb.linearVelocity = new Vector3(controller.rb.linearVelocity.x, controller.rb.linearVelocity.y, 15);
                }
                if (controller.rb.linearVelocity.z < -15.1f)
                {
                    controller.rb.linearVelocity = new Vector3(controller.rb.linearVelocity.x, controller.rb.linearVelocity.y, -15);
                }

            }
                else
                {

                }*/
        }
        

        //if space is pressent give breakingforce value
        if (Input.GetKey(KeyCode.LeftControl))
        {
            currentBreakForce = breakingForce;
        }
        else
        {
            currentBreakForce = 0f;
        }

        //apply acceleration to front wheels

        frontRight.motorTorque = currentAcceleration;
        frontLeft.motorTorque = currentAcceleration;
        backLeft.motorTorque = currentAcceleration;
        backRight.motorTorque = currentAcceleration;

        frontRight.brakeTorque = currentBreakForce;
        frontLeft.brakeTorque = currentBreakForce;
        backRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;


        //steering
        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;

        UpdateWheel(frontLeft, frontLeftTransform);
        UpdateWheel(frontRight, frontRightTransform);
    }

    void UpdateWheel ( WheelCollider col, Transform trans)
    {
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);

        trans.position = position;
        trans.rotation = rotation;
    }
}
