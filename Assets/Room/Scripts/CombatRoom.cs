using System.Diagnostics;
using UnityEngine;

public class CombatRoom : Room
{
    private GameObject enemy;
    
    public GameObject GetEnemy()
    {
        return enemy;
    }

    void GenerateEnemy()
    {
        //roomNumber;
    }

    public override void Initialize()
    {
        print("CombatRoom");
    }

    public override void OnEnter()
    {
        GenerateEnemy();

        print("Entered Combat");
        //ExplorationManager.GetInstance().Advance();
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
