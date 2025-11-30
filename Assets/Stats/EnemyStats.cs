using UnityEngine;
using System.Collections.Generic;

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

    int weightedRandomRange(int minimum, int maximum, float luck, bool weightMaximum, int detail = 100)
    {
        int size = maximum - minimum;
        int midSize = size / 2;

        float multipliedLuck = luck * detail / 100;

        // By default, luck is equal to 50
        Dictionary<int, int> weightedRange = new Dictionary<int, int>();
        int totalWeight = 0;

        // 55 luck would mean having the rarest element at a weight 55
        // and opposite at 45

        for (int i = 0; i < size; i++)
        {
            int weight;
            if (weightMaximum)
            {
                weight = (int)Mathf.Round((multipliedLuck) * (i) / size + (detail - multipliedLuck ) * (size - i) / size);
            }
            else
            {
                weight = (int)Mathf.Round((multipliedLuck) * (size - i) / size + (detail - multipliedLuck) * i / size);
            }
            weightedRange[i] = weight;
            totalWeight += weight;
        }

        float rand = UnityEngine.Random.value;

        int currentWeight = 0;
        for (int i = 0; i < size; i++)
        {
            currentWeight += weightedRange[i];
            if (rand < (float)currentWeight / (float)totalWeight)
            {
                return minimum + i;
            }
        }

        return -1;
    }

    float ScalingFunction(int floorNumber)
    {
        return floorNumber + floorNumber * floorNumber * 0.1f;
    }

    public void ScaleStats(int floorNumber)
    {
        PlayerStats stats;
        GameManager.Instance.playerManager.GetSystem<PlayerStats>(out stats);
        float playerLuck = stats.luck * 100;

        totalRandomizedValues = 0;
        int randLifeFactor = weightedRandomRange(0, maxFactorValue, playerLuck + roomLuck, true);
        totalRandomizedValues += randLifeFactor;
        int randStrengthFactor = weightedRandomRange(0, maxFactorValue, playerLuck + roomLuck, true);
        totalRandomizedValues += randStrengthFactor;
        int randDefenseFactor = weightedRandomRange(0, maxFactorValue, playerLuck + roomLuck, true);
        totalRandomizedValues += randDefenseFactor;
        int randAgilityFactor = weightedRandomRange(0, maxFactorValue, playerLuck + roomLuck, true);
        totalRandomizedValues += randAgilityFactor;
        life *= 1 + (randLifeFactor - maxFactorValue/2) / 100.0f * ScalingFunction(floorNumber);
        strength *= 1 + (randStrengthFactor - maxFactorValue / 2) / 100.0f * ScalingFunction(floorNumber);
        defense *= 1 + (randDefenseFactor - maxFactorValue / 2) / 100.0f * ScalingFunction(floorNumber);
        agility *= 1 + (randAgilityFactor - maxFactorValue / 2) / 100.0f * ScalingFunction(floorNumber);
    }
}
