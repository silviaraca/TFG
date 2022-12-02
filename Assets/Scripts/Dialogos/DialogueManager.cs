using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> frases;
    public bool dialogueEnd;

    // Start is called before the first frame update
    void Start()
    {
        frases = new Queue<string>();
        dialogueEnd = false;
    }

    public string StartDialogue(Dialogue dialogue){
        frases = new Queue<string>();
        dialogueEnd = false;
        Debug.Log("Starting conversation with " + dialogue.name);

        frases.Clear();

        foreach(string sentence in dialogue.sentences){
            frases.Enqueue(sentence);
        }

        return DisplayNextSentence();
    }

    public string DisplayNextSentence(){
        if(frases.Count == 0){
            EndDialogue();
            return "";
        }

        string sentence = frases.Dequeue();
        Debug.Log(sentence);
        return sentence;
    }

    void EndDialogue(){
        Debug.Log("End of conversation.");
        dialogueEnd = true;
    }

}