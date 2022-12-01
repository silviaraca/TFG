using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue(){
        DialogueManager man = FindObjectOfType<DialogueManager>();
        man.StartDialogue(dialogue);
        while(!man.dialogueEnd){
            if(Input.GetKeyDown(KeyCode.E)){
                man.DisplayNextSentence();
            }
        }
        return;
    }
}
