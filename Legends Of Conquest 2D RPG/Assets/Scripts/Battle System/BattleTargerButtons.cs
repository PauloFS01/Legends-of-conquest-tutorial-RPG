using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleTargerButtons : MonoBehaviour
{
    public string moveName;
    public int activeBattleTarget;
    public TextMeshProUGUI targetName;

    void Start()
    {
        targetName = GetComponentInChildren<TextMeshProUGUI>();
    }

    public  void Press()
    {
        BattleManager.instance.PlayerAttack(moveName, activeBattleTarget);
    }
}
