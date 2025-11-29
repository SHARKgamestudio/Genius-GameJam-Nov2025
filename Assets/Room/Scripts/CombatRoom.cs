using UnityEngine;

public class CombatRoom : Room
{
    [SerializeField] public Vector3 enemyPos;
    [SerializeField] GameObject enemyPrefab;
    
    private GameObject enemy;
    public GameObject GetEnemy()
    {
        return enemy;
    }

    void GenerateEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab);
        enemy = newEnemy;
        enemy.transform.position = enemyPos;
    }

    public override void Initialize()
    {
        print("CombatRoom");
    }

    public override void OnEnter()
    {
        GenerateEnemy();

        GameManager.Instance.fightingManager.StartFight(enemy);

        print("Entered Combat");
    }

    public override void OnExit()
    {
        base.OnExit();
        print("Exited Combat");
    }
    public override RoomType GetRoomType()
    {
        return RoomType.COMBAT;
    }
}
