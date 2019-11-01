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

    void Update() {
        if (Input.GetKeyDown("space")) {
            dialogue_script.display_next_sentence();
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        dialogue_script.start_dialogue(dialogue);
    }

    void OnTriggerExit2D(Collider2D collider) {
        dialogue_script.end_dialogue();
    }
}
