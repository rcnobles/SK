using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_InteractionControl : MonoBehaviour {
	public GameObject currentObject = null;
	public GameObject[] inventory = new GameObject[5];
	private int itemCount = 0;

	void Update(){
		if (Input.GetButtonDown ("Interact") && currentObject) {
			currentObject.SendMessage ("Interact", this);
			Debug.Log ("h");
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("InteractableObject")){
			currentObject = other.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (currentObject == other.gameObject) {
			currentObject = null;
		}
	}

	public void AddInventory(){
		inventory[itemCount] = currentObject;
		itemCount++;
		Debug.Log ("r");
	}
}
