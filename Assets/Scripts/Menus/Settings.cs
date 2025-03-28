using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings Instance;
    
    public float master_vol = 0.5f;
    public float music_vol = 0.5f;
    public float sfx_vol = 0.5f;

    public float brightness = 0.5f;
    public int screen_size = 16;
    public int graphic_quality = 0;
    public bool fullscreenBool = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist across scenes
    }

    public void update_settings(float cur_master_vol, float cur_music_vol, float cur_sfx_vol, float cur_brightness,
                        int cur_screen_size, int cur_graphic_quality, bool cur_fullscreenBool)
    {
        master_vol = cur_master_vol;
        music_vol = cur_music_vol;
        sfx_vol = cur_sfx_vol;
        brightness = cur_brightness;
        screen_size = cur_screen_size;
        graphic_quality = cur_graphic_quality;
        fullscreenBool = cur_fullscreenBool;
    }

    public (float, float, float, float, int, int, bool) get_settings()
    { 
        return (master_vol, music_vol, sfx_vol, brightness, screen_size, graphic_quality, fullscreenBool);
    }
}
