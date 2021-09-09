using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TranscendState : State {

    public TranscendState(StateController stateController) : base(stateController) { }
    private Transform target;
    private Vector3 targetPoint;

    public override void CheckTransitions()
    {
      if (!stateController.CheckIfInRange("Player"))
      {
          stateController.SetState(new WanderState(stateController));
      }
    }

    public override void Act()
    {
      stateController.ai.agent.baseOffset += .01f;
    }

    public override void OnStateEnter()
    {
        stateController.ChangeColor(Color.white);
        //stateController.ai.GetComponent<Rigidbody>().useGravity = false;
        //stateController.ai.GetComponent<Collider>().enabled = false;
        stateController.destination = stateController.GetTranscendPoint();
    }


}
