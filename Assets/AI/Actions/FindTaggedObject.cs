using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

namespace BT {
	[RAINAction]
	public class FindTaggedObject : RAINAction
	{
		[SerializeField]
		public string tag;

	    public override void Start(RAIN.Core.AI ai)
	    {
	        base.Start(ai);
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
}