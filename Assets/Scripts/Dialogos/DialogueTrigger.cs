using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public Dialogue dialogue;
    private bool cont;

    public int index;
    public string sentence;
//Se está intentando xd
    public IEnumerator TriggerDialogue(){
        DialogueManager man = FindObjectOfType<DialogueManager>();
        nameText.text = dialogue.name;
        dialogueText.text = "";
        index = 0;
        sentence = man.StartDialogue(dialogue);

        foreach (char letter in sentence.ToCharArray()){
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.2f);
        }
        while(!man.dialogueEnd){
            if(Input.GetKeyDown(KeyCode.E)){
                dialogueText.text = "";
                sentence = man.DisplayNextSentence();
                foreach (char letter in sentence){   
                    dialogueText.text += letter;
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }
    }
}

