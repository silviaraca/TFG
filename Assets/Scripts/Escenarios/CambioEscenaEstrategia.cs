using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

public class CambioEscenaEstrategia : MonoBehaviour
{
   public TextMeshProUGUI textoE;
   private bool usable;
   private Player player;
   public Cargar cargar;
   [SerializeField] private string escena;
   [SerializeField] private List<string> deck;
   [SerializeField] private List<string> enemies;

   private void Start(){
        textoE.gameObject.SetActive(false);
   }

   private void Update(){
        if(usable && Input.GetKeyDown(KeyCode.E))
            cargar.load("Estrategia");
   }
   public void cargaEstrategia(){
          string deckData = JsonConvert.SerializeObject(deck); //Hacer que lo tome mejor del player
          PlayerPrefs.SetString("DeckData", deckData);
          PlayerPrefs.Save();
          string enemyData = JsonConvert.SerializeObject(enemies); //Esto vale así porque lo toma del npc en concreto ya que cambio estrategia está dentro del NPC
          PlayerPrefs.SetString("EnemiesData", enemyData);
          PlayerPrefs.Save();
          cargar.load("Estrategia");
     }
}    