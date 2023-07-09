using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carta : MonoBehaviour
{
    //DATOS GENERALES DE CUALQUIER CARTA
        public bool hasBeenPlayed;
        public bool enMano;
        public int handIndex;
        private Casilla casAct;

        private int nombreCarta; //Basaremos en esta la creación de cartas distintas

    //DATOS DE CADA CARTA EN CONCRETO, dependiendo del nombre de la carta, los datos se buscarán en un documento
        
        //Dependiendo de la carta habrá que elegir la imagen de la carta a usar

        // DATOS SOBRE LA CARTA EN SI, estos datos se usaran al construir el pnj o definir lo que hace un hechizo
        [SerializeField] private bool personaje; //Saber si e sun hechizo o un pnj, en caso de pnj si es enemigo
        [SerializeField] private bool area, objetivoUnico, hechizoAtaque, hechizoDefensa, hechizoTablero; //Datos sobre las cartas de hechizo
        [SerializeField] private int ataque, vida, movMax, nAta, rango; //Datos si es personaje
        [SerializeField] private bool dano, cura; //datos sobre el tipo de hechizo o de alguna habilidad especial de personaje
        [SerializeField] private int areaHechizo;
        public bool zoom = false;
    
    public Casilla getCasAct(){
        return casAct;
    }

    public bool esPersonaje(){
        return personaje;
    }
    public bool esHechizoUnico(){
        return objetivoUnico;
    }
    public bool esHechizoArea(){
        return area;
    }
    public bool esHechizoDefensa(){
        return hechizoDefensa;
    }
    public bool esHechizoAtaque(){
        return hechizoAtaque;
    }
    public bool esHechizoTablero(){
        return hechizoTablero;
    }
    public int getAreaHechizo(){
        return areaHechizo;
    }
    public void setCasAct(Casilla cas){
        casAct = cas;
    }

    public int getVidaMax(){
        return vida;
    }
    public int getAtaque(){
        return ataque;
    }
    public int getMovMax(){
        return movMax;
    }
    public int getNumAta(){
        return nAta;
    }
    public int getRango(){
        return rango;
    }

    public void efectoHechizo(Personaje pnj){
        if(dano && pnj.enemigo){
            pnj.danar(ataque);
        }
        else if(cura && !pnj.enemigo){
            pnj.danar(-ataque);
        }
    }
    
}
