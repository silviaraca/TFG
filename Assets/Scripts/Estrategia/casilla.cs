using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casilla : MonoBehaviour
{
    public Personaje pnj;
    public bool vacia = true;
    private GameManager gm;
    private int posX, posY;
    public bool pintada = false;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        string nombreObjeto = this.gameObject.name.Substring(8);
        //print(nombreObjeto);
        posY = int.Parse(nombreObjeto.Substring(0,1));
        posX = int.Parse(nombreObjeto.Substring(1,1));
        print(posX);
        print(posY);
    }

    public int getPosX(){
        return posX;
    }

    public int getPosY(){
        return posY;
    }
}
