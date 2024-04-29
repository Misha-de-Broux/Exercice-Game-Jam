using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class BaitedState : IState {
    GameObject target;
    NavMeshAgent agent;
    Guard guard;
    GuardStateMachine stateMachine;

    public BaitedState(Guard guard, GuardStateMachine stateMachine) {
        this.agent = guard.Agent;
        this.guard = guard;
        this.stateMachine = stateMachine;
    }

    public void Exit() {
        Debug.Log("Exit SuspectState"); 
        guard.Torch.range *= 2;
        guard.Torch.spotAngle *= 2;
        guard.Torch.innerSpotAngle *= 2;
    }

    public void Peform() {
        if (guard.SeeTarget) {
            stateMachine.TransitionTo(stateMachine.huntState);
        } else if (target != null) {
            agent.SetDestination(target.transform.position);
            if (IsAtDestination) {
                guard.DestroyBait();
            }
        } else {
            stateMachine.TransitionTo(stateMachine.patrolState);
        }
    }

    public void Enter() {
        Debug.Log("Enter SuspectState");
        target = guard.BaitedBy;
        guard.Torch.color = Color.magenta;
        guard.Torch.range /= 2;
        guard.Torch.innerSpotAngle /= 2;
        guard.Torch.spotAngle /= 2;
    }

    private bool IsAtDestination {
        get { return agent.remainingDistance <= agent.stoppingDistance; }
    }
}
