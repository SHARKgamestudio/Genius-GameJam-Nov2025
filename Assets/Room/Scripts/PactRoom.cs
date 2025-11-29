using UnityEngine;

public class PactRoom : Room
{
    int debug;

    public override void Initialize()
    {
        print("PactRoom");
    }

    public override void OnEnter()
    {
        print("Entered Pact");
        //ExplorationManager.GetInstance().Advance();
    }

    public override void OnExit()
    {
        base.OnExit();
        print("Exited Pact");
    }

    public override RoomType GetRoomType()
    {
        return RoomType.PACT;
    }
}