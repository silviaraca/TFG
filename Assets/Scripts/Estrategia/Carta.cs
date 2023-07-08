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
        [SerializeField] private bool personaje;
        private bool enemigo; //Saber si e sun hechizo o un pnj, en caso de pnj si es enemigo
        [SerializeField] private bool area, objetivoUnico, hechizoAtaque, hechizoDefensa, hechizoTablero; //Datos sobre las cartas de hechizo
        [SerializeField] private int ataque, vida, movMax, nAta; //Datos si es personaje
        [SerializeField] private bool dano, cura; //datos sobre el tipo de hechizo o de alguna habilidad especial de personaje
        [SerializeField] private int areaHechizo;
    
    public Casilla getCasAct(){
        return casAct;
    }

    public bool esPersonaje(){
        return personaje;
    }
    public bool esEnemigo(){
        return enemigo;
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

    public void efectoHechizo(Personaje pnj){
        if(dano){
            pnj.danar(ataque);
        }
        else if(cura){
            pnj.danar(-ataque);
        }
    }
    
}
