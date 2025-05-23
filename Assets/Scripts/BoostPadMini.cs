using UnityEngine;

public class BoostPadMini : MonoBehaviour
{
    int boostQuantity = 12;

    public bool active = true;
    public float currentTime = 5f;

    public GameObject boostpad;
    MeshRenderer rnd;

    public CarControl cc;
    private void Start()
    {
        rnd = boostpad.GetComponent<MeshRenderer>();
    }
    void Update()
    {
        if (active)
            return;
        if (!active)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                StopTimer();
            }
        }
    }

    public void StopTimer()
    {
        active = true;
        currentTime = 0;
        rnd.enabled = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && cc.boostCount < 100)
        {
            if (active == true)
            {
                cc.boostCount = cc.boostCount + 12;

                currentTime = 5;
                active = false;
                rnd.enabled = false;
            }
        }
    }
}
