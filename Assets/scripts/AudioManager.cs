using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake() {
        if (instance == null)
            instance = this;
        else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
        Play("Theme");
        PlayerPrefs.SetInt("isplaying", 1);
        Loop("Theme");
    }

    

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) { 
            soundError(); 
            return; 
        } else s.source.Play();
    }
    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            soundError();
            return;
        } else s.source.Stop();
    }

    public void ChangeVolume(string name, float volume) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) { 
            soundError(); 
            return; 
        }
        s.source.volume = volume;
    }

    public void Loop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            soundError();
            return;
        }
        s.source.loop = true;
    }

    void soundError() {
        Debug.LogWarning("Sound: " + name + " not found");
    }
}
