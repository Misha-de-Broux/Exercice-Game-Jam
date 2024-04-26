using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IState
{
    NavMeshAgent agent;
    Transform waypoints;
    int index = 0;
    Transform target;
    Guard guard;
    GuardStateMachine stateMachine;
    Func<bool> isIlluminate;

    public PatrolState(Guard guard, GuardStateMachine stateMachine) {
        this.agent = guard.Agent;
        this.waypoints = guard.waypoints;
        this.guard = guard;
        this.stateMachine = stateMachine;
        isIlluminate = () => guard.target.GetComponent<Illuminate>().CastLight(guard.transform);
    }

    public void Exit() {
        Debug.Log("Exit PatrolState");
    }

    public void Peform() {
        if (guard.SeeTarget) {
            stateMachine.TransitionTo(stateMachine.huntState);
        } else if (isIlluminate()) {
            stateMachine.TransitionTo(stateMachine.suspectState);
        } else if (guard.BaitedBy != null) {
            stateMachine.TransitionTo(stateMachine.baitedState);
        } else if (IsAtDestination) {
            index++;
            SelectDestination();
        }
        agent.SetDestination(target.position);
    }

    public void Enter() {
        Debug.Log("Enter PatrolState");
        SelectDestination();
        guard.Torch.color = Color.green;
    }

    private void SelectDestination() {
        if(index == waypoints.childCount) {
            index = 0;
        }
        target = waypoints.GetChild(index); 
    }

    private bool IsAtDestination {
        get { return agent.remainingDistance <= agent.stoppingDistance; }
    }
}
