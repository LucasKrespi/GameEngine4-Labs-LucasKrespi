using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieComponent : MonoBehaviour
{
    public int zombieDamage = 5;

    public NavMeshAgent zombieNavMeshAgent;
    public Animator zombieAnimator;
    public ZombieStateMachine zombieStateMachine;
    public GameObject followTarget;

    public void Awake()
    {
        zombieNavMeshAgent = GetComponent<NavMeshAgent>();
        zombieAnimator = GetComponent<Animator>();
        zombieStateMachine = GetComponent<ZombieStateMachine>();
    }

    private void Start()
    {
        Initialize(followTarget);
    }

    public void Initialize(GameObject _followTarget)
    {
        //followTarget = _followTarget;

        ZombieIdleState idleState = new ZombieIdleState(this, zombieStateMachine);
        zombieStateMachine.AddState(ZombieStateType.Idling, idleState);

        ZombieDeadState deadState = new ZombieDeadState(this, zombieStateMachine);
        zombieStateMachine.AddState(ZombieStateType.Dying, deadState);

        ZombieAttackState attackState = new ZombieAttackState(_followTarget, this, zombieStateMachine);
        zombieStateMachine.AddState(ZombieStateType.Attacking, attackState);

        ZombieFollowState followState = new ZombieFollowState(_followTarget, this, zombieStateMachine);
        zombieStateMachine.AddState(ZombieStateType.Following, followState);

        zombieStateMachine.Initialize(ZombieStateType.Following);
    }


}
