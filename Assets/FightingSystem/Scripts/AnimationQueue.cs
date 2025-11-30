using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationQueue : MonoBehaviour
{
    private Queue<AnimationItem> queue = new Queue<AnimationItem>();
    private bool isRunning = false;

    public void EnqueueAnimation(AnimationItem anim)
    {
        queue.Enqueue(anim);
        TryStartNext();
    }

    private void TryStartNext()
    {
        if (isRunning) return;
        if (queue.Count == 0) return;

        StartCoroutine(ProcessAnimation(queue.Peek()));
    }

    private IEnumerator ProcessAnimation(AnimationItem anim)
    {
        isRunning = true;

        yield return StartCoroutine(anim.Animate());

        queue.Dequeue();
        isRunning = false;

        TryStartNext();
    }
}
