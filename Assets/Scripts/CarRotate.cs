using UnityEngine;
using System.Collections;

public class CarRotate : MonoBehaviour
{

    public CarControl cc;

    public bool flipStart = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flipStart)
            StartCoroutine("Test2");
    }

    IEnumerator Test2()
    {
        WaitForSeconds wait = new WaitForSeconds(0.0048f);

        float currentRotationZ = transform.localEulerAngles.z;

        float currentlyHeldZ = -Input.GetAxis("Horizontal");

        if (currentlyHeldZ > 0)
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
            transform.Rotate(0, 0, 6 * currentlyHeldZ);
            flipStart = false;
            yield return wait;
        }
        //check for ground

        //check for flip finishing

        print("zr=" + transform.localEulerAngles.z);

    }
}
