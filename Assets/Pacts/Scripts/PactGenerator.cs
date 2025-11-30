using System.Collections.Generic;
using System.Xml;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class PactGenerator : MonoBehaviour
{
    [SerializeField] List<Pact> pacts;
    [SerializeField] PactUI pactUI;

    PlayerMover mover;

    [ContextMenu("Test GeneratePacts")]

    public void ApplyPact(PactData pact)
    {
        if (pact.unique == true)
        {
            pacts.Remove(pact.type);
        }

        PlayerPactStack.Instance.pactStack.Add(pact);
        GameManager.Instance.explorationManager.WalkTowardsNextDoor();
        GameManager.Instance.playerManager.GetSystem<PlayerStats>(out PlayerStats stats);
        foreach (var effect in pact.effects)
        {
            switch (effect.affectedStat)
            {
                case PlaceholderStatType.Agility:
                    if (effect.affectType == PlaceholderStatAffectType.Percent)
                    {
                        if (effect.effectType == PlaceholderEffectType.Buff)
                        {
                            stats.AddAgilityPourcentage(effect.value / 100.0f);
                        }
                        else
                        {
                            stats.ReduceAgilityPourcentage(effect.value / 100.0f);
                        }
                    }
                    else
                    {
                        if (effect.effectType == PlaceholderEffectType.Buff)
                        {
                            stats.AddAgility(effect.value);
                        }
                        else
                        {
                            stats.ReduceAgility(effect.value);
                        }
                    }
                    break;
                case PlaceholderStatType.Strength:
                    if (effect.affectType == PlaceholderStatAffectType.Percent)
                    {
                        if (effect.effectType == PlaceholderEffectType.Buff)
                        {
                            stats.AddStrengthPourcentage(effect.value / 100.0f);
                        }
                        else
                        {
                            stats.ReduceStrengthPourcentage(effect.value / 100.0f);
                        }
                    }
                    else
                    {
                        if (effect.effectType == PlaceholderEffectType.Buff)
                        {
                            stats.AddStrength(effect.value);
                        }
                        else
                        {
                            stats.ReduceStrength(effect.value);
                        }
                    }
                    break;
                case PlaceholderStatType.Defense:
                    if (effect.affectType == PlaceholderStatAffectType.Percent)
                    {
                        if (effect.effectType == PlaceholderEffectType.Buff)
                        {
                            stats.AddDefensePourcentage(effect.value / 100.0f);
                        }
                        else
                        {
                            stats.ReduceDefensePourcentage(effect.value / 100.0f);
                        }
                    }
                    else
                    {
                        if (effect.effectType == PlaceholderEffectType.Buff)
                        {
                            stats.AddDefense(effect.value);
                        }
                        else
                        {
                            stats.ReduceDefense(effect.value);
                        }
                    }
                    break;
                case PlaceholderStatType.Health:
                    if (effect.affectType == PlaceholderStatAffectType.Percent)
                    {
                        if (effect.effectType == PlaceholderEffectType.Buff)
                        {
                            stats.AddMaxLifePourcentage(effect.value / 100.0f);
                        }
                        else
                        {
                            stats.LowerMaxLife(effect.value / 100.0f);
                        }
                    }
                    else
                    {
                        if (effect.effectType == PlaceholderEffectType.Buff)
                        {
                            stats.AddMaxLife(effect.value);
                        }
                        else
                        {
                            stats.ReduceMaxLife(effect.value);
                        }
                    }
                    break;
                case PlaceholderStatType.Luck:
                    if (effect.effectType == PlaceholderEffectType.Buff)
                    {
                        stats.AddLuck(effect.value);
                    }
                    else
                    {
                        stats.ReduceLuck(effect.value);
                    }
                    break;
                case PlaceholderStatType.None:
                    break;
            }
        }
        switch (pact.type.name)
        {
            case "Double Down":
                PlayerPactStack stack;
                GameManager.Instance.playerManager.GetSystem<PlayerPactStack>(out stack);
                List<PactData> pacts = stack.pactStack;
                foreach (PactData data in pacts)
                {
                    if (data.unique == true)
                        continue;

                    ApplyPact(data);
                }

                break;

            case "Redistribute":
                // Get score and set it to 0
                int score = GameManager.Instance.GetScore();
                GameManager.Instance.AddToScore(-score);

                List<float> thresholds = new List<float>();
                for (int i = 0; i < 4; i++)
                {
                    thresholds.Add(UnityEngine.Random.value);
                }
                thresholds.Sort();

                float lifePercent = thresholds[0];
                float strengthPercent = thresholds[1] - thresholds[0];
                float agilityPercent = thresholds[2] - thresholds[1];
                float defensePercent = thresholds[3] - thresholds[2];
                float luckPercent = thresholds[4] - thresholds[3];
                stats.AddMaxLife(lifePercent * score * (1 / 2));
                stats.AddStrength(strengthPercent * score * (1 / 10));
                stats.AddAgility(agilityPercent * score * (1 / 10));
                stats.AddDefense(defensePercent * score * (1 / 15));
                stats.AddLuck(luckPercent * score * (1 / 1000));

                break;

            case "Brambles":
                PlayerStats playerStats;
                GameManager.Instance.playerManager.GetSystem<PlayerStats>(out playerStats);
                playerStats.hasThorns = true;

                break;
        }
    }

    public void ChoosePact()
    {
        PactData[] generated = GeneratePacts();
        pactUI.GenerateCards(generated, (PactData pact) => ApplyPact(pact));
    }

    void Start()
    {
        GameManager.Instance.playerManager.GetSystem<PlayerMover>(out mover);
        mover.OnStayMovementEnd += OnShouldChoosePact;
    }

    void OnDisable()
    {
        mover.OnStayMovementEnd -= OnShouldChoosePact;
    }

    void OnShouldChoosePact()
    {
        GameObject roomObj = GameManager.Instance.explorationManager.GetRoom();
        bool isPactRoom = roomObj.TryGetComponent<PactRoom>(out PactRoom balec);
        if(isPactRoom)
        {
            ChoosePact();
        }
    }

    public PactData[] GeneratePacts(int count = 3)
    {
        PactData[] result = new PactData[count];

        int[] chosenIndexes = new int[count];
        int chosenCount = 0;

        for (int i = 0; i < count; i++)
        {
            PlayerStats stats;
            GameManager.Instance.playerManager.GetSystem<PlayerStats>(out stats);
            float playerLuck = stats.luck * 100;
            float roomLuck = GameManager.Instance.explorationManager.GetRoom().GetComponent<Room>().luckModifier * 100;
            float totalLuck = Mathf.Min(playerLuck + roomLuck, 100);

            int index = ChooseWeightedIndexAvoidDuplicates(pacts, chosenIndexes, chosenCount);
            chosenIndexes[chosenCount] = index;
            chosenCount++;

            Pact pact = pacts[index];

            PactData pactData = new PactData();
            pactData.type = pact;

            int buffCount = pact.buffs.Length;
            int debuffCount = pact.debuffs.Length;
            int totalEffects = buffCount + debuffCount;

            pactData.effects = new EffectData[totalEffects];

            // -----------------------
            // FILL BUFFS
            // -----------------------
            for (int j = 0; j < buffCount; j++)
            {
                Effect effect = pact.buffs[j];

                EffectData data = new EffectData();
                data.effectType = PlaceholderEffectType.Buff;
                data.affectedStat = effect.statType;
                data.affectType = effect.affectType;
                data.value = RandomUtils.weightedRandomRangeFloat(effect.minValue, effect.maxValue, totalLuck, true);
                //data.value = Random.Range(effect.minValue, effect.maxValue);

                pactData.effects[j] = data;
            }

            // -----------------------
            // FILL DEBUFFS
            // -----------------------
            for (int j = 0; j < debuffCount; j++)
            {
                Effect effect = pact.debuffs[j];

                EffectData data = new EffectData();
                data.effectType = PlaceholderEffectType.Debuff;
                data.affectedStat = effect.statType;
                data.affectType = effect.affectType;
                data.value = RandomUtils.weightedRandomRangeFloat(effect.minValue, effect.maxValue, totalLuck, false);
                //data.value = Random.Range(effect.minValue, effect.maxValue);

                pactData.effects[buffCount + j] = data;
            }

            result[i] = pactData;
        }

        return result;
    }

    private int ChooseWeightedIndexAvoidDuplicates(List<Pact> array, int[] used, int usedCount)
    {
        // Compute total weight of *unpicked* items
        float total = 0f;
        for (int i = 0; i < array.Count; i++)
        {
            if (!IsUsed(i, used, usedCount))
                total += array[i].rng;
        }

        float rand = UnityEngine.Random.value * total;

        // Run weighted selection
        for (int i = 0; i < array.Count; i++)
        {
            if (IsUsed(i, used, usedCount))
                continue;

            rand -= array[i].rng;
            if (rand <= 0f)
                return i;
        }

        // Fallback
        for (int i = array.Count - 1; i >= 0; i--)
            if (!IsUsed(i, used, usedCount))
                return i;

        return 0; // shouldn't happen
    }

    private bool IsUsed(int index, int[] used, int usedCount)
    {
        for (int i = 0; i < usedCount; i++)
            if (used[i] == index)
                return true;

        return false;
    }
}