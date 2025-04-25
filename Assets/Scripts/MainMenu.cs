using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator animator;
    public Toggle toggle;
    public TMP_Dropdown dropdown;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(toggle.isOn)
        {
            SettingsController.unlimitedBoost = true;
        }
        if(!toggle.isOn)
        {
            SettingsController.unlimitedBoost = false;
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
        SceneManager.LoadScene("Game");
    }
}
