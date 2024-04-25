using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SuspectState : IState
{
    Transform target;
    NavMeshAgent agent;
    Guard guard;
    GuardStateMachine stateMachine;
    Func<Transform, bool> isIlluminate;

    public SuspectState(Guard guard, GuardStateMachine stateMachine) {
        this.target = guard.target;
        this.agent = guard.Agent;
        this.guard = guard;
        this.stateMachine = stateMachine;
        isIlluminate = guard.target.GetComponent<Illuminate>().CastLight;
    }

    public void Exit() {
        Debug.Log("Exit SuspectState");
    }

    public void Peform() {
        agent.SetDestination(target.position);
        if (guard.SeeTarget) {
            stateMachine.TransitionTo(stateMachine.huntState);
        } else if (!isIlluminate(guard.transform)) { 
            stateMachine.TransitionTo(stateMachine.patrolState);
        }
    }

    public void Enter() {
        Debug.Log("Enter SuspectState");
        guard.Torch.color = Color.blue;
    }
}
