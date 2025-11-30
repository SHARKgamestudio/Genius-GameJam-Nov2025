using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class EnemyStats : Stats
{
    [SerializeField] int maxFactorValue;
    public int totalRandomizedValues; // 0 to 200
    public float roomLuck = 0;

    public override void TakeDamage(float amount)
    {
        amount -= defense;
        base.TakeDamage(amount);
    }

    float ScalingFunction(int floorNumber)
    {
        return (Mathf.Sqrt(floorNumber) / 2.5f) + 1f;
    }

    public void ScaleStats(int floorNumber)
    {
        PlayerStats stats;
        GameManager.Instance.playerManager.GetSystem<PlayerStats>(out stats);
        float playerLuck = stats.luck * 100;
        float totalLuck = Mathf.Min(roomLuck + playerLuck, 100);

        totalRandomizedValues = 0;
        int randLifeFactor = RandomUtils.weightedRandomRange(0, maxFactorValue, totalLuck, true);
        totalRandomizedValues += randLifeFactor;
        int randStrengthFactor = RandomUtils.weightedRandomRange(0, maxFactorValue, totalLuck, true);
        totalRandomizedValues += randStrengthFactor;
        int randDefenseFactor = RandomUtils.weightedRandomRange(0, maxFactorValue, totalLuck, true);
        totalRandomizedValues += randDefenseFactor;
        int randAgilityFactor = RandomUtils.weightedRandomRange(0, maxFactorValue, totalLuck, true);
        totalRandomizedValues += randAgilityFactor;
        life *= (1.0f + (randLifeFactor - maxFactorValue / 2f) / 100f) * ScalingFunction(floorNumber);
        strength *= (1.0f + (randStrengthFactor - maxFactorValue / 2f) / 100f) * ScalingFunction(floorNumber);
        defense *= (1.0f + (randDefenseFactor - maxFactorValue / 2f) / 100f) * ScalingFunction(floorNumber);
        agility *= (1.0f + (randAgilityFactor - maxFactorValue / 2f) / 100f) * ScalingFunction(floorNumber);
    }
}
