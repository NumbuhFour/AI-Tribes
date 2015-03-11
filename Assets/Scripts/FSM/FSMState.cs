using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

namespace FSM {
	public class FSMState : StateMachineBehaviour  {
	
		public string stateName;
	
		override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
			animator.gameObject.SendMessage("OnFStateEnter",stateName, SendMessageOptions.RequireReceiver);
		}
		
		override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
			animator.gameObject.SendMessage("OnFStateUpdate",stateName, SendMessageOptions.RequireReceiver);
		}
		
		
		override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
			animator.gameObject.SendMessage("OnFStateExit",stateName, SendMessageOptions.RequireReceiver);
		}
	}

}