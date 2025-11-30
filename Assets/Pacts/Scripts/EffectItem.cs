using System;
using UnityEngine;
using UnityEngine.UI;

public class EffectItem : MonoBehaviour
{
    [SerializeField] Text effectTitleText;
    [SerializeField] Text effectTypeText;
    [SerializeField] Text effectValueText;

    public void Initialize(EffectData effect, PlaceholderEffectType type, Color textColor)
    {
        effectTitleText.color = textColor;
        effectTypeText.color = textColor;
        effectValueText.color = textColor;

        if (effect.affectedStat == PlaceholderStatType.Custom)
        {
            effectTitleText.text = effect.type.customDesc;
            effectTypeText.text = "";
            effectValueText.text = "";
        }
        else
        {
            effectTitleText.text = effect.affectedStat.ToString();
            effectTypeText.text = type == PlaceholderEffectType.Buff ? "+" : "-";
            effectValueText.text = Mathf.Round(effect.value).ToString() + (effect.affectType == PlaceholderStatAffectType.Percent ? "%" : "");
        }
    }
}