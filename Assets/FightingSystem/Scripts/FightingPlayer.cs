using UnityEngine;
using static UnityEngine.GraphicsBuffer;
public class FightingPlayer : FightingScript
{   
    protected override void AddAttackAnimationToQueue()
    {
        GameObject currentRoom = GameManager.Instance.explorationManager.GetRoom();
        Vector3 targetPositon = (currentRoom.GetComponent<Room>() as CombatRoom).GetEnemy().transform.position;
        Transform selfTransform = GameManager.Instance.playerManager.transform;
        Vector3 currentPosition = selfTransform.position;
        queue.EnqueueAnimation(new AnimationItem(() => MoveOverTime(selfTransform, targetPositon, attackTime * 0.5f)));
        queue.EnqueueAnimation(new AnimationItem(() => MoveOverTime(selfTransform, currentPosition, attackTime * 0.5f)));
    }

    protected override void AddReceiveDamageAnimationToQueue()
    {
        SpriteRenderer selfSprite = GameManager.Instance.playerManager.GetComponent<SpriteRenderer>();
        Color currentColor = selfSprite.color;
        queue.EnqueueAnimation(new AnimationItem(() => ChangeColor(selfSprite, Color.darkRed, receiveDamageTime * 0.8f)));
        queue.EnqueueAnimation(new AnimationItem(() => ChangeColor(selfSprite, currentColor, receiveDamageTime * 0.2f)));
    }

    protected override void AddDieAnimationToQueue()
    {
        SpriteRenderer selfSprite = GameManager.Instance.playerManager.GetComponent<SpriteRenderer>();
        queue.EnqueueAnimation(new AnimationItem(() => BlinkSprite(selfSprite, dieTime, 10.0f)));
    }

}