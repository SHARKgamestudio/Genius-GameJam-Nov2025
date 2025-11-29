using UnityEngine;

public enum PlaceholderStatType
{
    None,
    Health,
    Strength,
    Defense,
    Agility,
    Luck
}

public enum PlaceholderStatAffectType
{
    None,
    Flat,
    Percent
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
    public PlaceholderStatAffectType affectType;
    public float value;
}

[System.Serializable]
public struct PactData
{
    public Pact type;
    public EffectData[] effects;
}

[System.Serializable]
public class Effect {
    [SerializeField] public PlaceholderStatType statType = PlaceholderStatType.Strength;
    [SerializeField] public PlaceholderStatAffectType affectType = PlaceholderStatAffectType.Percent;
    [Range(0.0f, 100.0f)]
    [SerializeField] public float minValue = 0.0f;
    [Range(0.0f, 100.0f)]
    [SerializeField] public float maxValue = 100.0f;
}

[CreateAssetMenu(fileName = "NewPact", menuName = "DungeonCrawler/Pact", order = 1)]
public class Pact : ScriptableObject
{
    [SerializeField] public new string name = "NewPact";
    [SerializeField] public string description = "Some Description";

    [Range (0f, 1f)]
    [SerializeField] public float rng = 0.25f;

    [SerializeField] public Effect[] buffs;
    [SerializeField] public Effect[] debuffs;
}