using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private MobAi mobAi; 
    [SerializeField] private int damageAmount = 10; 
    [SerializeField] private float attackCooldown = 1.0f; 

    private float lastAttackTime;

    private void Start()
    {
        if (mobAi != null)
        {
            mobAi.OnEnemyAttack += MobAi_OnEnemyAttack;
        }
    }

    private void OnDestroy()
    {
        if (mobAi != null)
        {
            mobAi.OnEnemyAttack -= MobAi_OnEnemyAttack;
        }
    }

    private void MobAi_OnEnemyAttack(object sender, System.EventArgs e)
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
         
            if (Player.Instance != null)
            {
                PlayerHealth health = Player.Instance.GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeDamage(damageAmount);
                    lastAttackTime = Time.time;
                }
            }
        }
    }
}
