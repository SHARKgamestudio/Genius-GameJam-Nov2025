using System;
using System.Collections;

public class AnimationItem
{
    public Func<IEnumerator> animationRoutine;
    public bool animationFinished { get; private set; }

    public AnimationItem(Func<IEnumerator> routine)
    {
        animationRoutine = routine;
        animationFinished = false;
    }

    public IEnumerator Animate()
    {
        yield return animationRoutine();
        animationFinished = true;
    }
}
