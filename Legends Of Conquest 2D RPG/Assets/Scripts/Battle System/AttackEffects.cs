using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffects : MonoBehaviour
{
    [SerializeField] float effectTime;
    [SerializeField] int SFXNumberToPlay;
    void Start()
    {
        AudioManager.instance.PlaySFX(SFXNumberToPlay);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, effectTime);
    }
}
