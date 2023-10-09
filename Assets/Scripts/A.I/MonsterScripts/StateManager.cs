using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public State CurrentState;
    void Update()
    {
        
    }

    private void RunStateMachine()
    {
        State nextState = CurrentState?.RunCurrentState();

        if (nextState != null)
        {
            //switch to the next state
            SwitchToTheNextState(nextState);
        }
    }

    private void SwitchToTheNextState(State nextState)
    {

    }
}
