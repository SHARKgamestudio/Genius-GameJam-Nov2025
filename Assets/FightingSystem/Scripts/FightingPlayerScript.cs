using UnityEngine;
public class FightingPlayerScript : MonoBehaviour
{   
    [SerializeField] Stats stats;

    bool isAlive = true;

    private void Awake()
    {
        stats.age = 10;
        stats.life = 50;
        stats.str = 2;
        stats.def = 1;
    }

    public Stats GetStats()
    {
        return stats;
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    public void Attack(FightingEnemyScript other)
    {
        other.ReceiveAttack(stats.str);
        // TODO launch attck anim
    }

    public void ReceiveAttack(int attack)
    { 
        //TODO change with stats and launch take damage anime 
        int damage = attack - stats.def;
        if (damage > 0)
            stats.life -= damage;

        if (stats.life <= 0)
            isAlive = false;
    }

    public int GetAttackFrame()
    {
        //TODO :: change
        return 10;
    }

    public void Die()
    {
        // TODO launch die anim
    }

    public int GetDieFrame()
    {
        // TODO :: change
        return 10;
    }
}
