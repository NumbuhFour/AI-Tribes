using UnityEngine;
using System.Collections;

namespace FSM {
	[AddComponentMenu("Scripts/FSM/Eat Berries")]
	public class EatBerries : FSMBehaviour {
		
		public string targetProperty = "FoodFound";
		
		protected override void OnEnd(){
			GameObject target = this.GetComponent<Species>().targetObject;
			if(target != null){
				target.SendMessage("EatBerries", SendMessageOptions.RequireReceiver);
				this.GetComponent<Species>().targetObject = null;
				this.GetComponent<Animator>().SetBool(targetProperty,false);
			}
		}
	}
}
