using UnityEngine;

public enum PlaceholderStatType
{
    None,
    Health,
    Strength,
    Defense,
    Agility
}

public enum PlaceholderEffectType
{
    None,
    Buff,
    Debuff
}

[System.Serializable]
public struct EffectData
{
    public PlaceholderEffectType effectType;
    public PlaceholderStatType affectedStat;
    public float value;
} 

[System.Serializable]
public class Effect {
    [SerializeField] public PlaceholderStatType statType;
    [Range(0f, 1f)]
    [SerializeField] public float minValue = 0.0f;
    [Range(0.0f, 1.0f)]
    [SerializeField] public float maxValue = 1.0f;
}

[System.Serializable]
public struct PactData
{
    public Pact type;
    public EffectData[] effects;
}

[CreateAssetMenu(fileName = "NewPact", menuName = "DungeonCrawler/Pact", order = 1)]
public class Pact : ScriptableObject
{
    [SerializeField] public string name = "NewPact";
    [SerializeField] public string description = "Some Description";

    [Range (0f, 1f)]
    [SerializeField] public float rng = 0.25f;

    [SerializeField] public Effect[] buffs;
    [SerializeField] public Effect[] debuffs;
}