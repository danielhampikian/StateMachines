using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepState : State {
    float targetDegree;

    public SleepState(StateController stateController) : base(stateController) { }

    public override void CheckTransitions()
    {
        if (stateController.sleepTimer == 2000)
        {
            stateController.SetState(new PatrolState(stateController));
        }
    }
    public override void Act()
    {
        if (targetDegree < 90)
        {
            targetDegree += .1f;
        }

        Quaternion currentRot = stateController.transform.rotation;
        Quaternion targetRot = Quaternion.Euler(targetDegree, 0, 0);
        stateController.transform.rotation = targetRot;

        //if (targetDegree == 90)
        //{
        //    targetDegree -= 1f;
        //    Quaternion currentRot = stateController.transform.rotation;
        //    Quaternion targetRot = Quaternion.Euler(targetDegree, 0, 0);
        //    stateController.transform.rotation = targetRot;
        //}



    }
    public override void OnStateEnter()
    {
        stateController.sleepTimer = 0;
        stateController.ChangeColor(Color.yellow);
        stateController.ai.agent.speed = 0.0f;
    }
}
