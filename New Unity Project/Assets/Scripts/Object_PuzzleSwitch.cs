using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_PuzzleSwitch : MonoBehaviour {
	public GameObject listener;
	private bool activated = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Interact () {
		listener.SendMessage ("Interact");
		activated = !activated;
	}
}
