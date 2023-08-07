using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueScripted : MonoBehaviour
{
    [SerializeField] private Image panel;
    [SerializeField] private List<int> momentosScript;
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI textName;
    public string[] sentences;
    public string[] names;
    public float textSpeed;
    private Scene currentScene;
    private int index;
    private bool activeE = true;
    private Player player;
    private int indexScript = 0;
    private bool reactivable = true;


    // Start is called before the first frame update
    void Start()
    {
        panel.gameObject.SetActive(false);
        textComponent.text = string.Empty;
        textName.text = string.Empty;
        currentScene = SceneManager.GetActiveScene ();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScene.name == "EstrategiaTuto" && activeE){
            if(momentosScript.Count > indexScript && momentosScript[indexScript] == 0){
                panel.gameObject.SetActive(false);
                GameManagerE gm = FindObjectOfType<GameManagerE>();
                gm.finDialogo();
                textComponent.text = string.Empty;
                textName.text = string.Empty;
                indexScript++;
            }
            else{
                gameObject.SetActive(true);
                panel.gameObject.SetActive(true);
                StartDialogue(); 
                textName.text = names[index];
                activeE = false;
            }
        }
        else if(activeE && Input.GetKeyDown(KeyCode.E) && currentScene.name != "EstrategiaTuto"){
            gameObject.SetActive(true);
            panel.gameObject.SetActive(true);
            StartDialogue(); 
            textName.text = names[index];
            activeE = false;
        }

        if(Input.GetKeyDown(KeyCode.E))
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
        if(momentosScript.Count > indexScript && index == momentosScript[indexScript]){
            panel.gameObject.SetActive(false);
            if(currentScene.name == "EstrategiaTuto")
            {
                GameManagerE gm = FindObjectOfType<GameManagerE>();
                gm.finDialogo();
            }
            textComponent.text = string.Empty;
            textName.text = string.Empty;
            indexScript++;
        }
        else if(index < sentences.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            textName.text = names[index];
            StartCoroutine(TypeLine());
        }
        else
        {
            panel.gameObject.SetActive(false);
            if(currentScene.name == "EstrategiaTuto")
            {
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
            activeE = true; 
        }                   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
            activeE = false;
        }            
    }

    public void reactivarDialogo(){
        if(reactivable){
            gameObject.SetActive(true);
            panel.gameObject.SetActive(true);
            NextLine();
            textName.text = names[index];
            activeE = false;
            reactivable = false;
        }
    }

    public void setReactivable(){
        reactivable = true;
    }

    public bool getReactivable(){
        return reactivable;
    }
}
