using UnityEngine;
using UnityEngine.UI;

public class PactCard : MonoBehaviour
{
    [SerializeField] Text cardTitleText;
    [SerializeField] Text cardDescriptionText;
    [SerializeField] Button cardSelectButton;
    [SerializeField] GameObject buffItemsPanel;
    [SerializeField] GameObject debuffItemsPanel;

    [SerializeField] GameObject effectItem;

    private PactData boundPact;
    private System.Action<PactData> onClickedCallback;

    void SpawnEffectItem(EffectData effect, PlaceholderEffectType type)
    {
        GameObject newEffect = Instantiate(effectItem, (type == PlaceholderEffectType.Buff ? buffItemsPanel.transform : debuffItemsPanel.transform));
        EffectItem newEffectItem = newEffect.GetComponent<EffectItem>();
        newEffectItem.Initialize(effect, type);
    }

    public void Initialize(PactData pact, System.Action<PactData> onClicked)
    {
        boundPact = pact;
        onClickedCallback = onClicked;

        cardTitleText.text = pact.type.name;
        cardDescriptionText.text = pact.type.description;

        foreach (var effect in pact.effects)
        {
            SpawnEffectItem(effect, effect.effectType);

            if (effect.affectedStat == PlaceholderStatType.Custom)
            {
                boundPact.unique = true;
            }
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