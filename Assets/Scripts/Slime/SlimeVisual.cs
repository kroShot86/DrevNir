using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeVisual : MonoBehaviour
{
    [SerializeField] private MobAi mobAi;
    [SerializeField] private EnemyEnity enemyEnity;

    private Animator animator;

    private const string IS_RUNNING = "IsRunning";
    private const string CHASING_SPEED_MULTIPLAYER = "ChasingSpeedMultiplayer";
    private const string ATTACK = "Attack";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        mobAi.OnEnemyAttack += MobAi_OnEnemyAttack;
    }

    private void MobAi_OnEnemyAttack(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ATTACK);
    }

    private void OnDestroy()
    {
        mobAi.OnEnemyAttack -= MobAi_OnEnemyAttack;
    }

    private void Update()
    {
        animator.SetBool(IS_RUNNING, mobAi.isRunning());
        animator.SetFloat(CHASING_SPEED_MULTIPLAYER, mobAi.GetPatrollAnimationSpeed());
    }
    public void TriggerOff()
    {
        enemyEnity.PolygonCollider2DTurnOff();
    }
    public void TriggerOn()
    {
        enemyEnity.PolygonCollider2DTurnOn();
    }
}
