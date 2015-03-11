using UnityEngine;
using System.Collections;


namespace FSM {
	[AddComponentMenu("Scripts/FSM/Wander")]
	public class Wander : FSMBehaviour {
		
		private Species species;
		private Movement move;
		
		private Vector3 roamGoal;
		private float wait = 0;
		private float giveUp = 0;
		protected override void OnBegin(){
			if(species == null) species = GetComponent<Species>();
			if(move == null) move = GetComponent<Movement>();
			wait = 0;
			giveUp = 0;
			roamGoal = Roam();
		}
		
		protected override void OnUpdate(){
			if(species.IsWithinReach(roamGoal) || giveUp <= 0){
				roamGoal = Roam();
				wait = Random.Range(0,Random.Range(0,5));
				giveUp = Random.Range(0,10);
			}else if(wait > 0){
				wait -= Time.deltaTime;
			}else{
				move.PathTo(roamGoal);
				giveUp -= Time.deltaTime;
			}
		}
		
		public Vector3 Roam(){
			Vector3 targetDir = Quaternion.AngleAxis(Random.Range(-45,45), this.transform.up)*this.transform.forward * Random.Range(70,200);
			return transform.position + targetDir;
		}
	}
}
