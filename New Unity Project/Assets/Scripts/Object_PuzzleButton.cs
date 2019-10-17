using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_PuzzleButton : MonoBehaviour {
	public GameObject listener;
	private bool activated = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Interact(){
		if (!activated) {
			listener.SendMessage ("Interact");
			activated = true;
		}
	}
}
