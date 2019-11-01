using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogue_trigger : MonoBehaviour {

    public Dialogue dialogue;

    public GameObject dialogue_manager;
    private dialogue_manager dialogue_script;

    private bool has_conversation_started = false;

    void Start() {
        dialogue_script = dialogue_manager.GetComponent<dialogue_manager>();
    }

    void Interact(Player_InteractionControl player) {
        if (!has_conversation_started) {
            dialogue_script.start_dialogue(dialogue, player, this);
            player.movementEnabled = false;
            player.dialogueActive = true;
            has_conversation_started = true;
        }
        else {
            dialogue_script.display_next_sentence(player, this);
        }
    }

    public void FlipConversationStart() {
        has_conversation_started = !has_conversation_started;
    }
}
