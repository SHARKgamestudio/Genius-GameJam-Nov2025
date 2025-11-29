using UnityEngine;
using System;
using JetBrains.Annotations;
public struct Stats
{
    public int life;
    public int str;
    public int def;
    public int age;
    public float luck;
}

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
    [SerializeField] public FightingEnemyScript enemyScript;
    [SerializeField] public FightingPlayerScript playerScript;

    PriorityStat playerPriority;
    PriorityStat enemyPriority;

    bool IsFightGoing = false;
    bool EndFight = false;
    bool IsAnimationGoing = false;
    int OnGoingAnimationFrame = 0;


    private void Start()
    {
        StartFight();
    }
    void StartFight()
    {
        IsFightGoing = true;
        EndFight = false;
        IsAnimationGoing = false;
        OnGoingAnimationFrame = 0;

        //enemyScript = ExplorationManager.GetInstance().GetCurrentRoom().GetNearestEnemy().GetComponent<EnemyScript>();
        Stats statsEnemy = enemyScript.GetStats();
        Stats statsPlayer = playerScript.GetStats();

        enemyPriority.InitPriority(statsEnemy.age);
        playerPriority.InitPriority(statsPlayer.age);

        playerPriority.maxSpeed = enemyPriority.speed > playerPriority.speed ? enemyPriority.speed : playerPriority.speed;
        enemyPriority.maxSpeed = enemyPriority.speed > playerPriority.speed ? enemyPriority.speed : playerPriority.speed;

    }


    void Update()
    {
        if (!IsFightGoing) //TODO :: end fight scene
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

        if (!enemyScript.IsAlive())
        {
            EndFight = true;
            enemyScript.Die();
            OnGoingAnimationFrame = enemyScript.GetDieFrame();
            return;
        }

        if (!playerScript.IsAlive())
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
            playerScript.Attack(enemyScript);
            playerPriority.DecreamentATB();
        }
        else
        {
            OnGoingAnimationFrame = enemyScript.GetAttackFrame();
            enemyScript.Attack(playerScript);
            enemyPriority.DecreamentATB();
        }

    }
}
