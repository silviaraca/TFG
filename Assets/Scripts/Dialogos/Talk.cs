using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Talk : MonoBehaviour
{
    public TextMeshProUGUI E;
    public DialogueTrigger trigger;
    public Movement move;
    public Image dialoguePanel;
    private bool hablable;
    private Player player;

    private void Start()
    {
        E.gameObject.SetActive(false);
        dialoguePanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(hablable && Input.GetKeyDown(KeyCode.E)){
            dialoguePanel.gameObject.SetActive(true);
            move.allowMove = false;
            trigger.TriggerDialogue();
            //dialoguePanel.gameObject.SetActive(false);
            move.allowMove = true;
        }                
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
            E.gameObject.SetActive(true);
            hablable = true; 
        }                   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
            E.gameObject.SetActive(false);
            hablable = false;
        }            
    }
}
