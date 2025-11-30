using System.Collections;
using UnityEngine;

public abstract class FightingScript : MonoBehaviour
{
    protected IEnumerator MoveOverTime(Transform t, Vector3 targetPosition, float duration)
    {
        Vector3 start = t.position;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            t.position = Vector3.Lerp(start, targetPosition, time / duration);
            yield return null;
        }

        t.position = targetPosition;
    }

    protected IEnumerator BlinkSprite(SpriteRenderer sprite, float duration, float frequency)
    {
        float elapsed = 0f;
        bool visible = true;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            visible = !visible;
            sprite.enabled = visible;

            yield return new WaitForSeconds(1f / (frequency * 2f));
        }

        sprite.enabled = true;
    }

    protected IEnumerator ChangeColor(SpriteRenderer sprite, Color targetColor, float duration)
    {
        Color startColor = sprite.color;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            sprite.color = Color.Lerp(startColor, targetColor, time / duration);
            yield return null;
        }

        sprite.color = targetColor;
    }



    [SerializeField] protected AnimationQueue queue;

    [SerializeField, Range(0.25f,1.0f)] protected float attackTime;
    [SerializeField, Range(0.25f,1.0f)] protected float receiveDamageTime;
    [SerializeField, Range(0.25f,1.0f)] protected float dieTime;
    protected abstract void AddAttackAnimationToQueue();
    protected abstract void AddReceiveDamageAnimationToQueue();
    protected abstract void AddDieAnimationToQueue();


    public float Attack()
    {
        AddAttackAnimationToQueue();
        return attackTime;
    }
    public float ReceiveDamage()
    {
        AddReceiveDamageAnimationToQueue();
        return receiveDamageTime;
    }
    public float Die()
    {
        AddDieAnimationToQueue();
        return dieTime;
    }
}
