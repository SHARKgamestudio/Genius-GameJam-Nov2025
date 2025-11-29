using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Slider healthbar;
    [SerializeField] Slider rechargebar;

    PlayerStats stats;

    void Start()
    {
        GameManager.Instance.playerManager.GetSystem<PlayerStats>(out stats);

        healthbar.value = 0;
        rechargebar.value = 0;
    }

    void Update()
    {
        healthbar.value = stats.life / stats.maxLife;

        float actual = GameManager.Instance.fightingManager.playerPriority.ATB;
        float max = GameManager.Instance.fightingManager.playerPriority.maxSpeed;

        rechargebar.value = actual / max;
    }
}