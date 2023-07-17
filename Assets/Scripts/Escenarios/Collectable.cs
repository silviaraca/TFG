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
    private bool recogible;
    private Player player;
    public CollectableType type;
    public Sprite icon;

    private void Start(){
        textoE.gameObject.SetActive(false);
        //type = CollectableType.CARD;
   }

   private void Update(){
        if(recogible && Input.GetKeyDown(KeyCode.E))
        {

            if(type == CollectableType.CARD)
            {
                player.inventory.Add(this);
            }
            Destroy(this.gameObject);
        }
       
               
   }
   
   private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(player){
            textoE.gameObject.SetActive(true);
            recogible = true; 
           //player.inventory.Add(this);
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

   

