using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_PuzzleListener : MonoBehaviour {
	private bool activated = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Interact () {
		if (!activated)
			GetComponent<SpriteRenderer> ().color = new Color (0, 0, 0, 1);
		else
			GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
		activated = !activated;
	}
}
