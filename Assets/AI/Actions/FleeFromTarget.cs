using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class FleeFromTarget : RAINAction
{
	public Expression chicken;
	private string potato;

	public FleeFromTarget(){
		actionName = "Bitch";
	}

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
		potato = chicken.Evaluate(ai.DeltaTime, ai.WorkingMemory).GetValue<string>();
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}