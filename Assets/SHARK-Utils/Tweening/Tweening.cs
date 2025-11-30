using UnityEngine;
using System;

public abstract class Tweening : MonoBehaviour
{
    const float ERR_MARGIN = 0.001f;

    [Header("Setup")]
    [SerializeField] protected AnimationCurve inCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] protected AnimationCurve outCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField, SliderOnly(0, 2)] protected float intensity = 1;
    [SerializeField, SliderOnly(0, 10)] protected float speed = 4;

    // Events
    public Action OnInBegin;
    public Action OnInEnd;
    public Action OnOutBegin;
    public Action OnOutEnd;

    enum Mode { Idle, In, Out, Stay }
    Mode mode = Mode.Idle;

    enum StayPhase { None, In, Out }
    StayPhase stayPhase = StayPhase.None;

    bool stayRequested;
    protected float time;
    protected abstract void Tween(float value);

    void Update()
    {
        switch (mode)
        {
            case Mode.In:
                if (time <= ERR_MARGIN) OnInBegin?.Invoke();
                time = Mathf.MoveTowards(time, 1f, Time.deltaTime * speed);
                Tween(inCurve.Evaluate(time) * intensity);
                if (time >= (1f - ERR_MARGIN))
                {
                    OnInEnd?.Invoke();
                    mode = Mode.Idle;
                }
                break;

            case Mode.Out:
                if (time >= (1f - ERR_MARGIN)) OnOutBegin?.Invoke();
                time = Mathf.MoveTowards(time, 0f, Time.deltaTime * speed);
                Tween(outCurve.Evaluate(time) * intensity);
                if (time <= ERR_MARGIN)
                {
                    OnOutEnd?.Invoke();
                    mode = Mode.Idle;
                }
                break;

            case Mode.Stay:
                if (stayRequested)
                {
                    // moving toward 1 using inCurve
                    if (stayPhase != StayPhase.In)
                    {
                        float currentValue = outCurve.Evaluate(time);
                        time = FindTimeForValue(inCurve, currentValue);
                        stayPhase = StayPhase.In;
                    }

                    if (time <= ERR_MARGIN) OnInBegin?.Invoke();
                    time = Mathf.MoveTowards(time, 1f, Time.deltaTime * speed);
                    Tween(inCurve.Evaluate(time) * intensity);
                    if (time >= (1f - ERR_MARGIN)) OnInEnd?.Invoke();
                }
                else
                {
                    // moving toward 0 using outCurve
                    if (stayPhase != StayPhase.Out)
                    {
                        float currentValue = inCurve.Evaluate(time);
                        time = FindTimeForValue(outCurve, currentValue);
                        stayPhase = StayPhase.Out;
                    }

                    if (time >= (1f - ERR_MARGIN)) OnOutBegin?.Invoke();
                    time = Mathf.MoveTowards(time, 0f, Time.deltaTime * speed);
                    Tween(outCurve.Evaluate(time) * intensity);
                    if (time <= ERR_MARGIN) OnOutEnd?.Invoke();
                }
                break;
        }

        stayRequested = false;
    }

    // --- Controls ---

    public void In()
    {
        if (mode == Mode.Out)
        {
            // preserve continuity
            float currentValue = outCurve.Evaluate(time);
            time = FindTimeForValue(inCurve, currentValue);
        }
        mode = Mode.In;
    }

    public void Out()
    {
        if (mode == Mode.In)
        {
            // preserve continuity
            float currentValue = inCurve.Evaluate(time);
            time = FindTimeForValue(outCurve, currentValue);
        }
        mode = Mode.Out;
    }

    /// <summary>
    /// Must be called every frame to "hold" toward 1.
    /// If not called, will auto-release toward 0.
    /// </summary>
    public void Hold()
    {
        mode = Mode.Stay;
        stayRequested = true;
    }

    // --- Utility: inverse lookup to preserve continuity ---
    float FindTimeForValue(AnimationCurve curve, float targetValue, int steps = 60)
    {
        float bestTime = 0f;
        float minDiff = float.MaxValue;

        // Approximate inverse lookup (brute force, good enough for 60fps)
        for (int i = 0; i <= steps; i++)
        {
            float t = i / (float)steps;
            float v = curve.Evaluate(t);
            float diff = Mathf.Abs(v - targetValue);
            if (diff < minDiff)
            {
                minDiff = diff;
                bestTime = t;
            }
        }

        return bestTime;
    }
}