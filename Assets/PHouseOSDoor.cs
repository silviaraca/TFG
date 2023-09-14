using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHouseOSDoor : MonoBehaviour
{
    public CambioEscena cambioEsc;
    // Update is called once per frame
    void Start()
    {

    }
    void Update()
    {
        
        if(!PlayerPrefs.HasKey("PHouseOSDoor")) {
            cambioEsc.desactivaPuerta();
        }
        else
        { 
            cambioEsc.enabled = true;
            cambioEsc.apagado = false;
        }
    }
}
