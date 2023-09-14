using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoE;
    [SerializeField] private Image panel;
    [SerializeField] private Image pnj;
    [SerializeField] private Movement move;
    [SerializeField] private Player playerExistente;
    [SerializeField] private GameObject npcHablando;
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI textName;
    public string[] sentences;
    public string[] names;
    public float textSpeed;
    private Scene currentScene;
    private int index;
    private bool activeE, dialogue = false;
    private static bool activo = false;
    public bool apagado = false;
    private Player player;
    [SerializeField] private Sprite personajeImage = null;


    void Start()
    {
        activeE = false;
        panel.gameObject.SetActive(false);
        textComponent.text = string.Empty;
        textName.text = string.Empty;
        currentScene = SceneManager.GetActiveScene ();
        pnj.gameObject.SetActive(false);
        textSpeed = 0.03f;
        dialogue = false;

        if(npcHablando.GetComponent<Image>() != null)
            personajeImage = npcHablando.GetComponent<Image>().sprite;
        
    }

    void Update()
    {
        //Activar el diálogo
        if((activeE && Input.GetKeyDown(KeyCode.E) && currentScene.name != "EstrategiaTuto") || (activeE && activo)){
            gameObject.SetActive(true);
            textoE.gameObject.SetActive(false);
            panel.gameObject.SetActive(true);
            move.allowMove = false;
            StartDialogue(); 
            textName.text = names[index];
            activeE = false;
            dialogue = true;
            activo = false;
        }
        //Desactivar el diálogo
        else if (currentScene.name == "EstrategiaTuto" && activeE && playerExistente.getActivo().Equals(this.gameObject.name)){
            gameObject.SetActive(true);
            textoE.gameObject.SetActive(false);
            panel.gameObject.SetActive(true);
            StartDialogue(); 
            textName.text = names[index];
            activeE = false;
        }

        //Continuar texto
        if(dialogue && Input.GetKeyDown(KeyCode.E) && playerExistente.getActivo().Equals(this.gameObject.name))
        {
            
            if(textComponent.text == sentences[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                pnj.gameObject.SetActive(false);
                textComponent.text = sentences[index];
            }
        }

        //Sprites
        if(dialogue && textName.text != string.Empty && textName.text != "Me" && personajeImage != null){
            pnj.sprite = personajeImage;
            pnj.gameObject.SetActive(true);
        }
        else if (dialogue && textName.text != string.Empty && personajeImage != null){
            pnj.gameObject.SetActive(false);
        }
    }

    void NextLine()
    { 
        //Continuar con el texto
        if(index < sentences.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            textName.text = names[index];
            StartCoroutine(TypeLine());
        }
        //Cerrar diálogo
        else
        {
            if(currentScene.name != "EstrategiaTuto"){
                index++;
                panel.gameObject.SetActive(false);
                move.allowMove = true;
            }
            else{
                GameManagerE gm = FindObjectOfType<GameManagerE>();
                gm.finDialogo();
            }

            textComponent.text = string.Empty;
            textName.text = string.Empty;
            dialogue = false;
            pnj.gameObject.SetActive(false);
        }
    }


    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in sentences[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player") && !apagado){
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

    public bool indexFin(){
        return index >= names.Length;
    }

    public void activa(){
        activo = true;
    }

    public Player getPlayerExistente(){
        return playerExistente;
    }

    public void desactivaDialogo(){
        apagado = true;
        this.enabled = false;
    }
}
