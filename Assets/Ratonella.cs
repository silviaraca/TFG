using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ratonella : MonoBehaviour
{
    public Dialogue dialogueScript1;
    public Dialogue dialogueScript2; 
    public Dialogue dialogueScript3;
    public static bool machine;
    private string ratData;

    void Start()
    {
        ratData = "";
        if(PlayerPrefs.HasKey("RatSecretary")) ratData = PlayerPrefs.GetString("RatSecretary");
        machine = false;
        
    }

    void Update()
    {
        //Si aún no ha cogido el café
        if(ratData == "done" && !machine)
        {
            dialogueScript1.enabled = true;
            dialogueScript2.enabled = false;
            dialogueScript3.enabled = false;
        }

        //Si ha cogido el café
        if(ratData == "done" && machine)
        {
            dialogueScript1.enabled = false;
            dialogueScript2.enabled = false;
            dialogueScript3.enabled = true;
        }

        //Inicio
        if(ratData != "done")
        {
            dialogueScript1.enabled = false;
            dialogueScript2.enabled = true;
            dialogueScript3.enabled = false;
        }
    }
}
