using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public Text TimerText; // Assign in Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Timer timer = other.GetComponent<Timer>();
            if (timer != null)
            {
                timer.StopTimer(); // Stop the timer

                // Update the text style
                TimerText.fontSize = 65; // Increase font size
                TimerText.color = Color.green; // Change color to green

                Debug.Log("Player reached WinFlag! Timer stopped.");
            }
        }
    }
}
