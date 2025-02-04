using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerBody;
    public float speed = 10.0f;
    private int score = 0;
    public int health = 5;
    public Text scoreText;
    public Text healthText;
    public Text winLoseText;
    public Image winLoseBG;

    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        SetScoreText();
        SetHealthText();

        // Set UI elements as hidden initially
        winLoseText.text = "";
        winLoseBG.color = Color.clear;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            playerBody.AddForce(Vector3.forward * speed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            playerBody.AddForce(Vector3.back * speed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            playerBody.AddForce(Vector3.left * speed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            playerBody.AddForce(Vector3.right * speed);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }

        if (health == 0)
        {
            GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            score++;
            Destroy(other.gameObject);
            SetScoreText();
        }

        if (other.CompareTag("Trap"))
        {
            health--;
            SetHealthText();
        }

        if (other.CompareTag("Goal"))
        {
            YouWin();
        }
    }

    void GameOver()
    {
        winLoseText.text = "Game Over!";
        winLoseText.color = Color.white;
        winLoseBG.color = Color.red;

        // Force UI to refresh before restarting
        Canvas.ForceUpdateCanvases();

        StartCoroutine(LoadScene(3f));
    }

    void YouWin()
    {
        winLoseText.text = "You Win!";
        winLoseText.color = Color.black;
        winLoseBG.color = Color.green;

        // Force UI to refresh before restarting
        Canvas.ForceUpdateCanvases();

        StartCoroutine(LoadScene(3f));
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void SetHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }

    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
