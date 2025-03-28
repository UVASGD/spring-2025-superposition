using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.Collections.Generic;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pause_menu;

    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    public Settings gameSettings;

    float master_vol;
    float music_vol;
    float sfx_vol;
    float brightness;
    int screen_size;
    int graphic_quality;
    bool fullscreenBool;

    private void Start()
    {
        // Grab all the current settings for the game established by the player
        master_vol = gameSettings.master_vol;
        music_vol = gameSettings.music_vol;
        sfx_vol = gameSettings.sfx_vol;
        brightness = gameSettings.brightness;
        screen_size = gameSettings.screen_size;
        graphic_quality = gameSettings.graphic_quality;
        fullscreenBool = gameSettings.fullscreenBool;

        // Setup the screen sizes dropdown options
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);

        if (screen_size == currentResolutionIndex)
        {
            resolutionDropdown.value = currentResolutionIndex;
        }
        else
        {
            resolutionDropdown.value = screen_size;
        }

        resolutionDropdown.RefreshShownValue();

        // Establish all settings as current
        SetMasterVolume(master_vol);
        SetMusicVolume(music_vol);
        SetSFXVolume(sfx_vol);
        SetBrightness(brightness);
        SetResolution(screen_size);
        SetQuality(graphic_quality);
        SetFullScreen(fullscreenBool);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void QuitGame()
    {
        // Debug.Log("QUIT");

        // Save changes
        gameSettings.update_settings(master_vol, music_vol, sfx_vol, brightness, screen_size, graphic_quality, fullscreenBool);

        Application.Quit();
    }

    void Pause()
    {
        pause_menu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Resume()
    {
        pause_menu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LoadMenu()
    {
        // Save changes
        gameSettings.update_settings(master_vol, music_vol, sfx_vol, brightness, screen_size, graphic_quality, fullscreenBool);

        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScreen");
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        fullscreenBool = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        screen_size = resolutionIndex;
    }

    // Modify Quality Levels in Unity Project
    public void SetQuality(int qualityIndex)
    {
        // Debug.Log(qualityIndex);

        QualitySettings.SetQualityLevel(qualityIndex, true);
        graphic_quality = qualityIndex;

        // Debug.Log("Quality Level set to: " + QualitySettings.names[QualitySettings.GetQualityLevel()]);
        // PrintURPSettings();
    }

    public void SetBrightness(float cur_brightness)
    {
        Screen.brightness = cur_brightness;
        brightness = cur_brightness;
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("master_volume", volume);
        master_vol = volume;
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("music_volume", volume);
        music_vol = volume;
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfx_volume", volume);
        sfx_vol = volume;
    }
}