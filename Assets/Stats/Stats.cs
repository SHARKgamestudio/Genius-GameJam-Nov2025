using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] public int life;
    [SerializeField] public int strength;
    [SerializeField] public int defense;
    [SerializeField] public int agility;

    public void TakeDamage(int amount)
    {
        life -= amount;
        if (life < 0)
            life = 0;
    }

    public bool IsAlive ()
    {
        return life != 0;
    }
}
