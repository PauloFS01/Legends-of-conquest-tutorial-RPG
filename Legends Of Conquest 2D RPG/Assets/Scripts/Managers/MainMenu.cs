using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string newGameScene;
    [SerializeField] GameObject continueButton;
    void Start()
    {
        if (PlayerPrefs.HasKey("Player_Pos_X"))
            continueButton.SetActive(true);
        else
            continueButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGameButton()
    {
        SceneManager.LoadScene(newGameScene);
    }
    
    public void ExitButton()
    {
        Debug.Log("click exit button");
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene("LoadScene");
    }
}
