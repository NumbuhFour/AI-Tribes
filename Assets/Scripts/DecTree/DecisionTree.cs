using UnityEngine;
using System.Collections;
using System.IO;

public class DecisionTree {

	public Node root;
	
	public Species.TargetAction Food;
	public Species.TargetAction Predators;
	public Species.TargetAction Return;

	public DecisionTree(string file){
		try{
			StreamReader sr = new StreamReader(file);
			root = ReadTree(sr);
			sr.Close();
			
		}
		catch(FileNotFoundException){
			Debug.Log("File not found");
		}

	}

	public Node ReadTree(StreamReader sr){
		try{
			string line = sr.ReadLine();
			string type = line.Substring(0, 1);
			string content = line.Substring(1, line.Length - 1);
			Node next = new Node();

			if (type.Equals("I")){
				next = new Node();
				next.Test = content;
				next.Yes = ReadTree (sr);
				next.No = ReadTree(sr);
			}
			else
				next.State = content;
			return next;
		}
		catch(UnityException e){

		}
		return null;
	}

	public string Run(){
		return Run (root);
	}

	public string Run(Node node){
		if (node.State != null)
			return node.State;
		if (Test (node.Test))
			return Run (node.Yes);
		return Run (node.No);
	}

	public bool Test(string t){
		switch(t){
			case "Predator":
				return Predators() != null;
				break;
			case "Food":
				return Food() != null;
				break;
			case "Return":
				return Return() != null;
				break;
		}
		return false;
	}
}

public class Node{

	public string Test;
	public string State;
	public Node Yes;
	public Node No;

	public Node(){
	}

}
