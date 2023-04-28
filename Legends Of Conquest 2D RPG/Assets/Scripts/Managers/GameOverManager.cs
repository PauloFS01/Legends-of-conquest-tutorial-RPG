using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayBackgroundMusic(4);
    }

    public void QuitToMainMenu()
    {
        DestroyGameSession();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLastSave()
    {
        DestroyGameSession();
        SceneManager.LoadScene("LoadScene");
    }

    public static void DestroyGameSession()
    {
        Destroy(GameManager.instance.gameObject);
        Destroy(Player.instance.gameObject);
        Destroy(MenuManager.instance.gameObject);
        Destroy(BattleManager.instance.gameObject);
    }
}
