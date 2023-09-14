using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBuilderActivator : MonoBehaviour
{
    public static bool okey;
    public CambioEscena cambioEsc;
    // Start is called before the first frame update
    void Start()
    {
      okey = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(okey && !PlayerPrefs.HasKey("DeckActivator"))
        {
            string deck = "done";
            PlayerPrefs.SetString("DeckActivator", deck);
        }

       if(PlayerPrefs.HasKey("DeckActivator"))  
       {
            cambioEsc.enabled = true;
            CambioEsc.apagado = false;
       } 
    }
}
