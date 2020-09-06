using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Sprite muted;
    public Sprite unmuted;
    public GameObject muteBtn;

    private AudioManager audio;

    private float ThemeVolume;

    private void Start() {
        audio = FindObjectOfType<AudioManager>();

        if (PlayerPrefs.GetInt("isplaying") == 0) { 
            audio.Play("Theme");
            PlayerPrefs.SetInt("isplaing", 1);
        }
        PlayerPrefs.SetFloat("volume", PlayerPrefs.GetFloat("volume", 1f));
        ThemeVolume = PlayerPrefs.GetFloat("volume");
    }
    private void Update() {
        if (PlayerPrefs.GetFloat("volume") == 1f) muteBtn.GetComponent<Image>().sprite = unmuted;
        else muteBtn.GetComponent<Image>().sprite = muted;

        PlayerPrefs.SetFloat("volume", ThemeVolume);

        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }

    public void startGame() {
        SceneManager.LoadScene("game");
    }

    public void exitGame() {
        Application.Quit();
    }

    public void Mute() {
        if (ThemeVolume == 1f) ThemeVolume = 0;
        else ThemeVolume = 1f;
    }
}
