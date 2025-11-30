using System;
using UnityEngine;
using UnityEngine.UI;

public class EffectItem : MonoBehaviour
{
    [SerializeField] Text effectTitleText;
    [SerializeField] Text effectTypeText;
    [SerializeField] Text effectValueText;

    public void Initialize(EffectData effect, PlaceholderEffectType type)
    {
        effectTitleText.text = effect.affectedStat.ToString();
        effectTypeText.text = type == PlaceholderEffectType.Buff ? "+" : "-";
        effectValueText.text = Mathf.RoundToInt(effect.value).ToString() + (effect.affectType == PlaceholderStatAffectType.Percent ? "%" : "");
    }
}