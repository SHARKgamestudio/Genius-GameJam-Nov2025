using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField][Min(0)]
    public int life;
    [SerializeField][Min(0)]
    public int strength;
    [SerializeField] 
    public int defense;
    [SerializeField][Min(0)]
    public int agility;

    public void TakeDamage(int amount)
    {
        amount -= defense;
        if (amount < 0)
            return;

        life -= amount;
        if (life < 0)
            life = 0;
    }

    public bool IsAlive ()
    {
        return life > 0;
    }
}
