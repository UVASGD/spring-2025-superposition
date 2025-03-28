using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Settings gameSettings;

    float master_vol;
    float music_vol;
    float sfx_vol;

    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource sfx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Grab all the current settings for the game established by the player
        master_vol = gameSettings.master_vol;
        music_vol = gameSettings.music_vol;
        sfx_vol = gameSettings.sfx_vol;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
