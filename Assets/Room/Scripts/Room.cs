using UnityEngine;

public enum RoomType
{
    COMBAT,
    PACT,

    _COUNT
}

public abstract class Room : MonoBehaviour
{
    public int roomNumber;
    public float luckModifier;

    public abstract RoomType GetRoomType();
    public abstract void Initialize();
    public abstract void OnEnter();
    public virtual void OnExit() 
    {
        Destroy(gameObject);
    }
}
