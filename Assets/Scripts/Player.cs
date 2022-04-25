﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : baseUnit
{
    
    public Player(Deck temp, Transform deckArea) : base(temp, deckArea) {
        this.deckArea = deckArea;
        Debug.Log(this);
         
    }

    

    void Start()
    {
        Powerlvl = 5;
        CurrentHp = maxHp;
        drawCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void drawCards()
    {
        Debug.Log(player);
        while (cards.Count < 20)
        {
            GameObject temp = deck.generateCard();
            temp.transform.SetParent(deckArea);
            //temp.GetComponent<baseCard>().user = this;
            temp.GetComponent<baseCard>().owner = player;
            cards.Add(temp);
           
        }
    }
}
