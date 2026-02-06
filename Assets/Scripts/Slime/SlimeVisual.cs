using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeVisual : MonoBehaviour
{
    [SerializeField] private MobAi mobAi;
    private Animator animator;

    private const string IS_RUNNING = "IsRunning";
    private const string CHASING_SPEED_MULTIPLAYER = "ChasingSpeedMultiplayer";


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_RUNNING, mobAi.isRunning());
        animator.SetFloat(CHASING_SPEED_MULTIPLAYER, mobAi.GetPatrollAnimationSpeed());
    }

}
