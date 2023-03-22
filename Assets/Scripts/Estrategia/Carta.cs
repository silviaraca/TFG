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
        private bool personaje, enemigo; //Saber si e sun hechizo o un pnj, en caso de pnj si es enemigo
        private int ataque, vida, movMax, nAta; //Datos si es personaje
    

    public Casilla getCasAct(){
        return casAct;
    }

    public void setCasAct(Casilla cas){
        casAct = cas;
    }
    
}
