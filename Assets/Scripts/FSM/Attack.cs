using UnityEngine;
using System.Collections;
namespace FSM {
	[AddComponentMenu("Scripts/FSM/Attack")]
	public class Attack : FSMBehaviour {

		protected override void OnUpdate(){
			GameObject target = this.GetComponent<Species>().targetObject;
			if(target != null){
				//TODO substitute 5 with strength var
				target.SendMessage("TakeDamage", 5, SendMessageOptions.RequireReceiver);
			}
		}
	}
}
