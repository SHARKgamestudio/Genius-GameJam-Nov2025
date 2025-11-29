using UnityEngine;
using System;
public struct FightingPriorityStat
{
    public float ATB;
    public float speed;
    public float maxSpeed;

    public FightingPriorityStat(float ATB = 0.0f, float speed = 0.0f, float maxSpeed = 1.0f)
    {
        this.ATB = ATB;
        this.speed = speed;
        this.maxSpeed = maxSpeed;
    }

    public bool IncrementATBByNTicks(int ticks)
    {
        ATB += speed * ticks * Time.deltaTime;
        return ATB >= maxSpeed; 
    }

    public void DecreamentATB()
    {
        if (ATB >= maxSpeed)
            ATB -= maxSpeed;
    }

    public void InitPriority(float agility)
    {
        speed = MathF.Log(agility, 2.0f);
        ATB = 0.0f;
    }
}