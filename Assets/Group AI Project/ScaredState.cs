using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredState : State
{
    public ScaredState(StateController stateController) : base(stateController) { }

    public override void CheckTransitions()
    {
    }
    public override void Act()
    {
        stateController.ai.agent.stoppingDistance = 100;
    }
    public override void OnStateEnter()
    {
        stateController.ChangeColor(Color.blue);
        stateController.ai.agent.speed = 10f;
    }
}

