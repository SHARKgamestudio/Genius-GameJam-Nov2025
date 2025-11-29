using UnityEngine;
using System;
public struct FightingPriorityStat
{
    public double ATB;
    public double speed;
    public double maxSpeed;
    double tickSpeed;

    public int GetTicksToOneATB()
    {
        int ticks = 0;
        double tempATB = ATB;
        while (tempATB < maxSpeed)
        {
            ticks++;
            tempATB += tickSpeed;
        }
        return ticks;
    }

    public void IncrementATBByNTicks(int ticks)
    {
        ATB += tickSpeed * ticks;
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
