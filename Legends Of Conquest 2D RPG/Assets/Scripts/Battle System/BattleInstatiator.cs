using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInstatiator : MonoBehaviour
{
    [SerializeField] BattleTypeManager[] avaiableBattles;
    [SerializeField] bool activeOnEnter;

    private bool inArea;

    [SerializeField] float timeBetweenBattles;
    private float battleCouter;

    [SerializeField] bool deactivateAterStarting;

    private void Start()
    {
        battleCouter = Random.Range(timeBetweenBattles * 0.5f, timeBetweenBattles * 1.5f);
    }

    private void Update()
    {
        if(inArea && !Player.instance.deactivateMovement)
        {
            if(Input.GetAxisRaw("Horizontal") !=0 || Input.GetAxisRaw("Vertical") !=0)
            {
                battleCouter -= Time.deltaTime;
            }
        }
        if(battleCouter <= 0)
        {
            battleCouter = Random.Range(timeBetweenBattles * 0.5f, timeBetweenBattles * 1.5f);
            StartCoroutine(StartBattleCoroutine());
        }
    }

    private IEnumerator StartBattleCoroutine()
    {
        MenuManager.instance.FadeImage();
        GameManager.instance.battleIsActive = true;
        int selectBattle = Random.Range(0, avaiableBattles.Length);

        BattleManager.instance.itemsReward = avaiableBattles[selectBattle].rewardItems;
        BattleManager.instance.XPRewardsAmount = avaiableBattles[selectBattle].rewardXP;

        yield return new WaitForSeconds(1.5f);

        MenuManager.instance.EndFade();

        BattleManager.instance.StartBattle(avaiableBattles[selectBattle].enemies);

        if (deactivateAterStarting)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
         if(collision.CompareTag("Player"))
        {
            if (activeOnEnter)
            {
                StartCoroutine(StartBattleCoroutine());
            }
            else
            {
                inArea = true;
            }
        }
    }
}
