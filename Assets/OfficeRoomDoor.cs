using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeRoomDoor : MonoBehaviour
{
    public CambioEscena cambioEsc;
    // Update is called once per frame
    void Start()
    {
        PlayerPrefs.DeleteKey("OfficeDoor");
    }
    void Update()
    {
        
        if(!PlayerPrefs.HasKey("OfficeDoor")) cambioEsc.desactivaPuerta();
        else
        { 
            cambioEsc.enabled = true;
            CambioEscena.apagado = false;
        }
    }
}
