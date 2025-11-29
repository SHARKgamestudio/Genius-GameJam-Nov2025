using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] Slider healthbar;
    [SerializeField] Slider rechargebar;

    EnemyStats stats;
    float maxLife;

    void Start()
    {
        stats = GetComponent<EnemyStats>();

        maxLife = stats.life;

        healthbar.value = 0;
        rechargebar.value = 0;
    }

    void Update()
    {
        healthbar.value = stats.life / maxLife;

        float actual = GameManager.Instance.fightingManager.enemyPriority.ATB;
        float max = GameManager.Instance.fightingManager.enemyPriority.maxSpeed;

        rechargebar.value = actual / max;
    }
}