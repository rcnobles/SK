using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Object_NPC : dialogue_trigger {

    public Dialogue reaction_dialogue;

    public GameObject[] listeners;

    public bool reaction = false;

    public override void Interact(Player_InteractionControl player) {
        if (!reaction) {
            if (!has_conversation_started) {
                dialogue_script.start_dialogue(dialogue, player, this);
                player.movementEnabled = false;
                player.dialogueActive = true;
                has_conversation_started = true;
            }
            else {
                dialogue_script.display_next_sentence(player, this);
                if (!player.dialogueActive) {
                    if (listeners.Length != 0) {
                        for (int i = 0; i < listeners.Length; i++) { 
                            listeners[i].SetActive(true);
                        }
                    }
                }
            }
        }


        else {
            if (!has_conversation_started) {
                dialogue_script.start_dialogue(reaction_dialogue, player, this);
                player.movementEnabled = false;
                player.dialogueActive = true;
                has_conversation_started = true;
            }
            else {
                dialogue_script.display_next_sentence(player, this);
                if (!player.dialogueActive) {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                }
            }
        }
    }
}