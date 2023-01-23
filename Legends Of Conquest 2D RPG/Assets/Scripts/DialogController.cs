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
    void Start()
    {
        dialogText.text = dialogSentences[currentSentences];
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
