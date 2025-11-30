using UnityEngine;


public class FightingEnemy : FightingScript
{
    protected override void AddAttackAnimationToQueue()
    {
        GameObject currentRoom = GameManager.Instance.explorationManager.GetRoom();
        Vector3 targetPositon = GameManager.Instance.playerManager.transform.position;
        Transform selfTransform = (currentRoom.GetComponent<Room>() as CombatRoom).GetEnemy().transform;
        Vector3 currentPosition = selfTransform.position;
        queue.EnqueueAnimation(new AnimationItem(() => MoveOverTime(selfTransform, targetPositon, attackTime * 0.5f)));
        queue.EnqueueAnimation(new AnimationItem(() => MoveOverTime(selfTransform, currentPosition, attackTime * 0.5f)));
    }
    protected override void AddReceiveDamageAnimationToQueue()
    {
        GameObject currentRoom = GameManager.Instance.explorationManager.GetRoom();
        SpriteRenderer selfSprite = (currentRoom.GetComponent<Room>() as CombatRoom).GetEnemy().GetComponentInChildren<SpriteRenderer>();
        Color currentColor = selfSprite.color;
        queue.EnqueueAnimation(new AnimationItem(() => ChangeColor(selfSprite, Color.darkRed, receiveDamageTime * 0.8f)));
        queue.EnqueueAnimation(new AnimationItem(() => ChangeColor(selfSprite, currentColor, receiveDamageTime * 0.2f)));
    }
    protected override void AddDieAnimationToQueue()
    {
        GameObject currentRoom = GameManager.Instance.explorationManager.GetRoom();
        SpriteRenderer selfSprite = (currentRoom.GetComponent<Room>() as CombatRoom).GetEnemy().GetComponentInChildren<SpriteRenderer>();
        queue.EnqueueAnimation(new AnimationItem(() => BlinkSprite(selfSprite, dieTime, dieTime * 0.05f)));
    }
}
