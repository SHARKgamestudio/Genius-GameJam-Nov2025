using UnityEngine;
public class FightingPlayerScript : MonoBehaviour
{   
    public void Attack()
    {
        // TODO launch attck anim
    }

    public int GetAttackFrame()
    {
        //TODO Add Animation
        return 10;
    }

    public void Die()
    {
        // TODO launch die anim
        GameManager.Instance.GameOver();
    }

    public int GetDieFrame()
    {
        // TODO Add Animation
        return 10;
    }
}
