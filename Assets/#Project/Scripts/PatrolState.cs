using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IState
{
    NavMeshAgent agent;
    Transform[] waypoints;
    int index = 0;
    Transform target;
    Guard guard;
    GuardStateMachine stateMachine;

    public PatrolState(Guard guard, GuardStateMachine stateMachine) {
        this.agent = guard.Agent;
        this.waypoints = guard.waypoints;
        this.guard = guard;
        this.stateMachine = stateMachine;
    }

    public void Exit() {
        Debug.Log("Exit PatrolState");
    }

    public void Peform() {
        if (guard.SeeTarget) {
            stateMachine.TransitionTo(stateMachine.huntState);
        }else if (IsAtDestination) {
            index++;
            SelectDestination();
        }
        agent.SetDestination(target.position);
    }

    public void Enter() {
        Debug.Log("Enter PatrolState");
        SelectDestination();
    }

    private void SelectDestination() {
        if(index == waypoints.Length) {
            index = 0;
        }
        target = waypoints[index]; 
    }

    private bool IsAtDestination {
        get { return agent.remainingDistance <= agent.stoppingDistance; }
    }
}
