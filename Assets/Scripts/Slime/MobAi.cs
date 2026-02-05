using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using KnightAdventure.Utils;

public class MobAi : MonoBehaviour
{
    [SerializeField] private State StartState;
    [SerializeField] private float MaxDistance;
    [SerializeField] private float MinDistance;
    [SerializeField] private float MaxTimePatrull;

    private NavMeshAgent agent;
    private State state;
    private float currentTimePatrull;
    private Vector3 PatrullPos;
    private Vector3 startPos;

    private enum State
    {
        Idle,
        Patroll
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        state = StartState;
    }

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.Idle:
                break;
            case State.Patroll:
                currentTimePatrull -= Time.deltaTime;

                if (currentTimePatrull < 0)
                {
                    Patrull();
                    currentTimePatrull = MaxTimePatrull;
                }

                break;
        }
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
