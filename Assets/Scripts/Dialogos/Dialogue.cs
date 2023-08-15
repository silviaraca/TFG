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
    [SerializeField] private Movement move;
    [SerializeField] private Player playerExistente;
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI textName;
    public string[] sentences;
    public string[] names;
    public float textSpeed;
    private Scene currentScene;
    private int index;
    private bool activeE;
    private Player player;


    // Start is called before the first frame update
    void Start()
    {
        activeE = false;
        panel.gameObject.SetActive(false);
        textComponent.text = string.Empty;
        textName.text = string.Empty;
        currentScene = SceneManager.GetActiveScene ();
        textSpeed = 0.03f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activeE && Input.GetKeyDown(KeyCode.E) && currentScene.name != "EstrategiaTuto"){
            gameObject.SetActive(true);
            textoE.gameObject.SetActive(false);
            panel.gameObject.SetActive(true);
            move.allowMove = false;
            StartDialogue(); 
            textName.text = names[index];
            activeE = false;
        }
        else if (currentScene.name == "EstrategiaTuto" && activeE && playerExistente.getActivo().Equals(this.gameObject.name)){
            gameObject.SetActive(true);
            textoE.gameObject.SetActive(false);
            panel.gameObject.SetActive(true);
            StartDialogue(); 
            textName.text = names[index];
            activeE = false;
        }
        if(Input.GetKeyDown(KeyCode.E) && playerExistente.getActivo().Equals(this.gameObject.name))
            {
                if(textComponent.text == sentences[index])
                {
                    NextLine();
                }
                else
                {
                    //textName.text = names[index];
                    StopAllCoroutines();
                    textComponent.text = sentences[index];
                }
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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
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
}
