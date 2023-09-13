using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Machine : MonoBehaviour
{
    public Player player;
    private bool activeE;
    private bool machine;
    public BoxCollider2D collider;
    public Dialogue dialogueScript;

    void Start()
    {
        dialogueScript.enabled = false;
        collider.enabled = false;
    }

    void Update()
    {
        if(PlayerPrefs.HasKey("ActivaMachine"))
        {
            dialogueScript.enabled = true;
            collider.enabled = true;
            if(activeE && Input.GetKeyDown(KeyCode.E))
            {
                string activa = "done";
                PlayerPrefs.SetString("Machine", activa);
                PlayerPrefs.Save();
                PlayerPrefs.DeleteKey("ActivaMachine");
                activeE = false;
            }
        }
        if(dialogueScript.indexFin()){
            dialogueScript.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
            activeE = true; 
        }                   
    }
}
