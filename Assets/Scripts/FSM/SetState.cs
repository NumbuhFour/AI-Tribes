using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace FSM {
	[AddComponentMenu("Scripts/FSM/Set State")]
	public class SetState : MonoBehaviour {
		
		[Serializable] public class Foo : SerializableDictionary<string, Species.States> {
			public Foo() { defKey = ""; }
		}
		public Foo stateAssignments;
		
		private Species species;
		public void Start(){
			species = this.GetComponent<Species>();
		}
		
		private void OnFStateEnter(string stateName){
			if(stateAssignments.ContainsKey(stateName))
				species.state = stateAssignments[stateName];
		}
	}
}