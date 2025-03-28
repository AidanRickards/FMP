using UnityEngine;

public class WheelMove : MonoBehaviour
{
    public GameObject wheelEmpty;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector3 emptyPos = new Vector3(wheelEmpty.transform.position.x, wheelEmpty.transform.position.y, wheelEmpty.transform.position.z);
        //transform.position = emptyPos;
    }

    private void OnCollisionStay(Collision collision)
    {
        print(collision);
        if(collision.gameObject.name == "Ground")
        {
            Vector3 emptyPos = new Vector3(wheelEmpty.transform.position.x, wheelEmpty.transform.position.y, wheelEmpty.transform.position.z);
            transform.position = emptyPos;
            print("Collision with ground");
        }
    }

}
