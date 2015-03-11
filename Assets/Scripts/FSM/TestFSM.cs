using UnityEngine;
using System.Collections;

public class TestFSM : MonoBehaviour {
	
	public void TestEnterState(){
		Debug.Log(this.gameObject.name + " START STATE");
	}
	
	public void TestUpdateState(){
		Debug.Log(this.gameObject.name + " UPDATE STATE");
	}
	
	public void TestExitState(){
		Debug.Log(this.gameObject.name + " EXIT STATE");
	}
}
