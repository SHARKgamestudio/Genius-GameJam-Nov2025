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

    public void TestApplyEffectsToFloat()
    {
        
    }

    public float ApplyEffectTo(float input, PlaceholderStatType affectedStat)
    {
        float result = input;
        foreach (var pact in pactStack)
        {
            foreach (var effect in pact.effects)
            {
                if(affectedStat == effect.affectedStat)
                {
                    if(effect.effectType == PlaceholderEffectType.Buff)
                    {
                        if(effect.affectType == PlaceholderStatAffectType.Flat)
                        {
                            result += effect.value;
                        }
                        else
                        {
                            result = result * 1 + (effect.value / 100.0f);
                        }
                    }
                    else
                    {
                        if (effect.affectType == PlaceholderStatAffectType.Flat)
                        {
                            result -= effect.value;
                        }
                        else
                        {
                            result = result * 1 - (effect.value / 100.0f);
                        }
                    }
                }
            }
        }

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