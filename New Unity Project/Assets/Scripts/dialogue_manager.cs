using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogue_manager : MonoBehaviour {

	public Text name_text;
	public Text dialogue_text;

	public Animator animator;

	public Queue<string> sentences;


	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
	}

	public void start_dialogue(Dialogue dialogue) {
		sentences.Clear();

		animator.SetBool("is_open", true);

		name_text.text = dialogue.name;

		foreach (string sentence in dialogue.sentences) {
			sentences.Enqueue(sentence);
		}

		display_next_sentence();
	}

	public void display_next_sentence() {
		if (sentences.Count == 0) {
			end_dialogue();
			return;
		}

		string sentence = sentences.Dequeue();

		StopAllCoroutines();
		StartCoroutine(type_sentence(sentence));
	}

	IEnumerator type_sentence (string sentence) {
		dialogue_text.text = "";
		foreach (char letter in sentence.ToCharArray()) {
			dialogue_text.text += letter;
			yield return null;
		}
	}

	public void end_dialogue() {
		animator.SetBool("is_open", false);
	}
}
