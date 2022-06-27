using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Reference for JSON save https://prasetion.medium.com/saving-data-as-json-in-unity-4419042d1334
// Reference for MCTS https://medium.com/@quasimik/monte-carlo-tree-search-applied-to-letterpress-34f41c86e238


[System.Serializable]
public class MCTS : Player
{

    private int fireballCount = 0;
    private int fireballWinCount = 0;

    private int swordsmanCount = 0;
    private int swordsmanWinCount = 0;

    private int wizardCount = 0;
    private int wizardWinCount = 0;

    private int soldierCount = 0;
    private int soldierWinCount = 0;

    private int staffCount = 0;
    private int staffWinCount = 0;

    string saveFile;

    public baseUnit copy;

    float timer = 0.0f;
    // private List<GameObject> cardsCopy;
    public int depth;

    public Board boardClone;
    public baseUnit playerCopy;

    public MCTS(Deck temp, Transform deckArea) : base(temp, deckArea)
    {
        this.deckArea = deckArea;
      
        Debug.Log(this);

    }

    void Start()
    {
        // copy = AICopy();
        Invoke("simulate", 2f);      
        // writeFile();
        // readFile();
        //boardClone = (Board)Board._instance.clone();
        playerCopy = Board._instance._playerOne.Clone(Board._instance._playerOne);
        //Debug.Log(copy.Cards.Count);
        Debug.Log("Starting");
        // SaveFile();
       // cards.CopyTo(cardsCopy.ToArray());
    }

    private void Awake()
    {
        saveFile = Application.persistentDataPath + "/savedata.json";
    }


    public void treesearch() {
     
        List<GameObject> playingCards = new List<GameObject>();

       playingCards = getBestCardPlay();
        timer = 0;
        while (timer < 10)
        {

            timer += Time.deltaTime;
        }
        //boardClone = new Board(boa)

        }

    public List<GameObject> getBestCardPlay() {
        List<GameObject> _availableCards = getCardsInCopyPowerCost(copy.Cards);
        List<GameObject> cardsToPlay = new List<GameObject>();
        GameObject card = null;
        while (_availableCards.Count != 0 && timer < 10)
        {
           
            foreach (var item in _availableCards)
            {
               
                if (card == null)
                {
                    card = item;
                }
                else if (getUCBValue( item.GetComponent<baseCard>().cardType, cardsToPlay[cardsToPlay.Count-1].GetComponent<baseCard>().cardType ) > getUCBValue(card.GetComponent<baseCard>().cardType, cardsToPlay[cardsToPlay.Count - 1].GetComponent<baseCard>().cardType))
                {
                    card = item;
                }
            }
            cardsToPlay.Add(card);


            timer += Time.deltaTime;
            _availableCards.Clear();
            _availableCards = getCardsInCopyPowerCost(copy.Cards);
        }
        return cardsToPlay;
    }

    public void simulate() {
        Debug.Log("simulating");
        // copy = AICopy();
        playerCopy = Board._instance._playerOne.Clone(Board._instance._playerOne);
        Debug.Log(playerCopy.Cards.Count);





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
    // Update is called once per frame
    void Update()
    {
        
    }

    public double getUCBValue(CardType type, CardType parent) {

        int num = 0;
        int numWin = 0;

        switch (type)
        {
            case CardType.SOLDIER:
                num = soldierCount;
                numWin = soldierWinCount;
                break;
            case CardType.SWORDSMAN:
                num = swordsmanCount;
                numWin = swordsmanWinCount;
                break;
            case CardType.FIREBALL:
                num = fireballCount;
                numWin = fireballWinCount;
                break;
            case CardType.STAFF:
                num = staffCount;
                numWin = staffWinCount;
                break;
            case CardType.WIZARD:
                num = wizardCount;
                numWin = wizardWinCount;
                break;
        }

        float numParent = 0;

        switch (parent)
        {
            case CardType.SOLDIER:
                numParent = soldierCount;
                break;
            case CardType.SWORDSMAN:
                numParent = swordsmanCount;
                break;
            case CardType.FIREBALL:
                numParent = fireballCount;
                break;
            case CardType.STAFF:
                numParent = staffCount;
                break;
            case CardType.WIZARD:
                numParent = wizardCount;
                break;
        }

        return ((numWin/num) + (Mathf.Sqrt(2) * Mathf.Sqrt(Mathf.Log(numParent)/num)));



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

        if (card.heal != 0 || getNeedToHealHeuristic(currentHp) <= 0.5)
        {
            return ((getPowerCostHeuristic(card.powerCost) + getDamageHeuristic(card.dmg) + getHealHeuristic(card.heal)) / 3);
        }
        else
        {
            Debug.Log(card + " " + (getPowerCostHeuristic(card.powerCost) + getDamageHeuristic(card.dmg) + getHealHeuristic(card.heal)) / 3);
            return ((getPowerCostHeuristic(card.powerCost) + getDamageHeuristic(card.dmg)) / 2);
        }
    }

    public void readFile()
    {
        // Does the file exist?
        if (File.Exists(saveFile))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);
            GameData data = new GameData();
           // Deserialize the JSON data 
           //  into a pattern matching the GameData class.
           data = JsonUtility.FromJson<GameData>(fileContents);
            Debug.Log(data);
        }
    }

    public void writeFile()
    {
        GameData data = new GameData(0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        // Serialize the object into JSON and save string.
        string jsonString = JsonUtility.ToJson(data);

        Debug.Log(jsonString);
        // Write JSON to file.
        File.WriteAllText(saveFile, jsonString);
    }

    public List<GameObject> search()
    {

        copy = AICopy();
        int depthIndex = 0;
        List<GameObject> _availableCards = getCardsInCopyPowerCost(copy.Cards);
        List<GameObject> cardsToPlay = new List<GameObject>();
        Debug.Log(getNeedForPowerHeuristic(powerlvl));
        if (getNeedForPowerHeuristic(powerlvl) >= 0.46)
        {
            return cardsToPlay;
        }



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
        return cardsToPlay;
    }



}
