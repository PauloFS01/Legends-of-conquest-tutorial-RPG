using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] string[] questNames;
    [SerializeField] bool[] questMakersCompleted;

    public static QuestManager instance;
    void Start()
    {
        instance = this;
        questMakersCompleted = new bool[questNames.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)){
            Debug.Log("Data has been saved");
            SaveQuestData();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Data has been load");
            LoadQuestData();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            print(CheckIfComplete("Defeat dragon"));
            MarkQuestComplete("Enter the cave");
            MarkQuestComplete("Steal gem");
        }
    }

    public int GetQuestNumber(string questToFind)
    {
        for(int i = 0; i < questNames.Length; i++)
        {
            if (questNames[i] == questToFind)
            {
                return i;
            }
        }

        Debug.LogWarning("Quest: " + questToFind + " does not exist");
        return 0;
    }

    public bool CheckIfComplete(string questToCheck)
    {
        int questNumberToCheck = GetQuestNumber(questToCheck);
        if(questNumberToCheck != 0)
        {
            return questMakersCompleted[questNumberToCheck];
        }
        return false;
    }

    public void UpdeteQuestObjects()
    {
        QuestObjects[] questObjects = FindObjectsOfType<QuestObjects>();

        if(questObjects.Length > 0)
        {
            foreach(QuestObjects questObject in questObjects)
            {
                questObject.CheckForCompletion();
            }
        }
    }

    public void MarkQuestComplete(string questToMark)
    {
        int questNumberToCheck = GetQuestNumber(questToMark);
        questMakersCompleted[questNumberToCheck] = true;

        UpdeteQuestObjects();
    }

    public void MarkQuestIncomplete(string questToMark)
    {
        int questNumberToCheck = GetQuestNumber(questToMark);
        questMakersCompleted[questNumberToCheck] = false;

        UpdeteQuestObjects();
    }

    public void SaveQuestData()
    {
        for (int i =0; i< questNames.Length; i++)
        {
            if (questMakersCompleted[i])
            {
                PlayerPrefs.SetInt("QuestMarker_" + questNames[i], 1);
            }
            else
            {
                PlayerPrefs.SetInt("QuestMarker_" + questNames[i], 0);
            }
        }
    }

    public void LoadQuestData()
    {
        for (int i = 0; i < questNames.Length; i++)
        {
            int valueToSet = 0;
            
            if(PlayerPrefs.HasKey("QuestMarker_" + questNames[i]))
            {
                valueToSet = PlayerPrefs.GetInt("QuestMarker_" + questNames[i]);
            }
            if(valueToSet == 0)
            {
                questMakersCompleted[i] = false;
            }
            else
            {
                questMakersCompleted[i] = true;
            }
        }
    }
}
