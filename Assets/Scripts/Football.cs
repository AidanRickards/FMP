using UnityEngine;

public class Football : MonoBehaviour
{
    public GameObject goal1;
    public BoxCollider goal2;
    public ParticleSystem particle;
    public GameController gameController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("goal1") == true)
        {
            if (GameController.gameAwake == true)
            {
                Debug.Log("Goal1 hit");
                particle.Play();
                GameController.score2++;

                gameController.StartGoal();
            }
        }

        if (other.CompareTag("goal2") == true)
        {
            if (GameController.gameAwake == true)
            {
                Debug.Log("Goal2 hit");
                particle.Play();
                GameController.score1++;

                gameController.StartGoal();
            }
        }
    }

    
}
