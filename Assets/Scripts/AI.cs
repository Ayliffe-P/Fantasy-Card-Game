using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This for the AI search was adapted from this website https://unitygem.wordpress.com/tree-search/
public class AI : Player
{
   // public Board instance;
    public move move;
    public int tempPowerlvl;
    List<GameObject> nextMove;
    public int depth;

    public AI(Deck temp, Transform deckArea) : base(temp, deckArea)
    {
        this.deckArea = deckArea;
        tempPowerlvl = powerlvl;
        Debug.Log(this);

    }

    void Start()
    {
     
        
        Powerlvl = 5;
        CurrentHp = maxHp;
        drawCards();

    
        switch (Settings._instance.difficulty)
        {
            case Difficulty.None:
                break;
            case Difficulty.Easy:
                depth = 5;
                break;
            case Difficulty.Medium:
                depth = 10;
                break;
            case Difficulty.Hard:
                depth = 15;
                break;

        }
        Debug.Log(getNeedForPowerHeuristic(powerlvl));

    }

    public List<GameObject> getCardsInPowerCost(List<GameObject> _cards)
    {
        List<GameObject> cards = new List<GameObject>();
        foreach (var item in _cards)
        {
            if (checkPowerCost(item.GetComponent<baseCard>()))
            {
                cards.Add(item);
            }
        }
        return cards;
    }

    public List<GameObject> getCardsInCopyPowerCost(List<GameObject> _cards)
    {
        List<GameObject> cards = new List<GameObject>();
        foreach (var item in _cards)
        {
            if (copy.checkPowerCost(item.GetComponent<baseCard>()))
            {
                cards.Add(item);
            }
        }
        return cards;
    }

    public double getCardHeuristic(baseCard card) {

        if (card.heal != 0 || getNeedToHealHeuristic(currentHp)  <= 0.5)
        {
            return ((getPowerCostHeuristic(card.powerCost) + getDamageHeuristic(card.dmg) + getHealHeuristic(card.heal)) / 3);
        }
        else {
            Debug.Log(card + " " + (getPowerCostHeuristic(card.powerCost) + getDamageHeuristic(card.dmg) + getHealHeuristic(card.heal)) / 3);
            return ((getPowerCostHeuristic(card.powerCost) + getDamageHeuristic(card.dmg) ) /2);
        }
    }

    public List<GameObject> getAvailableCards(baseUnit player) {
        List<GameObject> cards = new List<GameObject>();
        foreach (var item in player.Cards)
        {
            if (player.checkPowerCost(item.GetComponent<baseCard>()))
            {
                cards.Add(item);
            }
        }
        return cards;
    
    }

    public double getPowerCostHeuristic(double x = 0.0) {
        //double  = 0.0;
        double y;
        
        y = -0.06666667 * x + 1;
        //Debug.Log(y);
        return y;
    }

    public double getHealHeuristic(double x = 0.0)
    {
        //double  = 0.0;
        double y;


        y = 0.4463544 * (Mathf.Pow((float)x, 0.2692637f));
        //Debug.Log(y);
        return y;
    }

    public double getDamageHeuristic(double x = 0.0)
    {
        double y;


        y = 0.4463544 * (Mathf.Pow((float)x, 0.2692637f));
        //Debug.Log(y);
        return y;
    }

    public double getNeedToHealHeuristic(double x = 0.0)
    { 
        double y;

        y = -0.6388314 + 1.65594 * (Mathf.Pow((float) (float)System.Math.E, ((float)-0.0324402 * (float)x)));
       
        return y;
    }
    public double getNeedForPowerHeuristic(double x = 0.0)
    {
        double y;

        //Debug.Log(System.Math.Pow((float)1.51074 * (float)System.Math.E, ((float)0.07229967 * (float)x)));
       // Debug.Log(Mathf.Pow((float)1.51074 * (float)System.Math.E, ((float)0.07229967 * (float)x)));
        y = -0.5107393 + 1.51074 * (Mathf.Pow((float) (float)System.Math.E, ((float)-0.07229967 * (float)x)));

        return y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
   /* IEnumerator DoMoving()
    {
        List<GameObject> play = search();
        yield return new WaitForSeconds(4f);
        foreach (var item in play)
        {
            Debug.Log(item.name);
        }
        }*/

        public void startTurn() {
        List<GameObject> play = search();
        for (int i = 0; i < play.Count; i++)
        {
            play[i].transform.SetParent(playArea.transform, false);
            instance.placeCardOnBoard(User.PlayerTwo, play[i]);
            instance._playerTwo.Cards.Remove(play[i]);
            instance._playerTwo.Powerlvl = instance._playerTwo.Powerlvl - play[i].GetComponent<baseCard>().powerCost;
        }
     }
    public baseUnit copy;
    public List<GameObject> search() {

        copy = AICopy();
        //Debug.Log("cards in copy is : " + copy.Cards.Count);
        int depthIndex = 0;
        List<GameObject> _availableCards = getCardsInCopyPowerCost(copy.Cards);
        List<GameObject> cardsToPlay = new List<GameObject>();
        Debug.Log(getNeedForPowerHeuristic(powerlvl));
        if (getNeedForPowerHeuristic(powerlvl) >= 0.46)
        {
            return cardsToPlay;
        }
        

       
        GameObject card = null;
        while (_availableCards.Count != 0 && depthIndex <= depth) {
           
            foreach (var item in _availableCards)
            {
                Debug.Log(item.GetComponent<baseCard>());
                if (card == null) {
                    card = item;
                } 
                else if(getCardHeuristic(item.GetComponent<baseCard>()) > getCardHeuristic(card.GetComponent<baseCard>()))
                {
                    card = item;
                }

            }
        
            cardsToPlay.Add(card);
            copy.Cards.Remove(card);
            copy.Powerlvl = copy.Powerlvl - card.GetComponent<baseCard>().powerCost;
            card = null;
            depthIndex = depthIndex + 1;
            _availableCards.Clear();
            _availableCards = getCardsInCopyPowerCost(copy.Cards);  
        }
        return cardsToPlay;   
    }

    


}
