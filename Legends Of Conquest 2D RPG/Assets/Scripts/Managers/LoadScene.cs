using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] float waitToLoadTime;
    void Start()
    {
        if (waitToLoadTime > 0)
            StartCoroutine(LoadScenes());
    }

    public IEnumerator LoadScenes()
    {
        yield return new WaitForSeconds(waitToLoadTime);

        SceneManager.LoadScene(PlayerPrefs.GetString("Current_Scene"));
        GameManager.instance.LoadData();
        QuestManager.instance.LoadQuestData();
    }
}
