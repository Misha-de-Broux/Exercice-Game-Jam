using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class HuntState : IState
{
    Transform target;
    NavMeshAgent agent;
    float timeToLoseTarget = 5f;
    float timeSinceTargetSeen;
    Guard guard;
    GuardStateMachine stateMachine;

    public HuntState(Guard guard, GuardStateMachine stateMachine) {
        this.target = guard.target;
        this.agent = guard.Agent;
        this.timeToLoseTarget = guard.timeToLoseTarget;
        this.guard = guard;
        this.stateMachine = stateMachine;
        timeSinceTargetSeen = 0;
    }

    public void Exit() {
        Debug.Log("Exit HuntState");
    }

    public void Peform() {
        agent.SetDestination(target.position);
        if (guard.SeeTarget) {
            timeSinceTargetSeen = 0;
        } else {
            timeSinceTargetSeen += Time.deltaTime;
            if(timeSinceTargetSeen > timeToLoseTarget) {
                stateMachine.TransitionTo(stateMachine.patrolState);
            }
        }
    }

    public void Enter() {
        Debug.Log("Enter HuntState");
        timeSinceTargetSeen = 0;
        guard.Torch.color = Color.red;
    }
}
