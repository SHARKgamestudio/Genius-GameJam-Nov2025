using UnityEngine;
using System;

public struct PriorityStat
{
    public double ATB;
    public double speed;
    public double maxSpeed;
    double tickSpeed;

    public int GetTicksToOneATB()
    {   
        int ticks = 0;
        double tempATB = ATB;
        while (tempATB < maxSpeed) {
            ticks++;
            tempATB += tickSpeed;
        }
        return ticks;
    }

    public void IncrementATBByNTicks(int ticks)
    {
        ATB += tickSpeed * ticks;
    }

    public void DecreamentATB()
    {
        if (ATB >= maxSpeed)
            ATB -= maxSpeed;
    }

    public void CalculateTickSpeed()
    {
        tickSpeed = speed / 100.0;
    }

    public void InitPriority(int agility)
    {
        speed = Math.Log(agility, 2.0);
        ATB = 0.0f;
        CalculateTickSpeed();
    }
}

public class FightingScriptManager : MonoBehaviour
{
    [SerializeField] public FightingPlayerScript playerScript;
    [SerializeField] public Stats statsPlayer;

    private FightingEnemyScript enemyScript;
    private Stats statsEnemy;

    PriorityStat playerPriority;
    PriorityStat enemyPriority;

    bool IsFightGoing = false;
    bool EndFight = false;
    bool IsAnimationGoing = false;
    int OnGoingAnimationFrame = 0;

    void StartFight(GameObject enemy)
    {
        IsFightGoing = true;
        EndFight = false;
        IsAnimationGoing = false;
        OnGoingAnimationFrame = 0;

        enemyScript = enemy.GetComponent<FightingEnemyScript>();
        statsEnemy = enemy.GetComponent<EnnemyStats>();

        enemyPriority.InitPriority(statsEnemy.agility);
        playerPriority.InitPriority(statsPlayer.agility);

        playerPriority.maxSpeed = enemyPriority.speed > playerPriority.speed ? enemyPriority.speed : playerPriority.speed;
        enemyPriority.maxSpeed = enemyPriority.speed > playerPriority.speed ? enemyPriority.speed : playerPriority.speed;
    }


    void Update()
    {
        if (!IsFightGoing) //TODO End fight scene
            return;

        if (IsAnimationGoing)
        {
            OnGoingAnimationFrame--;
            if (OnGoingAnimationFrame <= 0)
                IsAnimationGoing = false;

            if (!IsAnimationGoing && EndFight)
                IsFightGoing = false;

            return;
        }

        IsAnimationGoing = true;

        if (!statsEnemy.IsAlive())
        {
            EndFight = true;
            enemyScript.Die();
            OnGoingAnimationFrame = enemyScript.GetDieFrame();
            return;
        }

        if (!statsPlayer.IsAlive())
        {
            EndFight = true;
            playerScript.Die();
            OnGoingAnimationFrame= playerScript.GetDieFrame();
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
            OnGoingAnimationFrame = playerScript.GetAttackFrame();
            playerScript.Attack();
            statsEnemy.TakeDamage(statsPlayer.strength);
            playerPriority.DecreamentATB();
        }
        else
        {
            OnGoingAnimationFrame = enemyScript.GetAttackFrame();
            enemyScript.Attack();
            statsPlayer.TakeDamage(statsEnemy.strength);
            enemyPriority.DecreamentATB();
        }

    }
}
