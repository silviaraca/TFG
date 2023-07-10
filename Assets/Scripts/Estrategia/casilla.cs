using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casilla : MonoBehaviour
{
    public Personaje pnj;
    public bool vacia = true;
    private GameManagerE gm;
    private int posX, posY;
    public bool pintada = false;

    private int consumeMov = 0;

    void Start()
    {
        gm = FindObjectOfType<GameManagerE>();
        string nombreObjeto = this.gameObject.name.Substring(8);
        posY = int.Parse(nombreObjeto.Substring(0,1));
        posX = int.Parse(nombreObjeto.Substring(1,1));
    }

    public int getPosX(){
        return posX;
    }

    public int getPosY(){
        return posY;
    }

    public int getConsumeMov(){
        return consumeMov;
    }

    public void setConsumeMov(int c){
        consumeMov = c;
    }
}
