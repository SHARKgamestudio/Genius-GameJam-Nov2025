using System.Collections.Generic;
using UnityEngine;

public class PlayerPactStack : MonoBehaviour
{
    public List<PactData> pactStack;

    public bool HasPact<T>() where T : Pact
    {
        bool hasPact = false;
        foreach (var pact in pactStack)
        {
            T casted = pact.type as T;
            if (casted != null)
            {
                hasPact = true;
                break;
            }
        }
        return hasPact;
    }

    float ApplyEffectTo(float input, PlaceholderStatType affectedStat)
    {
        //foreach (var pact in pactStack)
        //{
        //    foreach (var buff)
        //    {

        //    }
        //}

        return 0;
    }

    // TEMP //
    public static PlayerPactStack Instance;
    void Awake()
    {
        Instance = this;
    }
    //////////
}