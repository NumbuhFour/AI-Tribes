using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PropertyTracker : MonoBehaviour {
	
	private Dictionary<string, object> data = new Dictionary<string, object>();
	
    void Start(){
        if(this["strength"] == null) this["strength"] = 0;
        if(this["attackSpeed"] == null) this["attackSpeed"] = 1000; //Milliseconds
    }
    
	[SerializeField]
	private int maxHealth;
	public int MaxHealth{
		get { return maxHealth; }
		set {
			maxHealth = value;
			Health = Mathf.Min(maxHealth, Health);
		}
	}
	
	[SerializeField]
	private int health = 100;
	public int Health{
		get { return health; }
		set {
			health = value;
			if(health <= 0) {
				gameObject.tag = GetComponent<Species>() is Human ? "HumanMeat" : "AnimalMeat";
				this.transform.parent.BroadcastMessage("ObjectDeath", this, SendMessageOptions.DontRequireReceiver);
                GetComponent<Species>().enabled = false;
                //GetComponent<Role>().enabled = false;
                GetComponent<Movement>().enabled = false;
				//Object.Destroy(gameObject);
				for (int i = 0; i < transform.childCount; i++)
					Object.Destroy(transform.GetChild(i).gameObject);
                gameObject.GetComponent<Renderer>().enabled = false;
				gameObject.AddComponent<Decay>();
			}
		}
	}
	
	public object this[string key]{
		get { 
			if(key == "health") return Health;
			if(data.ContainsKey(key))
				return data[key]; 
			else return null;
		}
		set { 
			if(key == "health") {
				Health = (int)value;
			}
			else data[key] = value; 
		}
	}
}
