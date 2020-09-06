using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public GameObject cometPrefab;
    public PlayerController player;
    public GameObject playerObj;
    public UiManager uiMan;

    private bool isPaused;

    private AudioManager audio;

    public int highscore;

    public float spawnDelay = 1f;

    void Start() {
        audio = FindObjectOfType<AudioManager>();

        Time.timeScale = 1;
        
        highscore = PlayerPrefs.GetInt("highscore");
        PlayerPrefs.SetInt("highscore", highscore);
        isPaused = false;
        float a = spawnDelay;
        spawnDelay = 0;
        StartCoroutine(spawnComet());
        spawnDelay = a;
    }

    void Update() {
        Debug.Log("prefs: " + PlayerPrefs.GetInt("highscore") + " local: " + highscore.ToString());
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        setHighscore();

        if (player.hp == 0) {
            player.hp--;
            FindObjectOfType<Shake>().CamShake("longshake");
            audio.Play("PlayerDeath");
            audio.Stop("Theme");
            PlayerPrefs.SetInt("isplaying", 0);

            playerObj.GetComponent<EdgeCollider2D>().enabled = false; ;
            player.explode();
            Invoke("endgame", 2f); 
        }

        if(Input.GetKeyDown(KeyCode.Escape) && !isPaused && !uiMan.isover) {
            isPaused = true;
            Time.timeScale = 0;
            uiMan.pause(true);
        } else if(Input.GetKeyDown(KeyCode.Escape) && isPaused && !uiMan.isover) {
            isPaused = false;
            Time.timeScale = 1;
            uiMan.pause(false);
        }
    }

    IEnumerator spawnComet() {
        yield return new WaitForSeconds(spawnDelay);
        if(!uiMan.isover) Instantiate(cometPrefab);
        if (spawnDelay > 1f) spawnDelay -= 0.1f;
        StartCoroutine(spawnComet());
    }

    void setHighscore() {
        if (player.score > PlayerPrefs.GetInt("highscore", 0) && !uiMan.isover) {
            uiMan.newRecord(player.score);
            PlayerPrefs.SetInt("highscore", player.score);
        }
    }

    public void resetHighscore() {
        uiMan.newRecordText.text = "";
        player.score = 0;
        highscore = 0;
        PlayerPrefs.SetInt("highscore", 0);
        Debug.Log(highscore + " " + PlayerPrefs.GetInt("highscore"));
    }

    public void goToMenu() {
        SceneManager.LoadScene("menu");
    }
    public void goToMenuEnd() {
        PlayerPrefs.SetInt("isplaying", 1);
        audio.Play("Theme");
        SceneManager.LoadScene("menu");
    }

    public void playAgain() {
        PlayerPrefs.SetInt("isplaying", 1);
        audio.Play("Theme");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void endgame() {
        uiMan.isover = true;
        Destroy(playerObj);
    }
}
