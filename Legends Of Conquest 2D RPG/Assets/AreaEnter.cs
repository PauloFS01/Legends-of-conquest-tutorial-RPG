using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AreaEnter : MonoBehaviour
{
    public string transitionAreaName;

    private Player playerClass;
    void Start()
    {
        StartCoroutine(WaitForPlayerClass());
    }

    void Update()
    {
        
    }

    private IEnumerator WaitForPlayerClass()
    {
        while (playerClass == null)
        {
            playerClass = FindObjectOfType<Player>();
            yield return null;
        }
        PlayerPosition();
    }
    private void PlayerPosition()
    {
        if (transitionAreaName == Player.instance.transitionName)
        {
            Player.instance.transform.position = transform.position;
        }
    }
}
