using UnityEngine;

public class BoostPadFull : MonoBehaviour
{
    int boostQuantity = 12;

    public bool active = true;
    public float currentTime = 10f;

    public GameObject boostpad;
    public MeshRenderer rnd;
    public MeshRenderer rnd2;

    public CarControl cc;

    private void Start()
    {
        
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
        rnd2.enabled = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && cc.boostCount < 100)
        {

            currentTime = 10;
            active = false;
            rnd.enabled = false;
            rnd2.enabled = false;
        }
    }
}
