using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Machine : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI textoE;
    private bool activeE;
    private string ratData;
    public Dialogue dialogueScript;

    void Start()
    {
        ratData = PlayerPrefs.GetString("RatSecretary");
       dialogueScript.enabled = false;
    }

    void Update()
    {
        if(ratData == "done")
        {
            dialogueScript.enabled = true;
            if(activeE && Input.GetKeyDown(KeyCode.E))
            {
                Ratonella.machine = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
            textoE.gameObject.SetActive(true);
            activeE = true; 
        }                   
    }
}
