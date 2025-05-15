using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator animator;
    public Toggle toggle;
    public TMP_Dropdown dropdown;

    public Slider goalCounter;
    public TMP_Text levelText;

    public Slider _musicSlider, _sfxSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SettingsController.gameTime = dropdown.value;

        if(toggle.isOn)
        {
            SettingsController.unlimitedBoost = true;
        }
        if(!toggle.isOn)
        {
            SettingsController.unlimitedBoost = false;
        }

        if(SettingsController.totalGoals < 10)
        {
            goalCounter.maxValue = 10;
            levelText.text = "Current Level: 1";
        }
        if (SettingsController.totalGoals < 25 && SettingsController.totalGoals >= 10)
        {
            goalCounter.maxValue = 25;
            levelText.text = "Current Level: 2";
        }
        if (SettingsController.totalGoals < 100 && SettingsController.totalGoals >= 25)
        {
            goalCounter.maxValue = 100;
            levelText.text = "Current Level: 3";
        }

        if (SettingsController.totalGoals < 100)
        {
            goalCounter.value = SettingsController.totalGoals;
        }
        else
        {
            goalCounter.value = 100;
            levelText.text = "Current Level: MAX";
        }

            SettingsController.gameTime = dropdown.value;

        //Debug.Log(SettingsController.unlimitedBoost);
        //Debug.Log(SettingsController.gameTime);
        
    }

    public void StartSettings()
    {
        animator.Play("Settings");
    }

    public void BackSettings()
    {
        animator.Play("SettingsBack");
    }

    public void StartStart()
    {
        animator.Play("Start");
    }

    public void BackStart()
    {
        animator.Play("StartBack");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LoadingGame");
        GameController.score1 = 0;
        GameController.score2 = 0;
    }


    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSound()
    {
        AudioManager.Instance.ToggleSound();
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }

    public void SoundVolume()
    {
        AudioManager.Instance.SoundVolume(_sfxSlider.value);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
