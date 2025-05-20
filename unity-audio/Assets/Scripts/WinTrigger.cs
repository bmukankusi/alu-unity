using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameObject winScreen;
    public Timer timer;

    private void OnTriggerEnter(Collider other)
    {
        winScreen.SetActive(true);
        if (other.gameObject.CompareTag("Player"))
        {
            timer.StopTimer();
        }
    }

    //Stop background music on win
    public void StopBackgroundMusic()
    {
        AudioSource backgroundMusic = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }
    }
}