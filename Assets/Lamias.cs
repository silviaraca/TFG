using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Lamias : MonoBehaviour
{
    public Dialogue dialogoNormal;
    public DialogoDecisiones dialogoDecisiones; 
    private string lamias;
    public static int rightAnswers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.HasKey("RegaloLamias"))
        {
            dialogoNormal.enabled = true;
            dialogoDecisiones.enabled = false;

        }
        else
        {
            dialogoNormal.enabled = false;
            dialogoDecisiones.enabled = true;
        }
        
    }

}
