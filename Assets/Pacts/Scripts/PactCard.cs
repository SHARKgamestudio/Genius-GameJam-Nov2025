using UnityEngine;
using UnityEngine.UI;

public class PactCard : MonoBehaviour
{
    [SerializeField] Sprite rarity_low;
    [SerializeField] Color rarity_low_color;
    [SerializeField] Sprite rarity_mid;
    [SerializeField] Color rarity_mid_color;
    [SerializeField] Sprite rarity_high;
    [SerializeField] Color rarity_high_color;
    [SerializeField] Sprite rarity_legendary;
    [SerializeField] Color rarity_legendary_color;

    [SerializeField] Text rarity_buffs_title;
    [SerializeField] Text rarity_debuffs_title;

    [SerializeField] Image background;
    [SerializeField] Text cardTitleText;
    [SerializeField] Text cardDescriptionText;
    [SerializeField] Button cardSelectButton;
    [SerializeField] GameObject buffItemsPanel;
    [SerializeField] GameObject debuffItemsPanel;

    [SerializeField] GameObject effectItem;

    Color current;

    private PactData boundPact;
    private System.Action<PactData> onClickedCallback;

    void SpawnEffectItem(EffectData effect, PlaceholderEffectType type)
    {
        GameObject newEffect = Instantiate(effectItem, (type == PlaceholderEffectType.Buff ? buffItemsPanel.transform : debuffItemsPanel.transform));
        EffectItem newEffectItem = newEffect.GetComponent<EffectItem>();
        newEffectItem.Initialize(effect, type, current);
    }

    public void Initialize(PactData pact, System.Action<PactData> onClicked)
    {
        boundPact = pact;
        onClickedCallback = onClicked;

        cardTitleText.text = pact.type.name;
        cardDescriptionText.text = pact.type.description;

        Text[] texts = cardSelectButton.GetComponentsInChildren<Text>();

        if (pact.type.rng == 0.75f)
        {
            background.sprite = rarity_low;
            cardTitleText.color = rarity_low_color;
            cardDescriptionText.color = rarity_low_color;

            rarity_buffs_title.color = rarity_low_color;
            rarity_debuffs_title.color = rarity_low_color;

            foreach (var text in texts)
            {
                text.color = rarity_low_color;
            }

            current = rarity_low_color;
        }
        else if (pact.type.rng == 0.5f)
        {
            background.sprite = rarity_mid;
            cardTitleText.color = rarity_mid_color;
            cardDescriptionText.color = rarity_mid_color;

            rarity_buffs_title.color = rarity_mid_color;
            rarity_debuffs_title.color = rarity_mid_color;

            foreach (var text in texts)
            {
                text.color = rarity_mid_color;
            }

            current = rarity_mid_color;
        }
        else if (pact.type.rng == 0.25f)
        {
            background.sprite = rarity_high;
            cardTitleText.color = rarity_high_color;
            cardDescriptionText.color = rarity_high_color;

            rarity_buffs_title.color = rarity_high_color;
            rarity_debuffs_title.color = rarity_high_color;

            foreach (var text in texts)
            {
                text.color = rarity_high_color;
            }

            current = rarity_high_color;
        }
        else if (pact.type.rng == 0.125f)
        {
            background.sprite = rarity_legendary;
            cardTitleText.color = rarity_legendary_color;
            cardDescriptionText.color = rarity_legendary_color;

            rarity_buffs_title.color = rarity_legendary_color;
            rarity_debuffs_title.color = rarity_legendary_color;

            foreach (var text in texts)
            {
                text.color = rarity_legendary_color;
            }

            current = rarity_legendary_color;
        }

        foreach (var effect in pact.effects)
        {
            SpawnEffectItem(effect, effect.effectType);

            //if(effect.affectedStat == PlaceholderStatType.Custom)
            //{
            //    boundPact.unique = true;
            //}
        }

        // Register click event
        cardSelectButton.onClick.RemoveAllListeners();
        cardSelectButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        onClickedCallback?.Invoke(boundPact);
    }
}