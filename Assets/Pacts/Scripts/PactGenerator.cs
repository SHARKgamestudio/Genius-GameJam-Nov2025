using UnityEngine;

public class PactGenerator : MonoBehaviour
{
    [SerializeField] Pact[] pacts;
    [SerializeField] PactUI pactUI;

    public const int PACTSET_LENGTH = 3;

    PlayerMover mover;

    [ContextMenu("Test GeneratePacts")]
    public void ChoosePact()
    {
        PactData[] generated = GeneratePacts();
        pactUI.GenerateCards(generated, (PactData pact) =>
        {
            PlayerPactStack.Instance.pactStack.Add(pact);
            GameManager.Instance.explorationManager.WalkTowardsNextDoor();
        });
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

    public PactData[] GeneratePacts()
    {
        PactData[] result = new PactData[PACTSET_LENGTH];

        int[] chosenIndexes = new int[PACTSET_LENGTH];
        int chosenCount = 0;

        for (int i = 0; i < PACTSET_LENGTH; i++)
        {
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
                data.value = Random.Range(effect.minValue, effect.maxValue);

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
                data.value = Random.Range(effect.minValue, effect.maxValue);

                pactData.effects[buffCount + j] = data;
            }

            result[i] = pactData;
        }

        return result;
    }

    private int ChooseWeightedIndexAvoidDuplicates(Pact[] array, int[] used, int usedCount)
    {
        // Compute total weight of *unpicked* items
        float total = 0f;
        for (int i = 0; i < array.Length; i++)
        {
            if (!IsUsed(i, used, usedCount))
                total += array[i].rng;
        }

        float rand = Random.value * total;

        // Run weighted selection
        for (int i = 0; i < array.Length; i++)
        {
            if (IsUsed(i, used, usedCount))
                continue;

            rand -= array[i].rng;
            if (rand <= 0f)
                return i;
        }

        // Fallback
        for (int i = array.Length - 1; i >= 0; i--)
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