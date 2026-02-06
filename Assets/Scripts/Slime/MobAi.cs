using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using KnightAdventure.Utils;
using Unity.VisualScripting;

public class MobAi : MonoBehaviour
{
    [SerializeField] private State StartState;
    [SerializeField] private float MaxDistance;
    [SerializeField] private float MinDistance;
    [SerializeField] private float MaxTimePatrull;

    [SerializeField] private bool isChasingEnemy = false;
    private float chasingDistance = 10;
    private float chasingSpeedMultiplayer = 2;

    private NavMeshAgent agent;
    private State currentState;
    private float currentTimePatrull;
    private Vector3 PatrullPos;
    private Vector3 startPos;

    private float patrollSpeed;
    private float chasingSpeed;

    

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

                break;

            case State.Chasing:
                ChasingTarget();
                CheckCurrentState();
                break;

            case State.Death:
                break;

            case State.Attacking:
                break;

            default:
            case State.Idle:
                break;

        }
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

            currentState = newState;
        }
        

    }


    private void ChasingTarget()
    {
        agent.SetDestination(Player.Instance.transform.position);
    }


    private void Patrull()
    {
        //startPos = transform.position;
        PatrullPos = GetPos();
        agent.SetDestination(PatrullPos);
        ChangeAnima(transform.position, PatrullPos);
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
}
