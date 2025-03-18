using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public CarControl car;
    public GameObject boostText;
    public Slider boostBar;

    public string boostString;

    TextMeshProUGUI boostTMPText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boostTMPText = boostText.GetComponent<TextMeshProUGUI>();
        boostBar.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        boostBar.value = car.boostCount;
        boostTMPText.text = ((int)car.boostCount).ToString();
    }
}
