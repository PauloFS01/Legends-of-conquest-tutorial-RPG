using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource[] SFX, backGround;

    public static AudioManager instance;

    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlaySFX(3);
        }
        
    }

    public void PlaySFX(int soundToPLay)
    {
        if(soundToPLay < SFX.Length)
        {
            SFX[soundToPLay].Play();
        }
    }
}
