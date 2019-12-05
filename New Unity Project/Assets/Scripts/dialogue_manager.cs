using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogue_manager : MonoBehaviour {

	public Text name_text;
	public Text dialogue_text;

	public Animator animator;

	public Queue<string> sentences;
    private bool sentence_done;
    private string current_sentence;


	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
	}

	public void start_dialogue(Dialogue dialogue, Player_InteractionControl player, dialogue_trigger trigger) {
		sentences.Clear();
        sentence_done = true;

        animator.SetBool("is_open", true);

		name_text.text = dialogue.name;

		foreach (string sentence in dialogue.sentences) {
			sentences.Enqueue(sentence);
		}

		display_next_sentence(player, trigger);
	}

	public void display_next_sentence(Player_InteractionControl player, dialogue_trigger trigger) {
        Debug.Log("chess");
        if (sentence_done && sentences.Count != 0) {
            sentence_done = false;
            current_sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(type_sentence(current_sentence));
        }
        else if (sentences.Count == 0 && sentence_done) {
            end_dialogue(player, trigger);
        }
        else if (!sentence_done){
            StopAllCoroutines();
            dialogue_text.text = current_sentence;
            sentence_done = true;
        }

    }

    IEnumerator type_sentence (string sentence) {
		dialogue_text.text = "";
		foreach (char letter in sentence.ToCharArray()) {
			dialogue_text.text += letter;
			yield return null;
		}
        sentence_done = true;
	}

	public void end_dialogue(Player_InteractionControl player, dialogue_trigger trigger) {
		animator.SetBool("is_open", false);
        player.movementEnabled = true;
        player.dialogueActive = false;
        trigger.FlipConversationStart();
	}
}
