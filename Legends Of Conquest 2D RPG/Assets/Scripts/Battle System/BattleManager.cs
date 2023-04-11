using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
