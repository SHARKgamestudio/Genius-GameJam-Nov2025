using UnityEngine;
public class FightingPlayer : FightingScript
{   
    public override float Attack()
    {
        // TODO :: lauch Attack animation
        return 0.25f;// animation time 
    }

    public override float Die()
    {
        // TODO :: launch Die animation
        return 0.25f;// animation time 
    }

    public override float ReceiveDamage()
    {
        // TODO :: lauch ReceiveDamage animation
        return 0.25f; // animation time 
    }
}