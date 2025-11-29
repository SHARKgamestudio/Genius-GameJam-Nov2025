using UnityEngine;
using System;
public class FightingManager : MonoBehaviour
{
    [SerializeField] public FightingPlayer playerScript;
    [SerializeField] public Stats playerStats;

    private FightingEnemy enemyScript;
    private Stats enemyStats;

    FightingPriorityStat playerPriority;
    FightingPriorityStat enemyPriority;

    bool isFightGoing = false;
    bool endFight = false;
    bool isAnimationGoing = false;
    float onGoingAnimationFrame = 0.0f;

    void StartFight(GameObject enemy)
    {
        isFightGoing = true;
        endFight = false;
        isAnimationGoing = false;
        onGoingAnimationFrame = 0.0f;

        enemyScript = enemy.GetComponent<FightingEnemy>();
        enemyStats = enemy.GetComponent<EnemyStats>();

        enemyPriority.InitPriority(enemyStats.agility);
        playerPriority.InitPriority(playerStats.agility);

        playerPriority.maxSpeed = enemyPriority.speed > playerPriority.speed ? enemyPriority.speed : playerPriority.speed;
        enemyPriority.maxSpeed = enemyPriority.speed > playerPriority.speed ? enemyPriority.speed : playerPriority.speed;

        EntryFightAnimation();
    }

    void EntryFightAnimation()
    {
        // TODO :: lauch Entry fight animation
        onGoingAnimationFrame = 1.0f;
        isAnimationGoing = true;
    }


    void Update()
    {
        if (!isFightGoing) //TODO :: End fight ?
            return;

        if (isAnimationGoing)
        {
            onGoingAnimationFrame -= Time.deltaTime;
            if (onGoingAnimationFrame <= 0.0f)
                isAnimationGoing = false;

            if (!isAnimationGoing && endFight)
                isFightGoing = false;

            return;
        }

        isAnimationGoing = true;

        if (!enemyStats.IsAlive())
        {
            endFight = true;
            onGoingAnimationFrame = enemyScript.Die();
            return;
        }

        if (!playerStats.IsAlive())
        {
            endFight = true;
            onGoingAnimationFrame = playerScript.Die();
            return;
        }

        int enemyTicks = enemyPriority.GetTicksToOneATB();
        int playerTicks = playerPriority.GetTicksToOneATB();

        int lowestTicks = enemyTicks < playerTicks ? enemyTicks : playerTicks;
        bool playerTurn = enemyTicks < playerTicks ? false : true;

        enemyPriority.IncrementATBByNTicks(lowestTicks);
        playerPriority.IncrementATBByNTicks(lowestTicks);

        if (playerTurn)
        {
            onGoingAnimationFrame = playerScript.Attack();
            enemyScript.ReceiveDamage();
            enemyStats.TakeDamage(playerStats.strength);
            playerPriority.DecreamentATB();
        }
        else
        {
            onGoingAnimationFrame = enemyScript.Attack();
            playerScript.ReceiveDamage();
            playerStats.TakeDamage(enemyStats.strength);
            enemyPriority.DecreamentATB();
        }

    }
}
