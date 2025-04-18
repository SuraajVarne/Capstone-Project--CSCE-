using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 60;
    public Text timerText;

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = Mathf.RoundToInt(timeRemaining).ToString();
        }
        else
        {
            timerText.text = "Time's up!";
            // You can also end game or show a popup here
        }
    }
}

