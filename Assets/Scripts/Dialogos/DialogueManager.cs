using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;
    private Queue<string> sentences;
    public bool dialogueEnd;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        dialogueEnd = false;
    }

    public void StartDialogue(Dialogue dialogue){
        dialogueEnd = false;
        Debug.Log("Starting conversation with " + dialogue.name);

        sentences.Clear();

        foreach(string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        if(sentences.Count == 0){
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        //dialogueText.text = sentence;
    }

    void EndDialogue(){
        Debug.Log("End of conversation.");
        dialogueEnd = true;
    }

}