using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : State
{


    public WanderState(StateController stateController) : base(stateController) { }

    float timeLimit;
    float timer;
    public override void CheckTransitions()
    {
        if (stateController.CheckIfInRange("Player"))
        {
            stateController.SetState(new ChaseState(stateController));
        }
        if (timer > timeLimit)
        {
            stateController.SetState(new MakeNavPoints(stateController));
        }

    }
    public override void Act()
    {
        timer += Time.deltaTime;
        if (stateController.destination == null || stateController.ai.DestinationReached())
        {
            stateController.destination = stateController.GetWanderPoint();
            stateController.ai.SetTarget(stateController.destination);
        }
    }
    public override void OnStateEnter()
    {
        timer = 0f;
        timeLimit = 5f;
        stateController.madeContact = false;
        stateController.destination = stateController.GetWanderPoint();
        if (stateController.ai.agent != null)
        {
            stateController.ai.agent.speed = .2f;
        }
        stateController.ai.SetTarget(stateController.destination);
        stateController.ChangeColor(Color.cyan);
    }
}
