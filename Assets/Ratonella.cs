using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ratonella : MonoBehaviour
{

    public Player player;
    public TextMeshProUGUI textoE;
    public Dialogue dialogueScript1;
    public Dialogue dialogueScript2; 
    public Dialogue dialogueScript3;
    private bool activeE;
    public static bool machine;
    private string ratData;

    void Start()
    {
        ratData = "";
        if(PlayerPrefs.HasKey("RatSecretary")) ratData = PlayerPrefs.GetString("RatSecretary");
    }

    void Update()
    {
        //Si aún no ha cogido el café pero has hablado con la rata
        if(ratData == "done" && !machine)
        {
            dialogueScript1.enabled = false;
            dialogueScript2.enabled = true;
            dialogueScript3.enabled = false;
            if(activeE && Input.GetKeyDown(KeyCode.E))
            {
                string activa = "done";
                PlayerPrefs.SetString("ActivaMachine", activa);
                PlayerPrefs.Save();
                activeE = false;
            }
        }

        //Si ha cogido el café
        if(ratData == "done" && machine)
        {
            dialogueScript1.enabled = false;
            dialogueScript2.enabled = false;
            dialogueScript3.enabled = true;
            if(activeE && Input.GetKeyDown(KeyCode.E))
            {
                string activa = "done";
                PlayerPrefs.SetString("CompletaRat", activa);
                PlayerPrefs.Save();
                activeE = false;
            }
        }

        //Inicio
        if(ratData != "done")
        {
            dialogueScript1.enabled = true;
            dialogueScript2.enabled = false;
            dialogueScript3.enabled = false;
        }
        if(PlayerPrefs.HasKey("Machine")) machine = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
            activeE = true; 
        }                   
    }
}
