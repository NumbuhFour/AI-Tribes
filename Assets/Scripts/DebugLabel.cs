using UnityEngine;
using System.Collections;

public class DebugLabel : MonoBehaviour {
	
	public string message = "";
	
	public void SetDebugMessage(string message){
		this.message = message;
	}
	//Not get/set because I dont think sendmessage handles those
	public string GetMessage(){ return message; }
	
	
}
