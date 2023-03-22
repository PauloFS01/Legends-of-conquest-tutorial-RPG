using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource[] SFX, backGroundMusic;

    public static AudioManager instance;

    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayBackgroundMusic(4);
        }
        
    }

    public void PlaySFX(int soundToPLay)
    {
        if(soundToPLay < SFX.Length)
        {
            SFX[soundToPLay].Play();
        }
    }

    public void PlayBackgroundMusic(int musicToPlay)
    {
        StopMusic();
        if (musicToPlay < backGroundMusic.Length)
        {
            backGroundMusic[musicToPlay].Play();
        }
    }

    public void StopMusic()
    {
        foreach(AudioSource music in backGroundMusic)
        {
            music.Stop();
        }
    }
}
