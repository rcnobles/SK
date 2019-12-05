using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_PickupItem : dialogue_trigger{
    // item label
	public string itemName = "[REPLACE]";

    // item description
    public string itemDescription = "[REPLACE]";

    public Dialogue use_invalid;

    public Dialogue use_valid;

    public GameObject valid_object;

    public override void Interact(Player_InteractionControl player) {
        if (!has_conversation_started) {
            dialogue_script.start_dialogue(dialogue, player, this);
            player.movementEnabled = false;
            player.dialogueActive = true;
            has_conversation_started = true;
        }
        else {
            dialogue_script.display_next_sentence(player, this);
            if (!player.dialogueActive) {
                player.AddInventory();
                gameObject.SetActive(false);
            }
        }
	}

    public void Use(Player_InteractionControl player, GameObject currentObject) {
        if (currentObject == valid_object) {
            if (!has_conversation_started) {
                dialogue_script.start_dialogue(use_valid, player, this);
                player.movementEnabled = false;
                player.dialogueActive = true;
                has_conversation_started = true;
            }
            else {
                dialogue_script.display_next_sentence(player, this);
                if (!player.dialogueActive) {
                    player.RemoveInventory();
                    player.useActive = false;
                    player.movementEnabled = true;
                    valid_object.GetComponent<Object_NPC>().reaction = true;
                    valid_object.SendMessage("Interact", player);
                }
            }
        }
        else {
            if (!has_conversation_started) {
                dialogue_script.start_dialogue(use_invalid, player, this);
                player.movementEnabled = false;
                player.dialogueActive = true;
                has_conversation_started = true;
            }
            else {
                dialogue_script.display_next_sentence(player, this);
                if (!player.dialogueActive) {
                    player.useActive = false;
                    player.movementEnabled = true;
                }
            }
        }
    }
}
