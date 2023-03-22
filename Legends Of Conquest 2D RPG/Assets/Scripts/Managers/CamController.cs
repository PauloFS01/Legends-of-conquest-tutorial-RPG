using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamController : MonoBehaviour
{
    private Player playerTarget;
    CinemachineVirtualCamera virtualCamera;

    [SerializeField] int musicToPlay;
    private bool musicAlreadyPlayed;
    void Start()
    {
        playerTarget = FindObjectOfType<Player>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        virtualCamera.Follow = playerTarget.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!musicAlreadyPlayed)
        {
            musicAlreadyPlayed = true;
            AudioManager.instance.PlayBackgroundMusic(musicToPlay);
        }
    }
}
