using UnityEngine;

public class PlayerStats : Stats
{
    [SerializeField][Min(0)]
    public float luck;
    [SerializeField][Min(0)] 
    public float maxLife;

    public void Heal(float amount)
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

    public void AddMaxLife(float amount)
    {
        maxLife += amount;
    }

    public void ReduceMaxLife(float amount)
    {
        maxLife -= amount;
        if(maxLife < 0)
            maxLife = 0;

        if (life > maxLife)
            life = maxLife;
    }

    public  void LowerMaxLife(float pourcentage)
    {
        maxLife -= (float)(maxLife * pourcentage);
        if (maxLife < 0)
            maxLife = 0;

        if (life > maxLife)
            life = maxLife;
    }

    public void AddMaxLifePourcentage (float pourcentage)
    {
        maxLife += (float)(maxLife * pourcentage);
    }

    public void AddStrength (float  amount)
    {
        strength += amount;
    }

    public void AddStrengthPourcentage(float pourcentage)
    {
        strength += (float)(strength * pourcentage);
    }

    public void ReduceStrength (float amount)
    {
        strength -= amount;
        if (strength < 0) 
            strength = 0;
    }

    public void ReduceStrengthPourcentage (float pourcentage)
    {
        strength -= (float)(strength * pourcentage);
        if (strength < 0)
            strength = 0;
    }

    public void AddDefense (float amount)
    {
        defense += amount;
    }

    public  void AddDefensePourcentage (float pourcentage)
    {
        defense += (float)(defense * pourcentage);
    }

    public void ReduceDefense(float amount)
    {
        defense -= amount;
        if (defense < 0)
            defense = 0;
    }

    public void ReduceDefensePourcentage(float pourcentage)
    {
        defense -= (float)(defense * pourcentage);
        if (defense < 0)
            defense = 0;
    }

    public void AddAgility (float amount)
    {
        agility += amount;
    }

    public void AddAgilityPourcentage (float pourcentage)
    {
        agility += (float)(agility * pourcentage);
    }

    public void ReduceAgility (float amount)
    {
        agility -= amount;
        if (agility < 0)
            agility = 0;
    }

    public void ReduceAgilityPourcentage(float pourcentage)
    {
        agility -= (float)(agility * pourcentage);
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