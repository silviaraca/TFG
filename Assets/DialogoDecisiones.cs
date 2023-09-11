using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

public class DialogoDecisiones : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoE;
    [SerializeField] private Image panel;
    [SerializeField] private Movement move;
    [SerializeField] private CambioEscenaEstrategia cargar;
    [SerializeField] private List<int> listaMomentoDecisiones;
    [SerializeField] private List<string> listaDecisiones1;
    [SerializeField] private List<string> listaDecisiones2;
    [SerializeField] private Button BotonDecision1;
    [SerializeField] private Button BotonDecision2;
    [SerializeField] private TextMeshProUGUI TextoBotonDecision1;
    [SerializeField] private TextMeshProUGUI TextoBotonDecision2;
    [SerializeField] private Player playerExistente;
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI textName;
    public string[] sentences;
    public string[] names;
    public float textSpeed;

    private int index, indexDecisiones;
    private bool activeE;
    private static bool decision;
    private Player player;


    // Start is called before the first frame update
    void Start()
    {
        decision = false;
        BotonDecision1.gameObject.SetActive(false);
        BotonDecision2.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);
        textComponent.text = string.Empty;
        textName.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if(activeE && Input.GetKeyDown(KeyCode.E) && playerExistente.getActivo().Equals(this.gameObject.name)){
            gameObject.SetActive(true);
            textoE.gameObject.SetActive(false);
            panel.gameObject.SetActive(true);
            move.allowMove = false;
            StartDialogue(); 
            textName.text = names[index];
            activeE = false;
        }
        if(Input.GetKeyDown(KeyCode.E) && !decision && playerExistente.getActivo().Equals(this.gameObject.name))
        {

            if(sentences.Length > index && textComponent.text == sentences[index])
            {
                NextLine();
                
            }
            else
            {
                //textName.text = names[index];
                StopAllCoroutines();
                if(sentences.Length > index) textComponent.text = sentences[index];
                else EndDialogue();
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        indexDecisiones = 0;
        StartCoroutine(TypeLine());
        if(listaMomentoDecisiones.Count > 0 && index == listaMomentoDecisiones[indexDecisiones]){
            BotonDecision1.gameObject.SetActive(true);
            TextoBotonDecision1.text = listaDecisiones1[indexDecisiones];
            BotonDecision2.gameObject.SetActive(true);
            TextoBotonDecision2.text = listaDecisiones2[indexDecisiones];
            decision = true;
        }
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

            //Activar y mostrar botones
            if(listaMomentoDecisiones.Count > 0 && index == listaMomentoDecisiones[indexDecisiones]){
                BotonDecision1.gameObject.SetActive(true);
                TextoBotonDecision1.text = listaDecisiones1[indexDecisiones];
                BotonDecision2.gameObject.SetActive(true);
                TextoBotonDecision2.text = listaDecisiones2[indexDecisiones];
                decision = true;
                if(listaMomentoDecisiones.Count > indexDecisiones + 1) indexDecisiones++;
            }
            textComponent.text = string.Empty;
            textName.text = names[index];
            StartCoroutine(TypeLine());
        }
        else
        {
           EndDialogue();
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

    private void EndDialogue()
    {
        panel.gameObject.SetActive(false);
        move.allowMove = true;
        textComponent.text = string.Empty;
        textName.text = string.Empty;

    }
    public void decision1(){ //De alguna forma scriptear para que las decisiones sea dinámicas, de momento estática
        decision = false;
        //Invoca a la estrategia
        cargar.cargaEstrategia();
        
    }
    public void decision2(){ //De alguna forma scriptear para que las decisiones sea dinámicas, de momento estática
        decision = false;
        BotonDecision1.gameObject.SetActive(false);
        BotonDecision2.gameObject.SetActive(false);
        //Decisión NO, solo pasa como si hubiese dado a la E

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

    public void decision1_VH(){ //De alguna forma scriptear para que las decisiones sea dinámicas, de momento estática
        decision = false;
        //Invoca a la estrategia
        string tuto = "done";
        PlayerPrefs.SetString("Tutorial", tuto);
        PlayerPrefs.Save();
        cargar.cargaEstrategia();
        
    }


                

    public void decisionLamias_Wrong()
    {
        decision = false;
        BotonDecision1.gameObject.SetActive(false);
        BotonDecision2.gameObject.SetActive(false);

        if(textComponent.text == sentences[index])
        {
            NextLine();
            index = sentences.Length;
        }
        else
        {
            
            //textName.text = names[index];
            StopAllCoroutines();
        }
    }

    public void decisionLamias_Right()
    {
        decision = false;
        BotonDecision1.gameObject.SetActive(false);
        BotonDecision2.gameObject.SetActive(false);
        index += 2;

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

    public void decision1_Reinfield(){
        decision = false;
        //Invoca a la estrategia
        cargar.cargaEstrategia();

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
    public void decision2_Reinfield(){
        decision = false;
        BotonDecision1.gameObject.SetActive(false);
        BotonDecision2.gameObject.SetActive(false);
        
    }

    public void anadeCarta(){ //AñadeCarta
        List<string> inventory = new List<string>();
        if(PlayerPrefs.HasKey("InventoryCards")){
            string inventoryData1 = PlayerPrefs.GetString("InventoryCards");
            inventory = JsonConvert.DeserializeObject<List<string>>(inventoryData1);
        }
        inventory.Add("Agua");
        string inventoryData2 = JsonConvert.SerializeObject(inventory);
        PlayerPrefs.SetString("InventoryCards", inventoryData2);
        PlayerPrefs.Save();
        playerExistente.inventory.cargaInventory();
    }

}
