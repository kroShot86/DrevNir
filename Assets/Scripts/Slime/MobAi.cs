using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using KnightAdventure.Utils;
using Unity.VisualScripting;
using System;

public class MobAi : MonoBehaviour
{
    
    [SerializeField] private State StartState;
    [SerializeField] private float MaxDistance;
    [SerializeField] private float MinDistance;
    [SerializeField] private float MaxTimePatrull;

    [SerializeField] private bool isChasingEnemy = false;
    [SerializeField] private float chasingDistance = 10;
    [SerializeField] private float chasingSpeedMultiplayer = 2;

    private NavMeshAgent agent;
    private State currentState;
    private float currentTimePatrull;
    private Vector3 PatrullPos;
    private Vector3 startPos;

    private float patrollSpeed;
    private float chasingSpeed;

    [SerializeField] private bool isAttackingEnemy = false;
    private float AttackingDistance = 4;

    public event EventHandler OnEnemyAttack;

    private float attackRate = 2;
    private float nextAttackTime = 0;

    private float nextCheckDirectionTime = 0;
    private float checkDirectionDuration = 0.1f;
    private Vector3 lastPosition;

    public bool isRunning()
    {
        if (agent.velocity == Vector3.zero)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    private enum State
    {
        Idle,
        Patroll,
        Chasing,
        Attacking,
        Death
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        currentState = StartState;
        patrollSpeed = agent.speed;
        chasingSpeed = agent.speed * chasingSpeedMultiplayer;
    }

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        StateHandler();
        MovementDirectionHandler();
    }

    private void StateHandler()
    {
        switch (currentState)
        {

            case State.Patroll:
                currentTimePatrull -= Time.deltaTime;

                if (currentTimePatrull < 0)
                {
                    Patrull();
                    currentTimePatrull = MaxTimePatrull;
                }
                CheckCurrentState();
                break;

            case State.Chasing:
                ChasingTarget();
                CheckCurrentState();
                break;

            case State.Death:
                break;

            case State.Attacking:
                CheckCurrentState();
                AttackingTarget();
                break;

            default:
            case State.Idle:
                break;

        }
    }

    private void MovementDirectionHandler()
    {
        if(Time.time > nextAttackTime)
        {
            if(isRunning())
            {
                ChangeFacingDirection(lastPosition, transform.position);
            }
            else if(currentState == State.Attacking)
            {
                ChangeFacingDirection(transform.position, Player.Instance.transform.position);
            }

            lastPosition = transform.position;
            nextAttackTime = Time.time + checkDirectionDuration;
        }
    }

    public float GetPatrollAnimationSpeed()
    {
        return agent.speed / patrollSpeed;
    }

    private void AttackingTarget()
    {
        OnEnemyAttack?.Invoke(this, EventArgs.Empty);
    }

    private void CheckCurrentState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
        State newState = State.Patroll;

        if (isChasingEnemy)
        {
            if(distanceToPlayer <= chasingDistance)
            {
                newState = State.Chasing;
            }
        }

        if(isAttackingEnemy)
        {
            if(distanceToPlayer <= AttackingDistance)
            {
                newState = State.Attacking;
            }
        }

        if(newState != currentState)
        {
            if(newState == State.Chasing)
            {
                agent.ResetPath();
                agent.speed = chasingSpeed;
            }
            else if (newState == State.Patroll)
            {
                currentTimePatrull = 0;
                agent.speed = patrollSpeed;
            }
            else if(newState == State.Attacking)
            {
                agent.ResetPath();
            }

            currentState = newState;
        }
        

    }


    private void ChasingTarget()
    {
        if(Time.time > nextAttackTime)
        {
            agent.SetDestination(Player.Instance.transform.position);
            nextAttackTime = Time.time + attackRate;
        }
        
    }


    private void Patrull()
    {
        //startPos = transform.position;
        PatrullPos = GetPos();
        agent.SetDestination(PatrullPos);
        //ChangeAnima(transform.position, PatrullPos);
    }

    private Vector3 GetPos()
    {
        return (startPos + Utils.GetRandomDir() * UnityEngine.Random.Range(MinDistance, MaxDistance));
    }

    private void ChangeAnima(Vector3 currentPos, Vector3 targetPos)
    {
        if(currentPos.x > targetPos.x)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition)
    {
        if(sourcePosition.x > targetPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
