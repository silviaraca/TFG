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
    private Casilla casAnt;
    [SerializeField] private bool spawnEne, spawnAli;
    private Color32 colIni;
    [SerializeField] private int consumeMov = 0;

    void Start()
    {
        gm = FindObjectOfType<GameManagerE>();
        string nombreObjeto = this.gameObject.name.Substring(8);
        posY = int.Parse(nombreObjeto.Substring(0,1));
        posX = int.Parse(nombreObjeto.Substring(1,1));
        if(spawnEne) colIni = new Color32(100, 0, 100, 255);
        else if(spawnAli) colIni = new Color32(0, 100, 100, 255);
        else colIni = new Color32(255, 255, 255, 255);
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
    public Casilla getCasAnt(){
        return casAnt;
    }
    public void setCasAnt(Casilla cas){
        casAnt = cas;
    }
    public Color32 getColIni(){
        return colIni;
    }
    public bool esSpawnAli(){
        return spawnAli;
    }
    public bool esSpawnEne(){
        return spawnEne;
    }
}
