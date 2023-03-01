using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CambioDirecto : MonoBehaviour
{
   private bool usable;
   private Player player;
   public Cargar cargar;
   [SerializeField] private string escena;

   private void Start(){
       
   }

   private void Update(){
        if(usable)
            cargar.load(escena);
   }
   
   private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){

            usable = true; 
        }                   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
            usable = false;
        }            
    }
}
