using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State {


    public PatrolState(StateController stateController) : base(stateController) { }
   
    public override void CheckTransitions()
    {
        if (stateController.CheckIfInRange("Player"))
        {
            stateController.SetState(new ChaseState(stateController));
        }
     
     if (stateController.DoneWithNavPatrol())
        {
            GameObject newAI = GameObject.Instantiate(stateController.self);
            stateController.SetState(new WanderState(stateController));
        }

        
    }
    public override void Act()
    {
        if(stateController.destination == null || stateController.ai.DestinationReached())
        {
            stateController.destination = stateController.GetNextNavPoint();
            stateController.ai.SetTarget(stateController.destination);
        }
    }
    public override void OnStateEnter()
    {
        stateController.destination = stateController.GetNextNavPoint();
        if (stateController.ai.agent != null)
        {
            stateController.ai.agent.speed = 1f;
        }
        stateController.ai.SetTarget(stateController.destination);
        stateController.ChangeColor(Color.blue);
    }

}
