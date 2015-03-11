using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PropertyTracker : MonoBehaviour {
	
	private Dictionary<string, object> data = new Dictionary<string, object>();
	
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
				Destroy(this.gameObject);
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
