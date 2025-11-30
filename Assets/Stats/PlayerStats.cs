using UnityEngine;

public class PlayerStats : Stats
{
    [SerializeField][Min(0)]
    public float luck;
    [SerializeField][Min(0)] 
    public float maxLife;

    public bool hasThorns = false;

    public void Heal(float amount)
    {
        life += amount;
        if (life > maxLife)
            life = maxLife;
    }

    public override void TakeDamage(float amount)
    {
        float newStrength = amount - defense;
        base.TakeDamage(newStrength);

        if(hasThorns == true)
        {
            GameObject room = GameManager.Instance.explorationManager.GetRoom();
            GameObject enemy = room.GetComponent<CombatRoom>().GetEnemy();
            enemy.GetComponent<EnemyStats>().TakeDamage(defense);
        }
    }

    public float GetLuck()
    {
        return luck;
    }

    public void ReduceLuck(float amount)
    {
        luck -= amount;
        if (luck < 0)
            luck = 0;
    }

    public void AddLuck(float amount) //ratio de 0 a 1
    {
        luck += amount;
        if (luck > 1)
            luck = 1;
    }

    public void AddMaxLife(float amount)
    {
        maxLife += amount;
        Heal(amount);
    }

    public void ReduceMaxLife(float amount)
    {
        maxLife -= amount;
        if(maxLife < 1)
            maxLife = 1;

        if (life > maxLife)
            life = maxLife;
    }

    public  void LowerMaxLife(float pourcentage)
    {
        maxLife -= maxLife * pourcentage;
        if (maxLife < 1)
            maxLife = 1;

        if (life > maxLife)
            life = maxLife;
    }

    public void AddMaxLifePourcentage (float pourcentage)
    {
        float amount = maxLife * pourcentage;
        maxLife += amount;
        Heal(amount);
    }

    public void AddStrength (float  amount)
    {
        strength += amount;
    }

    public void AddStrengthPourcentage(float pourcentage)
    {
        strength += strength * pourcentage;
    }

    public void ReduceStrength (float amount)
    {
        strength -= amount;
        if (strength < 1) 
            strength = 1;
    }

    public void ReduceStrengthPourcentage (float pourcentage)
    {
        strength -= strength * pourcentage;
        if (strength < 1)
            strength = 1;
    }

    public void AddDefense (float amount)
    {
        defense += amount;
    }

    public  void AddDefensePourcentage (float pourcentage)
    {
        defense += defense * pourcentage;
    }

    public void ReduceDefense(float amount)
    {
        defense -= amount;
        if (defense < 0)
            defense = 0;
    }

    public void ReduceDefensePourcentage(float pourcentage)
    {
        defense -= defense * pourcentage;
        if (defense < 0)
            defense = 0;
    }

    public void AddAgility (float amount)
    {
        agility += amount;
    }

    public void AddAgilityPourcentage (float pourcentage)
    {
        agility += agility * pourcentage;
    }

    public void ReduceAgility (float amount)
    {
        agility -= amount;
        if (agility < 3)
            agility = 3;
    }

    public void ReduceAgilityPourcentage(float pourcentage)
    {
        agility -= agility * pourcentage;
        if (agility < 3)
            agility = 3;
    }

    public float GetSumsOfStats(bool ignoreLuck = true)
    {
        float sum = strength + agility + defense + maxLife;
        if (ignoreLuck)
            return sum;
        return sum + luck;
    }
}