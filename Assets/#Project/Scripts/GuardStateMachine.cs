using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class GuardStateMachine {
    public IState CurrentState { get; private set; }

    public HuntState huntState;
    public PatrolState patrolState;
    public SuspectState suspectState;

    public GuardStateMachine(Guard guard) {
        huntState = new HuntState(guard, this);
        patrolState = new PatrolState(guard, this);
        suspectState = new SuspectState(guard, this);
    }

    public void Init(IState startingState) {
        CurrentState = startingState;
        CurrentState.Enter();
    }
    public void Perform() {
        CurrentState?.Peform();
    }

    public void TransitionTo(IState nextState) {
        CurrentState?.Exit();
        CurrentState = nextState;
        CurrentState.Enter();
    }
}
