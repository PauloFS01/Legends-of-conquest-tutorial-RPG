using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjects : MonoBehaviour
{
    [SerializeField] GameObject objectToActive;
    [SerializeField] string questToCheck;
    [SerializeField] bool activateIfComplete;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            CheckForCompletion();
        }
    }

    public void CheckForCompletion()
    {
        if (QuestManager.instance.CheckIfComplete(questToCheck))
        {
            objectToActive.SetActive(activateIfComplete);
        }
    }
}
