using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    private bool isBattleActive;

    [SerializeField] GameObject battleScene;
    [SerializeField] List<BattleCharacters> activeCharacters = new List<BattleCharacters>();

    [SerializeField] Transform[] playerPositions, enemiesPositions;

    [SerializeField] BattleCharacters[] playerPrefabs, enemyPrefabs;

    [SerializeField] int currentTurn;
    [SerializeField] bool waitingForTurn;
    [SerializeField] GameObject UIButtonHolder;

    [SerializeField] BattleMoves[] battleMovesList;

    [SerializeField] ParticleSystem characterAttackEffect;

    [SerializeField] CharacterDamageGUI damageText;

    [SerializeField] GameObject[] playerBattleStats;

    [SerializeField] TextMeshProUGUI[] playersNameText;
    [SerializeField] Slider[] playerHealthSlider, playerManaSlider;

    [SerializeField] GameObject enemyTargetPanel;

    [SerializeField] BattleTargerButtons[] targetButtons;

    public GameObject magicChoicePannel;
    [SerializeField] BattleMagicButtons[] magicButtons;

    public BattleNotifications battleNotice;

    [SerializeField] float chanceToRunWay = 0.5f;

    public GameObject itemsToUseMenu;
    [SerializeField] ItemsManager selectedItem;
    [SerializeField] GameObject itemSlotContainer;
    [SerializeField] Transform itemSlotContainerParent;
    [SerializeField] Text itemName, itemDescription;

    [SerializeField] GameObject characterChoicePanel;
    [SerializeField] TextMeshProUGUI[] playerNames;

    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartBattle(new string[] { "Mage Master", "Warlok" });
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            NextTurn();
        }

        CheckPLayerButtonHolder();
    }

    private void CheckPLayerButtonHolder()
    {
        if (isBattleActive)
        {
            if (waitingForTurn)
            {
                if (activeCharacters[currentTurn].IsPlayer())
                {
                    UIButtonHolder.SetActive(true);
                }
                else
                {
                    UIButtonHolder.SetActive(false);
                    StartCoroutine(EnemyMoveCoroutine());
                }
            }
        }
    }

    public void StartBattle(string[] enemiesToSpaw)
    {
        SettingBatle();
        AddingPlayers();

        AddingEnemies(enemiesToSpaw);

        UpdatePlayerStats();

        waitingForTurn = true;
        currentTurn = Random.Range(0, activeCharacters.Count);
    }

    private void AddingEnemies(string[] enemiesToSpaw)
    {
        for (int i = 0; i < enemiesToSpaw.Length; i++)
        {
            if (enemiesToSpaw[i] != "")
            {
                for (int j = 0; j < enemyPrefabs.Length; j++)
                {
                    BattleCharacters newEnemy = Instantiate(
                        enemyPrefabs[i],
                        enemiesPositions[i].position,
                        enemiesPositions[i].rotation,
                        enemiesPositions[i]
                        );

                    activeCharacters.Add(newEnemy);
                }
            }
        }
    }

    private void AddingPlayers()
    {
        for (int i = 0; i < GameManager.instance.GetPlayerStats().Length; i++)
        {
            if (GameManager.instance.GetPlayerStats()[i].gameObject.activeInHierarchy)
            {
                for (int j = 0; j < playerPrefabs.Length; j++)
                {
                    if (playerPrefabs[j].characterName == GameManager.instance.GetPlayerStats()[i].playerName)
                    {
                        BattleCharacters newPlayer = Instantiate(
                            playerPrefabs[j],
                            playerPositions[i].position,
                            playerPositions[i].rotation,
                            playerPositions[i]
                            );

                        activeCharacters.Add(newPlayer);
                        ImportPlayerStats(i);

                    }
                }
            }
        }
    }

    private void ImportPlayerStats(int i)
    {
        PlayerStats player = GameManager.instance.GetPlayerStats()[i];

        activeCharacters[i].currentHP = player.currentHP;
        //activeCharacters[i].maxHp = player.maxHp;

        activeCharacters[i].currentMana = player.currentHP;
        //activeCharacters[i].maxMana = player.maxMana;

        activeCharacters[i].dexterity = player.dexterity;
        activeCharacters[i].defence = player.defence;

        //activeCharacters[i].wpnPower = player.wpnPower;
        activeCharacters[i].armorDefence = player.armorDefence;
    }

    private void SettingBatle()
    {
        if (!isBattleActive)
        {
            isBattleActive = true;
            GameManager.instance.battleIsActive = true;

            transform.position = new Vector3(
                Camera.main.transform.position.x,
                Camera.main.transform.position.y,
                transform.position.z
                );

            battleScene.SetActive(true);
        }
    }

    private void NextTurn()
    {
        currentTurn++;
        if(currentTurn >= activeCharacters.Count)
        {
            currentTurn = 0;
        }

        waitingForTurn = true;
        UpdateBattle();
        UpdatePlayerStats();
    }

    private void UpdateBattle()
    {
        bool allEnemiesAreDeath = true;
        bool allPlayersAreDeath = true;

        for (int i = 0; i < activeCharacters.Count; i++)
        {
            if (activeCharacters[i].currentHP < 0)
            {
                activeCharacters[i].currentHP = 0;
            }
            if (activeCharacters[i].currentHP == 0)
            {
                // Todo kill characters
            }
            else
            {
                if (activeCharacters[i].IsPlayer())
                {
                    allPlayersAreDeath = false;
                }
                else
                {
                    allEnemiesAreDeath = false;
                }
            }
        };

        if(allEnemiesAreDeath || allPlayersAreDeath)
        {
            if (allEnemiesAreDeath)
            {
                print("You win");
            }
            else if (allPlayersAreDeath)
            {
                print("You lost");
            }
            battleScene.SetActive(false);
            GameManager.instance.battleIsActive = false;
            isBattleActive = false;
        }
        else
        {
            while (activeCharacters[currentTurn].currentHP == 0)
            {
                currentTurn++;
                if(currentTurn >= activeCharacters.Count)
                {
                    currentTurn = 0;
                }
            }

        }

    }

    public IEnumerator EnemyMoveCoroutine()
    {
        waitingForTurn = false;

        yield return new WaitForSeconds(1f);
        EnemyAttack();

        yield return new WaitForSeconds(1f);
        NextTurn();
    }

    private void EnemyAttack()
    {
        List<int> players = new List<int>();


        for (int i = 0; i < activeCharacters.Count; i++)
        {
            if (activeCharacters[i].IsPlayer() && activeCharacters[i].currentHP > 0)
            {
                players.Add(i);
            }
        }

        int selectPlayerToAttack = players[Random.Range(0, players.Count)];
        int seleceAttack = Random.Range(0, activeCharacters[currentTurn].AttackMovesAvaiable().Length);
        int movePower = 0;

        for (int i = 0; i < battleMovesList.Length; i++)
        {
            if (battleMovesList[i].moveAttack == activeCharacters[currentTurn].AttackMovesAvaiable()[seleceAttack])
            {
                movePower = GettingMovePowerAndEffectInstatiation(seleceAttack, i);
            }
        }
        InstatiateEffectOnAttackCharacter();

        DealDamageToCharacters(selectPlayerToAttack, movePower);

        UpdatePlayerStats();

    }

    private void InstatiateEffectOnAttackCharacter()
    {
        Instantiate(
            characterAttackEffect,
            activeCharacters[currentTurn].transform.position,
            activeCharacters[currentTurn].transform.rotation
            );
    }

    private void DealDamageToCharacters(int selectedCharacterToAttack, int movePower)
    {
        float attackPower = activeCharacters[currentTurn].dexterity + activeCharacters[currentTurn].wpnPower;
        float defenceAmount = activeCharacters[selectedCharacterToAttack].defence + activeCharacters[selectedCharacterToAttack].armorDefence;

        float damageAmount = (attackPower / defenceAmount) * movePower * Random.Range(0.9f, 1.1f);
        int damageToGive = (int)damageAmount;

        damageToGive = CalculatingCritical(damageToGive);

        Debug.Log(activeCharacters[currentTurn].characterName + " Just deal " + damageAmount + "(" + damageToGive + ") to " + activeCharacters[selectedCharacterToAttack]);

        activeCharacters[selectedCharacterToAttack].TakeHPDamage(damageToGive);

        CharacterDamageGUI characterDamageText = Instantiate(
            damageText,
            activeCharacters[selectedCharacterToAttack].transform.position,
            activeCharacters[selectedCharacterToAttack].transform.rotation
            );

        characterDamageText.SetDamage(damageToGive);
    }

    private int CalculatingCritical(int damageToGive)
    {
        if(Random.value <= 0.1f)
        {
            Debug.Log("Critical Hit insted of " + damageToGive + " Points. " + (damageToGive * 2) + " was deal.");

            return (damageToGive * 2);
        }

        return damageToGive;
    }

    public void UpdatePlayerStats()
    {
        for(int i = 0; i < playersNameText.Length; i++)
        {
            if(activeCharacters.Count > i)
            {
                if (activeCharacters[i].IsPlayer())
                {
                    BattleCharacters playerData = activeCharacters[i];

                    playersNameText[i].text = playerData.characterName;

                    playerHealthSlider[i].maxValue = playerData.maxHP;
                    playerHealthSlider[i].value = playerData.currentHP;

                    playerManaSlider[i].maxValue = playerData.maxHP;
                    playerManaSlider[i].value = playerData.maxMana;

                }
                else
                {
                    playerBattleStats[i].SetActive(false);
                }
            }
            else
            {
                playerBattleStats[i].SetActive(false);

            }
        }
    }

    public void PlayerAttack(string moveNome, int selectEnemyTarget)
    {
        //int selectEnemyTarget = 3;
        int movePower = 0;
        for(int i =0; i < battleMovesList.Length; i++)
        {
            if (battleMovesList[i].moveAttack == moveNome)
            {
                movePower = GettingMovePowerAndEffectInstatiation(selectEnemyTarget, i);
            }
        }
        InstatiateEffectOnAttackCharacter();
        DealDamageToCharacters(selectEnemyTarget, movePower);

        NextTurn();

        enemyTargetPanel.SetActive(false);
    }

    public void OpenTargetMenu(string moveName)
    {
        enemyTargetPanel.SetActive(true);

        List<int> Enemies = new List<int>();
        for(int i =0; i < activeCharacters.Count; i++)
        {
            if (!activeCharacters[i].IsPlayer())
            {
                Enemies.Add(i);
            }
        }
        //Debug.Log(Enemies.Count);

        for(int i = 0; i < targetButtons.Length; i++)
        {
            if(Enemies.Count > i)
            {
                targetButtons[i].gameObject.SetActive(true);
                targetButtons[i].moveName = moveName;
                targetButtons[i].activeBattleTarget = Enemies[i];
                targetButtons[i].targetName.text = activeCharacters[Enemies[i]].characterName;

            }
        }
    }

    private int GettingMovePowerAndEffectInstatiation(int characterTarget, int i)
    {
        int movePower;
        Instantiate(
            battleMovesList[i].theEffectToUse,
            activeCharacters[characterTarget].transform.position,
            activeCharacters[characterTarget].transform.rotation
        );

        movePower = battleMovesList[i].movePower;
        return movePower;
    }

    public void OpenMagicPanel()
    {
        magicChoicePannel.SetActive(true);
        for(int i = 0;i < magicButtons.Length; i++)
        {
            if (activeCharacters[currentTurn].AttackMovesAvaiable().Length > 1)
            {
                magicButtons[i].gameObject.SetActive(true);
                magicButtons[i].spellName = GetCurrentActiveCaaracter().AttackMovesAvaiable()[i];
                magicButtons[i].spellNameText.text = magicButtons[i].spellName;

                for(int j=0; j < battleMovesList.Length; j++)
                {
                    if (battleMovesList[j].moveAttack == magicButtons[i].spellName)
                    {
                        magicButtons[i].spellCost = battleMovesList[j].manaCost;
                        magicButtons[i].spellCostText.text = magicButtons[i].spellCost.ToString();
                    }
                }
            }
            else
            {
                magicButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public BattleCharacters GetCurrentActiveCaaracter()
    {
        return activeCharacters[currentTurn];
    }

    public void RunAway()
    {
        if(Random.value > chanceToRunWay)
        {
            isBattleActive = true;
            battleScene.SetActive(false);
        }
        else
        {
            NextTurn();
            battleNotice.SetText("No scape!");
            battleNotice.Activate();
        }
    }

    public void UpdateItemsInventory()
    {
        itemsToUseMenu.SetActive(true);

        foreach (Transform itemSlot in itemSlotContainerParent)
        {
            Destroy(itemSlot.gameObject);
        };
        foreach (ItemsManager item in Inventory.instance.GetItemsList())
        {
            RectTransform itemSlot = Instantiate(itemSlotContainer, itemSlotContainerParent).GetComponent<RectTransform>();

            Image itemImage = itemSlot.Find("Items Image").GetComponent<Image>();
            itemImage.sprite = item.itemsImage;

            //Text itemsAmoutText = itemSlot.Find("Amount Text").GetComponent<Text>();

            /*        if(item.amount > 1)
                    {
                        itemsAmoutText.text = item.amount.ToString();
                    }else
                    {
                        itemsAmoutText.text = "";
                    }*/

            itemSlot.GetComponent<ItemButton>().itemOnButton = item;

        }
    }

    public void SelectedItemToUse(ItemsManager itemToUse)
    {
        selectedItem = itemToUse;
        itemName.text = itemToUse.itemName;
        itemDescription.text = itemToUse.itemDescription;
    }

    public void OpenCharacterMenu()


    {
        if (selectedItem)
        {
            characterChoicePanel.SetActive(true);
            for(int i =0; i < activeCharacters.Count; i++)
            {
                if (activeCharacters[i].IsPlayer())
                {
                    PlayerStats activePlayer = GameManager.instance.GetPlayerStats()[i];

                    playerNames[i].text = activePlayer.playerName;

                    bool activePlayerInHierarchy = activePlayer.gameObject.activeInHierarchy;
                    playerNames[i].transform.parent.gameObject.SetActive(activePlayerInHierarchy);
                }
            }
        }
        else
        {
            print("no item selected");
        }
    }

    public void UseItemButton(int selectedPlayer)
    {
        activeCharacters[selectedPlayer].UseItemInTheBattle(selectedItem);
        Inventory.instance.RemoveItem(selectedItem);

        UpdatePlayerStats();
        CloseCharacterChoice();
        UpdateItemsInventory();
    }

    public void CloseCharacterChoice()
    {
        characterChoicePanel.SetActive(false);
        itemsToUseMenu.SetActive(false);
    }
}
