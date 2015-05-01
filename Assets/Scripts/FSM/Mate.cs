using UnityEngine;
using System.Collections;

namespace FSM {
	[AddComponentMenu("Scripts/FSM/Mate")]
	public class Mate : FSMBehaviour {
		
		public string mateTimeProperty = "MateTimeDelay";
		public string mateFoundProperty = "MateFound";
		
		protected override void OnEnd(){
			GameObject target = this.GetComponent<Species>().targetObject;
			if(target != null){
				Mate m = target.GetComponent<Mate>();
				if(m.GetComponent<Animator>().GetInteger(mateTimeProperty) > 0){
					//Mate not suitable.
					this.GetComponent<Animator>().SetInteger(mateTimeProperty,Random.Range(1000, 2500));
				}else {
					if(this.gameObject.GetInstanceID() > target.GetInstanceID()){ //I WILL BEAR THIS CHILD
						GameObject child = Instantiate(this.gameObject);
						child.transform.position = this.transform.position;
						child.transform.SetParent(this.transform.parent);
						
						Genes mg = this.GetComponent<Genes>();
						Genes og = target.GetComponent<Genes>();
						
						Genes cg = child.GetComponent<Genes>();
						
						mg.MutateTo(og, cg);
					}
					
					this.GetComponent<Species>().targetObject = null;
					this.GetComponent<Animator>().SetInteger(mateTimeProperty,Random.Range(1000, 5000));
					this.GetComponent<Animator>().SetBool(mateFoundProperty, false);
				}
			}
		}
	}
}
