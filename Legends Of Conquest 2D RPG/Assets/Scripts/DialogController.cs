using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogText, nameText;
    [SerializeField] GameObject dialogBox, nameBox;

    [SerializeField] string[] dialogSentences;
    [SerializeField] int currentSentences;

    private bool justStartedTotalk;

    public static DialogController instance;
    void Start()
    {
        instance = this;
        dialogText.text = dialogSentences[currentSentences];
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogBox.activeInHierarchy)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                if (!justStartedTotalk)
                {

                    currentSentences++;
                    if(currentSentences >= dialogSentences.Length)
                    {
                        dialogBox.SetActive(false);
                        Player.instance.deactivateMovement = false;
                    }
                    else
                    {
                        dialogText.text = dialogSentences[currentSentences];
                    }
                }
                else
                {
                    justStartedTotalk = false;
                }
            }
        }
    }

    public void ActivateDialog(string[] newSentecesToUse)
    {
        dialogSentences = newSentecesToUse;
        currentSentences = 0;

        justStartedTotalk = true;
        dialogText.text = dialogSentences[currentSentences];
        dialogBox.SetActive(true);

        Player.instance.deactivateMovement = true;
    }

    public bool IsDialogBoxActive()
    {
        return dialogBox.activeInHierarchy;
    }
}
