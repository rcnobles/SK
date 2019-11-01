using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_PickupItem : MonoBehaviour{
    // item label
	public string itemName = "[REPLACE]";

    // item description
    public string itemDescription = "[REPLACE]";
	
	void Interact(Player_InteractionControl player) {
		player.AddInventory();
		gameObject.SetActive (false);
	}

    public void Use() {

    }
}
