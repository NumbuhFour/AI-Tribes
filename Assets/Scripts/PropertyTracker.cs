using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PropertyTracker : MonoBehaviour {
	
	private Dictionary<string, object> data = new Dictionary<string, object>();
	
    void Start(){
        if(this['strength'] == null) this['strength'] = 0;
        if(this['attackSpeed'] == null) this['attackSpeed'] = 1000; //Milliseconds
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
				this.transform.parent.BroadcastMessage("ObjectDeath", this, SendMessageOptions.DontRequireReceiver);
                other.GetComponent<Species>().enabled = false;
                other.GetComponent<Role>().enabled = false;
                other.GetComponent<Movement>().enabled = false;
                foreach(Transform obj in transform)
                    Object.Destroy(obj.gameObject);
                other.gameObject.GetComponent<Renderer>().enabled = false;
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
