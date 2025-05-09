using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
