using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    public CarControl car;
    public GameObject boostText;
    public Slider boostBar;

    public string boostString;

    TextMeshProUGUI boostTMPText;

    public GameObject pauseMenu;
    public bool isPaused = false;
    private float fixedDeltaTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boostTMPText = boostText.GetComponent<TextMeshProUGUI>();
        boostBar.GetComponent<Slider>();
    }
    private void Awake()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        boostBar.value = car.boostCount;
        boostTMPText.text = ((int)car.boostCount).ToString();

        if (Input.GetButtonDown("Esc"))
        {
            if (isPaused)
            {
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
                isPaused = false;
                pauseMenu.SetActive(false);
                GameController.gameAwake = true;
            }
            if (!isPaused)
            {
                Time.timeScale = 0f;
                Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
                isPaused = true;
                pauseMenu.SetActive(true);
                GameController.gameAwake = false;
            }
        }
    }
    public void Resume()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        isPaused = false;
        pauseMenu.SetActive(false);
        GameController.gameAwake = true;
    }
    public void Quit()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        isPaused = false;
        pauseMenu.SetActive(false);
        GameController.gameAwake = true;
        print("Game quit");
        SceneManager.LoadScene("LoadingMenu");
    }
}
