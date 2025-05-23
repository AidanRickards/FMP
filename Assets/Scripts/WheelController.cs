using UnityEngine;
using UnityEngine.Animations;
using System;
using System.Collections;

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
    public float stopForce = 1000000f;

    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentTurnAngle = 0f;

    public CarControl controller;

    public RotationConstraint fr;
    public RotationConstraint fl;
    public RotationConstraint br;
    public RotationConstraint bl;

    public float waitTime = 0.54f;
    bool crRunning = false;


    public void Update()
    {
        if (!GameController.gameAwake)
        {
            WheelFrictionCurve curve = frontLeft.forwardFriction;
            curve.stiffness = 0;

            frontLeft.forwardFriction = curve;
            frontRight.forwardFriction = curve;
            backLeft.forwardFriction = curve;
            backRight.forwardFriction = curve;
        }
        if (GameController.gameAwake)
        {
            WheelFrictionCurve curve = frontLeft.forwardFriction;
            curve.stiffness = 1;

            frontLeft.forwardFriction = curve;
            frontRight.forwardFriction = curve;
            backLeft.forwardFriction = curve;
            backRight.forwardFriction = curve;
        }
    }

    public void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            if (controller.grounded == true)
            {
                //get forward/reverse acceleration from the vertical axis (W and s keys)

                //AudioManager.Instance.PlaySound("Car");
                //StartCoroutine("Playsound");
                currentAcceleration = acceleration * Input.GetAxis("Vertical");

               
            }

            if (!crRunning)
                StartCoroutine("Playsound");


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

        if (Input.GetAxis("Vertical") == 0)
        {
            if (controller.grounded == true)
            {
                //get forward/reverse acceleration from the vertical axis (W and s keys)
                if(currentAcceleration > 0f)
                currentAcceleration = -acceleration * Input.GetAxis("Vertical");
            }
        }
            //if space is pressent give breakingforce value
            if (Input.GetButton ("Fire3"))
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

        FixWheel();
    }

    void UpdateWheel(WheelCollider col, Transform trans)
    {
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);

        trans.position = position;
        trans.rotation = rotation;
    }

    void FixWheel()
    {
        if (controller.grounded == false && (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L)))
        {
            frontLeft = GameObject.Find("FrontLeft").GetComponent<WheelCollider>();
            frontRight = GameObject.Find("FrontRight").GetComponent<WheelCollider>();
            backLeft = GameObject.Find("BackLeft").GetComponent<WheelCollider>();
            backRight = GameObject.Find("BackRight").GetComponent<WheelCollider>();

            frontLeftTransform = GameObject.Find("WheelFL").GetComponent<Transform>();
            frontRightTransform = GameObject.Find("WheelFR").GetComponent<Transform>();


        }
     }



    IEnumerator Playsound()
    {
        crRunning = true;
        AudioManager.Instance.PlaySound("Car");
        WaitForSeconds wait = new WaitForSeconds(waitTime);

        
        yield return wait;
        crRunning = false;
    }
}

