using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System;

public class SettingsMenu : MonoBehaviour
{
    public GameObject main_menu;
    public GameObject settings_menu;

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

    public void QuitGame()
    {
        // Debug.Log("QUIT");

        // Save changes
        gameSettings.update_settings(master_vol, music_vol, sfx_vol, brightness, screen_size, graphic_quality, fullscreenBool);

        Application.Quit();
    }

    public void Back()
    {
        // Save changes
        gameSettings.update_settings(master_vol, music_vol, sfx_vol, brightness, screen_size, graphic_quality, fullscreenBool);

        if (main_menu != null && settings_menu != null)
        {
            main_menu.SetActive(!main_menu.activeSelf);
            settings_menu.SetActive(!settings_menu.activeSelf);
        }
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

    /*
    void PrintURPSettings()
    {
        var urpAsset = QualitySettings.renderPipeline as UniversalRenderPipelineAsset;

        if (urpAsset == null)
        {
            Debug.LogWarning("Current quality level does not use a URP Asset.");
            return;
        }

        Debug.Log("Render Scale: " + urpAsset.renderScale);
        Debug.Log("MSAA Level: " + urpAsset.msaaSampleCount);
        Debug.Log("Shadow Distance: " + urpAsset.shadowDistance);
    }
    */
}
