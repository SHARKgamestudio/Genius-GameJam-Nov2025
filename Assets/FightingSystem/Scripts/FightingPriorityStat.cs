using UnityEngine;
using System;
public struct FightingPriorityStat
{
    public double ATB;
    public double speed;
    public double maxSpeed;

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
        speed = Math.Log(agility, 2.0);
        ATB = 0.0f;
    }
}
