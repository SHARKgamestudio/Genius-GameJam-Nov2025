using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        GameManager.Instance.playerManager.GetSystem<PlayerStats>(out PlayerStats playerStats);

        text.text = $"Health : {Mathf.RoundToInt(playerStats.life)}\n" +
            $"Max Health : {Mathf.RoundToInt(playerStats.maxLife)}\n" +
            $"Strength : {Mathf.RoundToInt(playerStats.strength)}\n" +
            $"Defense : {Mathf.RoundToInt(playerStats.defense)}\n" +
            $"Agility : {Mathf.RoundToInt(playerStats.agility)}\n" +
            $"Luck : {Mathf.RoundToInt(playerStats.luck * 100)}";
    }
}