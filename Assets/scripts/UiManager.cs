using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text scoreText;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public Button playAgainBtn;
    public PlayerController player;
    public Text overScore;
    public Text overHighscore;
    public GameManager gameManager;
    public GameObject muteBtn;
    public Text newRecordText;
    
    public bool isover;

    public Sprite muted;
    public Sprite unmuted;

    private float ThemeVolume;


    void Start() {  
        newRecordText.text = "";
        ThemeVolume = PlayerPrefs.GetFloat("volume");
        isover = false;
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    void Update() {       
        if (PlayerPrefs.GetFloat("volume") == 1f) muteBtn.GetComponent<Image>().sprite = unmuted;
        else muteBtn.GetComponent<Image>().sprite = muted;
        PlayerPrefs.SetFloat("volume", ThemeVolume);

        scoreText.text = player.score.ToString();
        overScore.text = "score: " + scoreText.text;
        overHighscore.text = "highscore: " + PlayerPrefs.GetInt("highscore").ToString();

        if (isover) gameOverPanel.SetActive(true);
    }

    public void pause(bool onOff) {
        pausePanel.SetActive(onOff);
    }

    public void Mute() {
        if (ThemeVolume == 1f) ThemeVolume = 0f;
        else ThemeVolume = 1f;
    }
    public void newRecord(int score) {
        newRecordText.text = "new";
        overHighscore.text = player.score.ToString();
        PlayerPrefs.SetInt("highscore", score);
    }
}
