using UnityEngine;

public class CameraWork : MonoBehaviour
{
    public GameObject settings;
    public GameObject main;
    public GameObject start;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowSettings()
    {
        settings.SetActive(true);
    }

    public void ShowMain()
    {
        main.SetActive(true);
    }

    public void ShowStart()
    {
        start.SetActive(true);
    }
}
