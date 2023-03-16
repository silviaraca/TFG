using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TomatoCollect : MonoBehaviour
{
   public TextMeshProUGUI textoE;
   private bool recogible;
   private Player player;
   public Cargar cargar;
   public CollectableType type;

   public Collectable col;

   private void Start(){
        textoE.gameObject.SetActive(false);
        //type = CollectableType.CARD;
   }

   private void Update(){
        if(recogible && Input.GetKeyDown(KeyCode.E))
            if(player.inventory.Add(col))
                Destroy(this.gameObject);
   }
   
   private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
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

 