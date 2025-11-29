using UnityEngine;

public class PlayerStats : Stats
{
    [SerializeField][Min(0)]
    public float luck;
    [SerializeField][Min(0)] 
    public int maxLife;

    public void Heal(int amount)
    {
        life += amount;
        if (life > maxLife)
            life = maxLife;
    }

    public override void TakeDamage(float amount)
    {
        GameManager.Instance.playerManager.GetSystem<PlayerPactStack>(out PlayerPactStack stack);
        float newDefense = stack.ApplyEffectTo(defense, PlaceholderStatType.Defense);
        float newStrength = amount - newDefense;
        base.TakeDamage(newStrength);
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

    public void AddMaxLife(int amount)
    {
        maxLife += amount;
    }

    public void ReduceMaxLife(int amount)
    {
        maxLife -= amount;
        if(maxLife < 0)
            maxLife = 0;
    }

    public  void LowerMaxLife(float pourcentage)
    {
        maxLife -= (int)(maxLife * pourcentage);
        if (maxLife < 0)
            maxLife = 0;
    }

    public void AddMaxLifePourcentage (float pourcentage)
    {
        maxLife += (int)(maxLife * pourcentage);
    }

    public void AddStrength (int  amount)
    {
        strength += amount;
    }

    public void AddStrengthPourcentage(float pourcentage)
    {
        strength += (int)(strength * pourcentage);
    }

    public void ReduceStrength (int amount)
    {
        strength -= amount;
        if (strength < 0) 
            strength = 0;
    }

    public void ReduceStrengthPourcentage (float pourcentage)
    {
        strength -= (int)(strength * pourcentage);
        if (strength < 0)
            strength = 0;
    }

    public void AddDefense (int amount)
    {
        defense += amount;
    }

    public  void AddDefensePourcentage (float pourcentage)
    {
        defense += (int)(defense * pourcentage);
    }

    public void ReduceDefense(int amount)
    {
        defense -= amount;
        if (defense < 0)
            defense = 0;
    }

    public void ReduceDefensePourcentage(float pourcentage)
    {
        defense -= (int)(defense * pourcentage);
        if (defense < 0)
            defense = 0;
    }

    public void AddAgility (int amount)
    {
        agility += amount;
    }

    public void AddAgilityPourcentage (float pourcentage)
    {
        agility += (int)(agility * pourcentage);
    }

    public void ReduceAgility (int amount)
    {
        agility -= amount;
        if (agility < 0)
            agility = 0;
    }

    public void ReduceAgilityPourcentage(float pourcentage)
    {
        agility -= (int)(agility * pourcentage);
        if (agility < 0)
            agility = 0;
    }

    void Update()
    {

        //AddMaxLife(20);
        //AddStrength(20);
        //AddLuck(0.2f);
        //AddDefense(20);
        //AddAgility(20);

        //ReduceStrengthPourcentage(1f);
        //ReduceDefensePourcentage(1f);
        //ReduceAgilityPourcentage(1f);
        //LowerMaxLife(1f);

        //AddMaxLifePourcentage(0.5f);
        //AddStrengthPourcentage(0.5f);
        //AddDefensePourcentage(0.5f);
        //AddAgilityPourcentage(0.5f);

        //ReduceAgility (140);
        //ReduceDefense(140);
        //ReduceLuck(140);
        //ReduceMaxLife(140);
        //ReduceStrength(140);

    }
}