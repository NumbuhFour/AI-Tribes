using UnityEngine;
using System.Collections;

namespace FSM {
	[AddComponentMenu("")]
	[RequireComponent(typeof(Animator))]
	public class FSMBehaviour : MonoBehaviour {
		
		private bool running = false;
		
		protected virtual void OnBegin(){
			
		}
		
		protected virtual void OnUpdate(){
			
		}
		
		protected virtual void OnEnd(){
			
		}
		
		
		protected virtual void Update(){
			if(this.running) OnUpdate();
		}
		
		
		
		[SerializeField]
		private string activatorState;
		public string ActivatorState{
			get { return activatorState; }
			set { activatorState = value; ResplitActivators(); }
		}
		
		private string[] activators;
		
		private void Awake(){
			ResplitActivators();
		}
		
		private void ResplitActivators(){
			activators = activatorState.Split(',');
		}
		
		private bool IsActivator(string stateName){
			if(activatorState.Trim().ToLower() == "any") return true;
			foreach ( string s in activators ) 
				if(s == stateName) return true;
			return false;
		}
		
		private void OnFStateEnter(string stateName){
			if(IsActivator(stateName) && !running) {
				running = true;
				OnBegin();
			}
		}
		
		private void OnFStateUpdate(string stateName){
			//if(IsActivator(stateName)) OnUpdate();
		}
		
		private void OnFStateExit(string stateName){
			if(IsActivator(stateName) && activatorState.Trim().ToLower() != "any") {
				running = false;
				OnEnd();
			}
		}
	}
}
