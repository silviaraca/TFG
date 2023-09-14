using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class PuertaHospital : MonoBehaviour
{
    public CambioEscena cambio;
    void Start()
    {
        if(!PlayerPrefs.HasKey("Renfield")){
            cambio.desactivaPuerta();
        }
    }
}
