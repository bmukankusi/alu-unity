using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonHandler : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Check if the back button is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();  // Call the function to open the menu
        }
    }

    void OpenMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("menu");
    }
}
