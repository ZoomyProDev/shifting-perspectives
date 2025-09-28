using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public GameObject levelChanger;


    public GameObject Title;
    public GameObject playButton;
    public GameObject creditsButton;
    public GameObject settingsButton;
    public GameObject SettingsBG;
    public GameObject SettingsTitle;
    public GameObject Music;
    public GameObject SFX;
    public GameObject BackSettings;

    public GameObject Loading;

    private void Awake()
    {
        Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;

        if (PlayerPrefs.GetInt("Music", 1) == 1)
        {
            PlayerPrefs.SetInt("Music", 1);
        }
        if (PlayerPrefs.GetInt("SFX", 1) == 1)
        {
            PlayerPrefs.SetInt("SFX", 1);
        }
    }

    public void Play()
    {
        Title.SetActive(false);
        playButton.SetActive(false);
        creditsButton.SetActive(false);
        settingsButton.SetActive(false);

        Loading.SetActive(true);

        StartCoroutine(LoadAsynchronously("Main"));
    }

    public void Credits()
    {
        levelChanger.GetComponent<LevelChanger>().FadeToLevel("Credits");
    }


    public void Settings()
    {
        Title.SetActive(false);
        playButton.SetActive(false);
        creditsButton.SetActive(false);
        settingsButton.SetActive(false);
        SettingsBG.SetActive(true);
        SettingsTitle.SetActive(true);
        Music.SetActive(true);
        SFX.SetActive(true);
        BackSettings.SetActive(true);

    }

    public void SettingsBack()
    {
        Title.SetActive(true);
        playButton.SetActive(true);
        creditsButton.SetActive(true);
        settingsButton.SetActive(true);
        SettingsBG.SetActive(false);
        SettingsTitle.SetActive(false);
        Music.SetActive(false);
        SFX.SetActive(false);
        BackSettings.SetActive(false);
    }

    public void MusicChange()
    {
        if (Music.GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt("Music", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Music", 0);
        }
    }

    public void SFXChange()
    {
        if (SFX.GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt("SFX", 1);
        }
        else
        {
            PlayerPrefs.SetInt("SFX", 0);
        }
    }


    IEnumerator LoadAsynchronously (string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            Debug.Log(operation.progress);

            yield return null;
        }
    }
}
