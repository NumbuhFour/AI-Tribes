using UnityEngine;
using System.Collections;

namespace FSM {
	[AddComponentMenu("Scripts/FSM/Attack")]
	public class Attack : FSMBehaviour {
		
		private Species species;
		private Movement move;
        private PropertyTracker myProp;
		
		private Vector3 roamGoal;
		private float delay = 0;
		protected override void OnBegin(){
			if(species == null) species = GetComponent<Species>();
			if(move == null) move = GetComponent<Movement>();
			delay = 0;
			roamGoal = Roam();
            myProp = GetComponent<PropertyTracker>();
		}
		
		protected override void OnUpdate(){
			/*if(species.IsWithinReach(roamGoal) || giveUp <= 0){
				roamGoal = Roam();
				wait = Random.Range(0,Random.Range(0,5));
				giveUp = Random.Range(0,10);
			}else if(wait > 0){
				wait -= Time.deltaTime;
			}else{
				move.PathTo(roamGoal);
				giveUp -= Time.deltaTime;
			}*/
            
            if(delay <= 0){
                GameObject target = species.targetObject;
                PropertyTracker prop = target.GetComponent<PropertyTracker>();
                int my = Random.Range(0,(int)myProp["strength"]);
				int opp = Random.Range(0,(int)prop["strength"]);
                
                if(my > opp){
					prop["health"] = (int)prop["health"] - (int)myProp["strength"];
                }
                delay = (float)myProp["attackSpeed"];

            }else {
                delay -= Time.deltaTime;
            }
		}
		
		public Vector3 Roam(){
			Vector3 targetDir = Quaternion.AngleAxis(Random.Range(-45,45), this.transform.up)*this.transform.forward * Random.Range(70,200);
			return transform.position + targetDir;
		}
	}
}
