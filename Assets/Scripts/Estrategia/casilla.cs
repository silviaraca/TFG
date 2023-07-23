using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Casilla : MonoBehaviour
{
    public Personaje pnj;
    public bool vacia = true;
    private GameManagerE gm;
    private int posX, posY;
    public bool pintada = false;
    public int fila;
    private Casilla casAnt;
    private Sprite colorAnt;
    [SerializeField] private bool spawnEne, spawnAli;
    [SerializeField] private int consumeMov = 0;
    [SerializeField] private Sprite imagenIni;

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
    public Casilla getCasAnt(){
        return casAnt;
    }
    public void setCasAnt(Casilla cas){
        casAnt = cas;
    }
    public bool esSpawnAli(){
        return spawnAli;
    }
    public bool esSpawnEne(){
        return spawnEne;
    }
    public Sprite getImagenIni(){
        return imagenIni;
    }
    public Sprite getColorAnt(){
        return colorAnt;
    }
    public void setColorAnt(Sprite c){
        colorAnt = c;
    }
}
