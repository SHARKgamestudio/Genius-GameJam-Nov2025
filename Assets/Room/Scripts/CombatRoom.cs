using UnityEngine;

public class CombatRoom : Room
{
    [SerializeField] GameObject enemyPrefab;
    
    private GameObject enemy;
    public GameObject GetEnemy()
    {
        return enemy;
    }

    void GenerateEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, this.transform);
        enemy = newEnemy;
    }

    public override void Initialize()
    {
        print("CombatRoom");
    }

    public override void OnEnter()
    {
        GenerateEnemy();

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
