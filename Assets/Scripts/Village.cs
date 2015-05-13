using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class Village : MonoBehaviour {

	public float range;
	public float food;
	public float childFoodCost;
	public float breedingTime;
	public float elapsedTime;
	public float totalElapsedTime;
	public float iterationTime;
	public GameObject hunter;
	public GameObject gatherer;

	public List<Data> data;
	public List<Data> newData;
	public List<GameObject> newObjectData;

	public struct Data{
		public float strength;
		public float speed;
		public float foodGain;
		public int foodCost;
		public bool hunter;
	}

	// Use this for initialization
	void Start () {
		food = 20;
		data = new List<Data>();
		newData = new List<Data>();
		newObjectData = new List<GameObject>();
		ReadFromFile();
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		totalElapsedTime += Time.deltaTime;
		if (totalElapsedTime > iterationTime){
			UpdateData();
			SaveToFile();
			totalElapsedTime = 0;
		}
		if (elapsedTime > breedingTime && food > childFoodCost){
			float num = UnityEngine.Random.Range(0.0f, 1.0f); 
			float rotation = UnityEngine.Random.Range(0.0f, 360.0f);
			float strength = UnityEngine.Random.Range(2, 4);
			float speed = UnityEngine.Random.Range(2, 4);
			//bool type = CalculateDecision(strength, speed);
			GameObject obj = (GameObject)Instantiate(num < 0.5f ? gatherer : hunter, transform.position, Quaternion.AngleAxis(rotation, Vector3.up));
			//GameObject obj = (GameObject)Instantiate(type ? hunter : gatherer, transform.position, Quaternion.AngleAxis(rotation, Vector3.up));

			obj.GetComponent<Species>().strength = strength;
			obj.GetComponent<Movement>().speedMult = speed/3;

			/*Data d = new Data();
			//d.hunter = type;
			d.foodCost = (int)childFoodCost;
			d.strength = strength;
			d.speed = speed;

			newObjectData.Add(obj);
			newData.Add(d);
			*/
			obj.GetComponent<Species>().foodCost = (int)childFoodCost;
			food -= obj.GetComponent<Species>().foodCost;
			elapsedTime = 0;
	
			range *= 1.25f;

		}
	}

	public void OnTriggerEnter(Collider collider){
		Human human = collider.gameObject.GetComponent<Human>();
		if (human != null){
			food += Convert.ToSingle(human.prop["food"]);
			for (int i = 0; i < newData.Count; i++){
				if (newObjectData[i] == collider.gameObject){
					Data d = new Data();
					d.strength = newData[i].strength;
					d.speed = newData[i].speed;
					d.foodGain = newData[i].foodGain + Convert.ToSingle(human.prop["food"]);
					d.foodCost = newData[i].foodCost;
					newData.Insert(i, d);
					newData.RemoveAt(i+1);
					break;
				}
			}
			human.prop["food"] = 0;
			human.UpdateDecision();
		}
	}

	public bool CalculateDecision(float strength, float speed){
		int total = data.Count;

		float strongHunters = 0.1f;
		float strongGatherers = 0.1f;
		float weakHunters = 0.1f;
		float weakGatherers = 0.1f;
		float fastHunters = 0.1f;
		float fastGatherers = 0.1f;
		float slowHunters = 0.1f;
		float slowGatherers = 0.1f;
		float hunters = 0.1f;
		float gatherers = 0.1f;

		for (int i = 0; i < total; i++){
			if (data[i].strength >= 3){
				if (data[i].hunter)
					strongHunters++;
				else
					strongGatherers++;
			}
			if (data[i].strength < 3){
				if (data[i].hunter) 
					weakHunters++;
				else
					weakGatherers++;
			}
			if (data[i].speed >= 3){
				if (data[i].hunter)
					fastHunters++;
				else
					fastGatherers++;
			}
			if (data[i].speed < 3){
				if (data[i].hunter)
					slowHunters++;
				else
					slowGatherers++;
			}
			if (data[i].hunter)
				hunters++;
			else
				gatherers++;
		}
		float hunter = 0;
		float gatherer = 0;
		if (strength >= 3){
			if (speed >= 3){
				hunter = strongHunters / hunters * fastHunters / hunters * hunters / total;
				gatherer = strongGatherers / gatherers * fastGatherers / gatherers * gatherers / total;
			}
			else{
				hunter = strongHunters / hunters * slowHunters / hunters * hunters / total;
				gatherer = strongGatherers / gatherers * slowGatherers / gatherers * gatherers / total;
			}
		}
		else{
			if (speed >= 3){
				hunter = weakHunters / hunters * fastHunters / hunters * hunters / total;
				gatherer = weakGatherers / gatherers * fastGatherers / gatherers * gatherers / total;
			}
			else{
				hunter = weakHunters / hunters * slowHunters / hunters * hunters / total;
				gatherer = weakGatherers / gatherers * slowGatherers / gatherers * gatherers / total;
			}
		}
		float split = hunter + gatherer;
		hunter /= split;
		gatherer /= split;
		if (hunter > gatherer){
			return true;
		}
		return false;
	}

	public void UpdateData(){
		for (int i = 0; i < newData.Count; i++){
			if (newData[i].foodGain < newData[i].foodCost){
				Data d = new Data();
				d.strength = newData[i].strength;
				d.speed = newData[i].speed;
				d.hunter = !newData[i].hunter;
				data.Add(d);
			}
			else{
				data.Add(newData[i]);
			}
		}
		newData = new List<Data>();
		newObjectData = new List<GameObject>();
	}

	public void SaveToFile(){
		try{
			using (StreamWriter sw = new StreamWriter(Application.dataPath + "/" + "data.txt")){
				foreach(Data d in data){
					sw.WriteLine(Mathf.Floor(d.strength) + " " + Mathf.Floor(d.speed) + " " + (d.hunter ? "True" : "False"));
				}
			}

		} catch
		{
			Debug.Log("Problem writing to file.");
		}
	}

	public void ReadFromFile(){
		try {
			using (StreamReader rdr = new StreamReader (Application.dataPath + "/" + "data.txt"))
			{
				string lineBuf = null;
				while ((lineBuf = rdr.ReadLine ()) != null)
				{
					string[] lineAra = lineBuf.Split (' ');
					
					// Map strings to correct data types for conditions & action
					// and Add the observation to List obsTab
					Data d = new Data();
					d.strength = float.Parse(lineAra[0]);
					d.speed = float.Parse(lineAra[1]);
					d.hunter = lineAra[2] == "True" ? true : false;
					data.Add(d);
				}
			}
		} catch
		{
			Debug.Log ("Problem reading and/or parsing observation file");
			//Environment.Exit (-1);
		}
	}
}
