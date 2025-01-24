using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //variables
    public Material trapMat;
    public Material goalMat;
    public Toggle colorblindMode;
   
    // Load maze scene
    public void PlayMaze()
    {
        if (colorblindMode.isOn)
        {
            trapMat.color = new Color32(255, 112, 0, 1);
            goalMat.color = Color.blue;
        }
      
        SceneManager.LoadScene("maze");
    }

    // Method to quit game window
    public void QuitMaze()
    {
        //close game window when Quit button pressed
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
