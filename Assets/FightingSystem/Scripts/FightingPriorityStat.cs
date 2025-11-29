using UnityEngine;
using System;
public struct FightingPriorityStat
{
    public double ATB;
    public double speed;
    public double maxSpeed;
    double tickSpeed;

    public bool IncrementATBByNTicks(int ticks)
    {
        ATB += tickSpeed * ticks * Time.deltaTime;
        return ATB >= maxSpeed; 
    }

    public void DecreamentATB()
    {
        if (ATB >= maxSpeed)
            ATB -= maxSpeed;
    }

    private void CalculateTickSpeed()
    {
        tickSpeed = speed / 100.0;
    }

    public void InitPriority(int agility)
    {
        speed = Math.Log(agility, 2.0);
        ATB = 0.0f;
        CalculateTickSpeed();
    }
}
