using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Collect : MonoBehaviour
{
   public TextMeshProUGUI textoE;
   private bool recogible;
   private Player player;
   //public CollectableType type;

   private void Start(){
        textoE.gameObject.SetActive(false);
        //type = CollectableType.CARD;
   }

   private void Update(){
        if(recogible && Input.GetKeyDown(KeyCode.E))
        {
            Jabs.num++;
            Destroy(this.gameObject);
        }
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

 