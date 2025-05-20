using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public Toggle invertY;
    private string previousScene;
    private bool invert;
    public AudioMixer masterMixer;
    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Start()
    {
        previousScene = PlayerPrefs.GetString("Previous", "MainMenu");
        invertY.isOn = PlayerPrefs.GetInt("InvertY", 0) == 1;

        // Load saved values or defaults
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

    }

    public void SetBGMVolume(float volume)
    {
        float dB = volume > 0.01f ? 20f * Mathf.Log10(volume) : -80f;
        masterMixer.SetFloat("BGMVolume", dB);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        float dB = volume > 0.01f ? 20f * Mathf.Log10(volume) : -80f;
        masterMixer.SetFloat("SFXVolume", dB);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void Back()
    {
        SceneManager.LoadScene(previousScene);
    }

    public void Apply()
    {
        invert = invertY.isOn;
        PlayerPrefs.SetInt("InvertY", invert ? 1 : 0);
        SceneManager.LoadScene(previousScene);
    }

    // Save volume settings playerprefs
    void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("BGMVolume", bgmSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.Save();
    }



}