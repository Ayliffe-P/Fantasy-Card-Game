using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class baseUnit : MonoBehaviour
{
    [SerializeField] 
    protected List<GameObject> cards = new List<GameObject>();
    
    public List<GameObject> Cards { get { return cards; }
        set { cards = value; }
    }
    public Transform deckArea;
    public Transform playArea;
    [SerializeField]
    protected int powerlvl;

    public baseUnit(Deck temp, Transform deckTemp) {
       
        deck = temp;
    
    }
    public baseUnit(List<GameObject> _cards, int _powerlevel, int _health)
    {
        cards = _cards;
        powerlvl = _powerlevel;
        currentHp = _health;
    }
    public User player;
    public int Powerlvl {
        get { return powerlvl; }
        set { powerlvl = value; }
    }

    public Deck deck;

    public int maxHp;

    [SerializeField]
    protected int currentHp;
    public int CurrentHp  
    {
        get { return currentHp; }
        set { currentHp = value; }
    }

    public bool isDead = false;

    void Start()
    {
        
    }
    public baseUnit AICopy()
    {
        baseUnit other = (baseUnit)this.MemberwiseClone();
        Debug.Log(other.cards.Count);
        return other;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void drawCards() {
        //OVERRIDDEN
        while (cards.Count < 15)
        {
            GameObject temp = deck.generateCard();
            temp.gameObject.tag = "playercard";
            temp.GetComponent<baseCard>().owner = player;
            cards.Add(temp);
           
            
        }
        Debug.Log(cards.Count);
    }

    public bool checkPowerCost(baseCard card) {
        
        return (powerlvl - card.powerCost >= 0);        //Returns true if player has enough Power to place card
        
    }

    public baseUnit Clone(baseUnit temp) { 

        baseUnit cl = new baseUnit(temp.cards, temp.powerlvl, temp.currentHp);
        return cl;

    }
    

}
