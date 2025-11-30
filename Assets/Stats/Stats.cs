using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField][Min(0)]
    public float life;
    [SerializeField][Min(0)]
    public float strength;
    [SerializeField] 
    public float defense;
    [SerializeField][Min(0)]
    public float agility;

    virtual public void TakeDamage(float amount)
    {
        if (amount < 1)
            amount = 1;

        life -= amount;
        if (life < 0)
            life = 0;
    }

    public bool IsAlive ()
    {
        return life > 0;
    }
}