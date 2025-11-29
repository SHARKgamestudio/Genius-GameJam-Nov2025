using UnityEngine;

public class EnemyStats : Stats
{
    public override void TakeDamage(float amount)
    {
        amount -= defense;
        base.TakeDamage(amount);
    }
}