using UnityEngine;

public class PactUI : MonoBehaviour
{
    [SerializeField] GameObject card;
    [SerializeField] GameObject pactSelectorPanel;

    public void ClearCards()
    {
        foreach (Transform child in pactSelectorPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void SpawnCard(PactData pact, System.Action<PactData> callback)
    {
        GameObject newCard = (GameObject)Instantiate(card, pactSelectorPanel.transform) as GameObject;
        PactCard newPactCard = newCard.GetComponent<PactCard>();
        newPactCard.Initialize(pact, (PactData pact)=>
        {
            callback.Invoke(pact);
            ClearCards();
        });
    }

    public void GenerateCards(PactData[] pacts, System.Action<PactData> onSelected)
    {
        ClearCards();
        foreach (PactData pact in pacts)
        {
            SpawnCard(pact, onSelected);
        }
    }
}