using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum CollectableType{ NONE, CARD, COLLECT};

public class Collectable : MonoBehaviour
{
   
    public TextMeshProUGUI textoE;
    public CollectableType type;
    public Sprite icon;
    public AutoDialogue auto;
    private bool recogible;
    private Player player;

    private void Start(){
        textoE.gameObject.SetActive(false);
   }

   private void Update(){
        if(recogible && Input.GetKeyDown(KeyCode.E))
        {
            auto.delete();
            Destroy(this.gameObject);
        }
       
               
   }
   
   private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(player){
            textoE.gameObject.SetActive(true);
            recogible = true; 
        }                   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
            textoE.gameObject.SetActive(false);
            recogible = false;
        }            
    }
}

   

