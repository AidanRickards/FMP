using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool gameAwake;

    public static int score1 = 0;
    public static int score2 = 0;
    public TMP_Text blueScore;
    public TMP_Text orangeScore;

    public GameObject player;
    public GameObject ball;

    public Rigidbody rb;
    public Rigidbody ballRb;

    public GameObject playerPos;
    public GameObject ballPos;

    public GameObject countdownObj;
    public TMP_Text countdown;

    public int countdownNum;

    float elapsedTime;
    public float countTime;
    public TMP_Text time;

    public TMP_Text wins;
    public GameObject gameFinished;

    void Start()
    {
        StartCoroutine("Restart");

        if (SettingsController.gameTime == 0)
        {
            print("done");
            countTime = 300;
        }
        if (SettingsController.gameTime == 1)
        {
            print("done");
            countTime = 600;
        }
        if (SettingsController.gameTime == 2)
        {
            print("done");
            countTime = 1200;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        countdown.text = countdownNum.ToString();

        blueScore.text = score1.ToString();
        orangeScore.text = score2.ToString();

        if (gameAwake)
        {
            if (SettingsController.gameTime != 3)
            {
                if (countTime > 0)
                {
                    Console.WriteLine("Working");
                    countTime -= Time.deltaTime;
                    int minutes = Mathf.FloorToInt(countTime / 60);
                    int seconds = Mathf.FloorToInt(countTime % 60);
                    time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                }
                else
                {
                    countTime = 0;
                    int minutes = Mathf.FloorToInt(countTime / 60);
                    int seconds = Mathf.FloorToInt(countTime % 60);
                    time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                }
            }
            if (SettingsController.gameTime == 3)
            {
                elapsedTime += Time.deltaTime;
                int minutes = Mathf.FloorToInt(elapsedTime / 60);
                int seconds = Mathf.FloorToInt(elapsedTime % 60);
                time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            
            if(SettingsController.gameTime != 3 && countTime <= 0)
            {
                StartCoroutine("GameOver");
            }

        }

        if (countTime < 0)
        {
            countTime = 0;
        }

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

    IEnumerator GameOver()
    {
        gameAwake = false ;
        WaitForSeconds wait = new WaitForSeconds(3);

        if(score1 > score2)
        {
            print("Game Over");
            countTime = 0;
            gameAwake = false;
            wins.color = Color.blue;
            wins.text = ("Blue Wins");
            gameFinished.SetActive(true);
            countTime = 0;
        }

        if (score1 < score2)
        {
            countTime = 0;
            gameAwake = false;
            wins.color = new Vector4(0.996f, 0.644f, 0, 1);
            wins.text = ("Orange Wins");
            gameFinished.SetActive(true);
            countTime = 0;
        }

        yield return wait;

        SceneManager.LoadScene("Main Menu");
        print("Game complete");
    }
}
