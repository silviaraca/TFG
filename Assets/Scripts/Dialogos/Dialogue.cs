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
    private Player player;
    [SerializeField] private Sprite personajeImage = null;


    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        if(activeE)textoE.gameObject.SetActive(true);
        else textoE.gameObject.SetActive(false);

        if(activeE && Input.GetKeyDown(KeyCode.E) && currentScene.name != "EstrategiaTuto"){
            gameObject.SetActive(true);
            textoE.gameObject.SetActive(false);
            panel.gameObject.SetActive(true);
            move.allowMove = false;
            StartDialogue(); 
            textName.text = names[index];
            activeE = false;
            dialogue = true;
        }
        else if (currentScene.name == "EstrategiaTuto" && activeE && playerExistente.getActivo().Equals(this.gameObject.name)){
            gameObject.SetActive(true);
            textoE.gameObject.SetActive(false);
            panel.gameObject.SetActive(true);
            StartDialogue(); 
            textName.text = names[index];
            activeE = false;
        }
        if(dialogue && Input.GetKeyDown(KeyCode.E) && playerExistente.getActivo().Equals(this.gameObject.name))
        {
            
            if(textComponent.text == sentences[index])
            {
                NextLine();
            }
            else
            {
                //textName.text = names[index];
                StopAllCoroutines();
                pnj.gameObject.SetActive(false);
                textComponent.text = sentences[index];
            }
        }
        if(dialogue && textName.text != string.Empty && textName.text != "Me" && personajeImage != null){
            pnj.sprite = personajeImage;
            pnj.gameObject.SetActive(true);
        }
        else if (dialogue && textName.text != string.Empty && personajeImage != null){
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

    void NextLine()
    {
        if(index < sentences.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            textName.text = names[index];
            StartCoroutine(TypeLine());
        }
        else
        {
            panel.gameObject.SetActive(false);
            if(currentScene.name != "EstrategiaTuto")
                move.allowMove = true;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
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
}
