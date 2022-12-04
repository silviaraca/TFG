using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Talk : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI textoE;
   [SerializeField] private TextMeshProUGUI textoNombre;
   [SerializeField] private TextMeshProUGUI textoChat;
   [SerializeField] private TestChat test;
   [SerializeField] private Dialogue d;
   [SerializeField] private Image panel;
   [SerializeField] private Movement move;
   private bool activeE;
   private Player player;
    private bool finFrase;
    private int index;
   

   private void Start(){
        panel.gameObject.SetActive(false);
        textoE.gameObject.SetActive(false);
        activeE = false;
        finFrase = true;
        index = 0;
   }

   private void Update(){
        
        if(activeE && Input.GetKeyDown(KeyCode.E)){
            panel.gameObject.SetActive(true);
            textoE.gameObject.SetActive(false);
            move.allowMove = false;
            textoNombre.text = d.name;
            if(Input.GetKeyDown(KeyCode.E)  && index <= d.sentences.Length){
                if(index != d.sentences.Length)
                    test.Run(d.sentences[index], textoChat);
                index++;
            }
            if (index > d.sentences.Length){
                panel.gameObject.SetActive(false);
                textoE.gameObject.SetActive(true);
                move.allowMove = true;
                index = 0;
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
            textoE.gameObject.SetActive(false);
            activeE = false;
        }            
    }

    public void SetFin(bool b){
        finFrase = b;
    }
}



