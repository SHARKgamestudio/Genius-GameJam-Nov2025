using UnityEngine;
using System;
public class FightingManager : MonoBehaviour
{
    private FightingPlayer playerScript;
    private PlayerStats playerStats;

    private FightingEnemy enemyScript;
    private EnemyStats enemyStats;

    public FightingPriorityStat playerPriority;
    public FightingPriorityStat enemyPriority;

    bool isFightGoing = false;
    bool endFight = false;
    bool isAnimationGoing = false;
    float onGoingAnimationFrame = 0.0f;
    public bool doubleHit = false;
    bool secondHit = false;

    void Awake()
    {
        playerPriority = new FightingPriorityStat(0.0f, 0.0f, 1.0f);
        enemyPriority = new FightingPriorityStat(0.0f, 0.0f, 1.0f);
    }

    private void Start()
    {
        GameManager.Instance.playerManager.GetSystem<PlayerStats>(out playerStats);
        GameManager.Instance.playerManager.GetSystem<FightingPlayer>(out playerScript);
    }

    public void StartFight(GameObject enemy)
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
        if (!isFightGoing) 
            return;

        if (isAnimationGoing)
        {
            onGoingAnimationFrame -= Time.deltaTime;
            if (onGoingAnimationFrame <= 0.0f)
                isAnimationGoing = false;

            if (!isAnimationGoing && endFight)
            {
                isFightGoing = false;
                if (enemyStats.IsAlive())
                {
                    GameManager.Instance.GameOver();
                    return;
                }
                Destroy(enemyScript.gameObject);
                GameManager.Instance.AddToScore(GameManager.Instance.explorationManager.lastRoomNumber * 10);
                GameManager.Instance.explorationManager.WalkTowardsNextDoor();
            } 

            return;
        }

        if (!enemyStats.IsAlive())
        {
            endFight = true;
            onGoingAnimationFrame = enemyScript.Die();
            isAnimationGoing = true;
            return;
        }

        if (!playerStats.IsAlive())
        {
            endFight = true;
            onGoingAnimationFrame = playerScript.Die();
            isAnimationGoing = true;
            return;
        }

        bool playerTurn = playerPriority.IncrementATBByNTicks(1);
        bool enemyTurn = enemyPriority.IncrementATBByNTicks(1);

        if (playerTurn)
        {
            Debug.Log("Player attacks");

            if (doubleHit && !secondHit) 
            {
                onGoingAnimationFrame = playerScript.Attack();
                enemyScript.ReceiveDamage();

                enemyStats.TakeDamage(playerStats.strength);
                isAnimationGoing = true;
                secondHit = true;
                return;
            }

            onGoingAnimationFrame = playerScript.Attack();
            enemyScript.ReceiveDamage();
            enemyStats.TakeDamage(playerStats.strength);
            playerPriority.DecreamentATB();
            isAnimationGoing = true;
            secondHit = false;
        }
        else if (enemyTurn)
        {
            Debug.Log("Enemy attacks");
            onGoingAnimationFrame = enemyScript.Attack();
            playerScript.ReceiveDamage();

            playerStats.TakeDamage(enemyStats.strength);
            enemyPriority.DecreamentATB();
            isAnimationGoing = true;
        }
    }
}