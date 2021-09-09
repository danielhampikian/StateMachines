using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TranscendState : State {

private bool madeContact = false;

    public TranscendState(StateController stateController) : base(stateController) { }

    public override void CheckTransitions()
    {
      if (!stateController.CheckIfInRange("Player"))
      {
        Debug.Log("leave Transcend");
          stateController.SetState(new WanderState(stateController));
      }
    }

    public override void Act()
    {
      stateController.ai.GetComponent<Rigidbody>().useGravity = false;
      stateController.ai.transform.position += Vector3.up;
    }

    public override void OnStateEnter()
    {
        stateController.ChangeColor(Color.white);
        stateController.ai.agent.speed = 0f;
    }


}
