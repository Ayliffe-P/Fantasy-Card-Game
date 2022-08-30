using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimBoard : MonoBehaviour
{
    public SimAI _playerSimOne;
    public SimAI _playerSimTwo;
    public MCTS main;

    public int depth;

    public List<GameObject> EnemySimulationCards = new List<GameObject>();
    public List<GameObject> PlayerSimulationCards = new List<GameObject>();

    public int round;
    public bool playerTurn;
    public baseUnit copy;
    public SimBoard(SimAI p1, SimAI p2, MCTS main, bool turn, int rou)
    {
        _playerSimOne = p1;
        _playerSimTwo = p2;
        playerTurn = turn;
        this.main = main;
        round = rou;
        depth = 7;
    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetupSimulation() {

        if (playerTurn == true)
        {
            simulatePlayerTurn();
        }
        else 
        {
            simulateAITurn();
        }
    }

    public void endRound()
    {
        if (playerTurn == true)
        {
            Debug.Log(PlayerSimulationCards.Count);
            foreach (GameObject item in PlayerSimulationCards)
            {
                int[] arr = item.GetComponent<baseCard>().activate2();
                attackPlayer(User.PlayerTwo, arr[0]);
                healPlayer(User.PlayerOne, arr[1]);
                
                int res = checkSimEndGame();
                Debug.Log(res);
                switch (res)
                {
                    case 0:
                        break;
                    case 1:
                        endSim(item.GetComponent<baseCard>().cardType, res);
                        return;
                    case -1:
                        endSim(item.GetComponent<baseCard>().cardType, res);
                        return;
                };
            }

            playerTurn = false;
          
            Debug.Log(_playerSimTwo);

        }
        else
        {
            foreach (GameObject item in EnemySimulationCards)
            {
                int[] arr = item.GetComponent<baseCard>().activate2();
                attackPlayer(User.PlayerOne, arr[0]);
                healPlayer(User.PlayerTwo, arr[1]);
                int res = checkSimEndGame();
                Debug.Log(res);
                switch (res)
                {
                    case 0:
                        break;
                    case 1:
                        endSim(item.GetComponent<baseCard>().cardType, res);
                        return;
                    case -1:
                        endSim(item.GetComponent<baseCard>().cardType, res);
                        return;
                }
            }
            playerTurn = true;
        }
        Debug.Log(checkCards());
        

        if (round % 2 == 0)
        {
            giveSimPower();
        }
        round = round + 1;
        if (checkCards() == true)
        {
            Debug.Log("NO CARDS LEFT");
            endSim(0);
            return;
        }
        else {
            clearSimulationPlayArea();
            getSimulationPlay();
            Debug.Log(_playerSimTwo);
        }

        Debug.Log("Sim 2 hp is  : " + _playerSimTwo.CurrentHp);

    }

    public bool checkCards() {
        if (_playerSimOne.Cards.Count == 0 || _playerSimTwo.Cards.Count == 0)
        {
            return true;
        }
        return false;
    }

    public void endSim(CardType type, int result) {

        main.updateStats(type, result);
    
    }

    public void endSim(int result)
    {
        Debug.Log("SIM ENDED RESULT ; " + result);


    }
    public void attackPlayer(User playerToAttack, int amount)
    {
        if (playerToAttack == User.PlayerOne)
        {
            _playerSimOne.CurrentHp = _playerSimOne.CurrentHp - amount;
        }
        else if (playerToAttack == User.PlayerTwo)
        {
            _playerSimTwo.CurrentHp = _playerSimTwo.CurrentHp - amount;
        }
    }


    public void healPlayer(User playerToHeal, int amount)
    {
        if (playerToHeal == User.PlayerOne)
        {
            _playerSimOne.CurrentHp = _playerSimOne.CurrentHp + amount;
        }
        else if (playerToHeal == User.PlayerTwo)
        {
            _playerSimTwo.CurrentHp = _playerSimTwo.CurrentHp + amount;
        }
    }
    

    public int checkSimEndGame()
    {
        if (_playerSimTwo.CurrentHp <= 0)
        {
            Debug.Log(_playerSimTwo.CurrentHp);
            //UIManager._instance.sendMessage("The Game is over, Player One Won");
            return -1;
        }
        else if (_playerSimOne.CurrentHp <= 0)
        {
            Debug.Log(_playerSimOne.CurrentHp);
            //UIManager._instance.sendMessage("The Game is over, Player Two Won");
            return 1;
        }
        return 0;

    }

    public void placeSimulationCard(User user, GameObject card)
    {
        if (user == User.PlayerOne)
        {
            PlayerSimulationCards.Add(card);
        }
        else if (user == User.PlayerTwo)
        {
            EnemySimulationCards.Add(card);
        }

    }

    public void getSimulationPlay()
    {
        if (playerTurn == true)
        {

            simulatePlayerTurn();
        }
        
        if (playerTurn == false)
        {
           simulateAITurn();
        }

    }
    public void giveSimPower()
    {
        _playerSimOne.Powerlvl = _playerSimOne.Powerlvl + 5;
        _playerSimTwo.Powerlvl = _playerSimTwo.Powerlvl + 5;
    }
    public void simulateAITurn()
    {
        List<GameObject> play = search(User.PlayerTwo);
        Debug.Log(_playerSimOne.Cards.Count + " is sim 2 card count");
        Debug.Log("Ai card count " + play.Count);
        for (int i = 0; i < play.Count; i++)
        {
            //play[i].transform.SetParent(playArea.transform, false);
            placeSimulationCard(User.PlayerTwo, play[i]);
           _playerSimTwo.Cards.Remove(play[i]);
           _playerSimTwo.Powerlvl = _playerSimTwo.Powerlvl - play[i].GetComponent<baseCard>().powerCost;
        }
        Debug.Log(_playerSimTwo.Cards.Count + " is sim 2 card count");
        endRound();
    }
    public void simulatePlayerTurn()
    {
        List<GameObject> play = search(User.PlayerOne);
        Debug.Log(_playerSimOne.Cards.Count + " is sim 1 card count");
        Debug.Log("Player card count " + play.Count);
        for (int i = 0; i < play.Count; i++)
        {
            //play[i].transform.SetParent(playArea.transform, false);
            placeSimulationCard(User.PlayerOne, play[i]);
            _playerSimOne.Cards.Remove(play[i]);
            _playerSimOne.Powerlvl =_playerSimOne.Powerlvl - play[i].GetComponent<baseCard>().powerCost;
        }
        Debug.Log(_playerSimOne.Cards.Count + " is sim 1 card count");
        endRound();
    }
    
    public void clearSimulationPlayArea()
    {
        
        

            PlayerSimulationCards.Clear();
        
            EnemySimulationCards.Clear();
        
    }

    public List<GameObject> search(User user)
    {
        if (user == User.PlayerOne)
        {
            copy = _playerSimOne.AICopy();
        }
        else {
            copy = _playerSimTwo.AICopy();
        }
        
        int depthIndex = 0;
        List<GameObject> _availableCards = getCardsInCopyPowerCost(copy.Cards);
        List<GameObject> cardsToPlay = new List<GameObject>();
        //  Debug.Log(getNeedForPowerHeuristic(powerlvl));
       



        GameObject card = null;
        while (_availableCards.Count != 0 && depthIndex <= depth)
        {

            foreach (var item in _availableCards)
            {
                Debug.Log(item.GetComponent<baseCard>());
                if (card == null)
                {
                    card = item;
                }
                else if (getCardHeuristic(item.GetComponent<baseCard>()) > getCardHeuristic(card.GetComponent<baseCard>()))
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
        copy = null;
        return cardsToPlay;
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
    public double getPowerCostHeuristic(double x = 0.0)
    {
        // Returns the the cards powercost Heuristic;
        //double  = 0.0;
        double y;

        y = -0.06666667 * x + 1;
        //Debug.Log(y);
        return y;
    }

    public double getHealHeuristic(double x = 0.0)
    {
        // Returns the the cards heal Heuristic;

        //double  = 0.0;
        double y;


        y = 0.4463544 * (Mathf.Pow((float)x, 0.2692637f));
        //Debug.Log(y);
        return y;
    }

    public double getDamageHeuristic(double x = 0.0)
    {
        // Returns the the cards damage Heuristic;

        double y;


        y = 0.4463544 * (Mathf.Pow((float)x, 0.2692637f));
        //Debug.Log(y);
        return y;
    }

    public double getNeedToHealHeuristic(double x = 0.0)
    {
        // Returns the players Need to heal Heuristic;
        double y;

        y = -0.6388314 + 1.65594 * (Mathf.Pow((float)(float)System.Math.E, ((float)-0.0324402 * (float)x)));

        return y;
    }
    public double getNeedForPowerHeuristic(double x = 0.0)
    {
        // Returns the players Need for power Heuristic;
        double y;

        y = -0.5107393 + 1.51074 * (Mathf.Pow((float)(float)System.Math.E, ((float)-0.07229967 * (float)x)));

        return y;
    }

    public double getCardHeuristic(baseCard card)
    {
        // Returns the Average Heuristic of the Card

        if (card.heal != 0 || getNeedToHealHeuristic(copy.CurrentHp) <= 0.5)
        {
            return ((getPowerCostHeuristic(card.powerCost) + getDamageHeuristic(card.dmg) + getHealHeuristic(card.heal)) / 3);
        }
        else
        {
            Debug.Log(card + " " + (getPowerCostHeuristic(card.powerCost) + getDamageHeuristic(card.dmg) + getHealHeuristic(card.heal)) / 3);
            return ((getPowerCostHeuristic(card.powerCost) + getDamageHeuristic(card.dmg)) / 2);
        }
    }
}
