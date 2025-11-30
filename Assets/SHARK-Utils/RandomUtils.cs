using System.Collections.Generic;
using UnityEngine;

public class RandomUtils : MonoBehaviour
{
    public static int weightedRandomRange(int minimum, int maximum, float luck, bool weightMaximum, int detail = 100)
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
                weight = (int)Mathf.Round((multipliedLuck) * (i) / size + (detail - multipliedLuck) * (size - i) / size);
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

    public static float weightedRandomRangeFloat(float minimum, float maximum, float luck, bool weightMaximum, int floatPrecision = 2, int detail = 100)
    {
        
        int multiplier = (int)Mathf.Pow(10, floatPrecision);
        minimum = multiplier * minimum;
        maximum = multiplier * maximum;
        int size = (int)(maximum - minimum);

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
                weight = (int)Mathf.Round((multipliedLuck) * (i) / size + (detail - multipliedLuck) * (size - i) / size);
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
                return (float)(minimum + i) / (float)multiplier;
            }
        }

        return (float)(minimum) / (float)multiplier;
    }
}
