using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class CambioEscenaEstrategia : MonoBehaviour
{
   public TextMeshProUGUI textoE;
   private bool usable;
   private Player player;
   public Cargar cargar;
   [SerializeField] private string escena;
   [SerializeField] private List<string> enemies;

   private void Start(){
        //textoE.gameObject.SetActive(false);
   }

   private void Update(){
        if(usable && Input.GetKeyDown(KeyCode.E))
            cargar.load("Estrategia");
   }
   public void cargaEstrategia(){
          string thisScene = SceneManager.GetActiveScene().name;
          string enemyData = JsonConvert.SerializeObject(enemies); //Esto vale así porque lo toma del npc en concreto ya que cambio estrategia está dentro del NPC
          PlayerPrefs.SetString("EnemiesData", enemyData);
          PlayerPrefs.Save();
          string escenaRPG = JsonConvert.SerializeObject(thisScene);
          PlayerPrefs.SetString("EscenaRPG", escenaRPG);
          PlayerPrefs.Save();
          Player playerPos = FindObjectOfType<Player>();
          Vector3 vectorPos = playerPos.gameObject.transform.position;
          string posRPG = JsonUtility.ToJson(vectorPos);
          PlayerPrefs.SetString("PosicionPlayer", posRPG);
          PlayerPrefs.Save();
          if(escena == "EstrategiaTuto") cargar.load("EstrategiaTuto");
          else cargar.load("Estrategia");
     }
     public void cargaRPG(){
          string escenaRPG = PlayerPrefs.GetString("EscenaRPG");
          string escena = JsonConvert.DeserializeObject<string>(escenaRPG);
          string escena2 = escena;
            if(PlayerPrefs.HasKey(escena)) {
                escena2 = PlayerPrefs.GetString(escena);
            }
            cargar.load(escena2);
     }

     public void cargaFin(){
          Vector3 vectorPos = new Vector3(-45, -23, 0);
          string posRPG = JsonUtility.ToJson(vectorPos);
          PlayerPrefs.SetString("PosicionPlayer", posRPG);
          PlayerPrefs.Save();

          cargar.load("Office room Final");
     }
}    