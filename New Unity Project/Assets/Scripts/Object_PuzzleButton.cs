using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_PuzzleButton : MonoBehaviour {
	public GameObject[] listeners;
	private bool activated = false;
	void Interact(){
		if (!activated) {
            for (int i = 0; i < listeners.Length; i++) {
                listeners[i].SendMessage("Triggered");
                activated = true;
            }
		}
	}
}
