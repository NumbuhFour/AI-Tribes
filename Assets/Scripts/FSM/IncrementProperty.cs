using UnityEngine;
using System.Collections;

namespace FSM {
	[AddComponentMenu("Scripts/FSM/Increment Property")]
	public class IncrementProperty : FSMBehaviour {
		
		public string property;
		public int increment = 0;
		
		protected override void OnUpdate(){
			if(increment != 0){
				Animator anim = this.GetComponent<Animator>();
				int val = anim.GetInteger(property);
				anim.SetInteger(property,val+increment);
			}
		}
	}
}
