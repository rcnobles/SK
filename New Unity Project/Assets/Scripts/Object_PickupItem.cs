using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_PickupItem : MonoBehaviour{
	public string itemName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Interact(Player_InteractionControl player) {
		player.AddInventory();
		gameObject.SetActive (false);
	}
}
