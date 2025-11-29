using UnityEngine;

public abstract class FightingScript : MonoBehaviour
{
    public abstract float Attack();
    public abstract float ReceiveDamage();
    public abstract float Die();

}
