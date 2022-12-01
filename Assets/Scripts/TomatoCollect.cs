using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum CollectableType{NONE, TOMATO}

public class TomatoCollect : MonoBehaviour
{
   public CollectableType type;
   public TextMeshProUGUI E;
   private bool recogible;
   private Player player;

   private void Start(){
        E.gameObject.SetActive(false);
        type = CollectableType.TOMATO;
   }

   private void Update(){
        if(recogible && Input.GetKeyDown(KeyCode.E))
            if(player.inventory.Add(type))
                Destroy(this.gameObject);
   }
   
   private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
            E.gameObject.SetActive(true);
            recogible = true; 
        }                   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
            E.gameObject.SetActive(false);
            recogible = false;
        }            
    }
}

 