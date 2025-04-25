using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static bool gameAwake;

    public static int score1 = 0;
    public static int score2 = 0;

    public GameObject player;
    public GameObject ball;

    public Rigidbody rb;
    public Rigidbody ballRb;

    public GameObject playerPos;
    public GameObject ballPos;

    public GameObject countdownObj;
    public TMP_Text countdown;

    public int countdownNum;

    void Start()
    {
        StartCoroutine("Restart");
    }

    // Update is called once per frame
    void Update()
    {
        countdown.text = countdownNum.ToString();
    }

    public void StartGoal()
    {
        print("Starttest");
        Invoke("GoalScored", 3);
    }

    public void GoalScored()
    {
        Debug.Log("Started");

        gameAwake = false;

        player.transform.position = playerPos.transform.position;
        player.transform.rotation = playerPos.transform.rotation;

        rb.linearVelocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3 (0, 0, 0);


        ball.transform.position = ballPos.transform.position;
        ball.transform.rotation = ballPos.transform.rotation;
        ballRb.linearVelocity = new Vector3(0,0, 0);
        ballRb.angularVelocity = new Vector3(0,0, 0);

        Debug.Log("Finished");

        StartCoroutine("Restart");

    }

    IEnumerator Restart()
    {
        print("GameBegin");
        gameAwake = false;

        WaitForSeconds wait = new WaitForSeconds(1);

        countdownNum = 4;
        countdownObj.SetActive(true);

        for(int i = 0; i < 3;  i++)
        {
            countdownNum--;
            yield return wait;
        }

        countdownObj.SetActive(false);
        gameAwake = true;
    }
}
