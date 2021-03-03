using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    public static UiController instance;
    public GameObject gameOverPanel;
    // public GameObject startUi;
    // public GameObject gameOverImage;
    public Text scoreText;
    public Text yourScoreText;
    public Text highScoreText;

    void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = ScoreController.instance.score.ToString();
    }
    
    public void GameStart() {
        scoreText.GetComponent<Text>().gameObject.SetActive(true);
    }

    public void GameOver() {
        scoreText.GetComponent<Text>().gameObject.SetActive(false);
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        yourScoreText.text = "Your Score: " + PlayerPrefs.GetInt("Score");
        gameOverPanel.SetActive(true);
    }

    public void Replay() {
        SceneManager.LoadScene("Level1");
    }

    public void Menu() {
        SceneManager.LoadScene("Menu");
    }
}
