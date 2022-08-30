using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Reference for JSON save https://prasetion.medium.com/saving-data-as-json-in-unity-4419042d1334
// Reference for MCTS https://medium.com/@quasimik/monte-carlo-tree-search-applied-to-letterpress-34f41c86e238


[System.Serializable]
public class MCTS : Player
{
    //public Board instance;

    public int fireballCount = 1;
    public int fireballWinCount = 1;

    public int swordsmanCount = 1;
    public int swordsmanWinCount = 1;

    public int wizardCount = 1;
    public int wizardWinCount = 1;

    public int soldierCount = 1;
    public int soldierWinCount = 1;

    public int staffCount = 1;
    public int staffWinCount = 1;

    string saveFile;

    public baseUnit copy;

    float timer = 0.0f;
    // private List<GameObject> cardsCopy;
    public int depth;

    public SimBoard boardClone;
    public SimAI playerCopyOne;
    public SimAI playerCopyTwo;
    

    public MCTS(Deck temp, Transform deckArea) : base(temp, deckArea)
    {
        this.deckArea = deckArea;
      
        Debug.Log(this);

    }

    void Start()
    {
            loadData();

            Powerlvl = 5;
            CurrentHp = maxHp;
            drawCards();
            // copy = AICopy();
          //  Invoke("simulate", 2f);      
        // writeFile();
        // readFile();
        //boardClone = (Board)Board._instance.clone();
        //playerCopy = Board._instance._playerOne.Clone(Board._instance._playerOne);
        //Debug.Log(copy.Cards.Count);
        Debug.Log("Starting");
        // SaveFile();
       // cards.CopyTo(cardsCopy.ToArray());
    }

    private void Awake()
    {
        saveFile = Application.persistentDataPath + "/savedata.json";
        Debug.Log(Application.persistentDataPath);
    }

    public void loadData() {
        GameData data = readFile();
        Debug.Log(data.fireballCount);
        if (data != null)
        {
            fireballCount = data.fireballCount;
            fireballWinCount = data.fireballWinCount;
            swordsmanCount = data.swordsmanCount;
            swordsmanWinCount = data.swordsmanWinCount;
            wizardCount = data.wizardCount;
            wizardWinCount = data.wizardWinCount;
            soldierCount = data.soldierCount;
            soldierWinCount = data.soldierWinCount;
            staffCount = data.staffCount;
            staffWinCount = data.staffWinCount;
        }
        else {
            fireballCount = 1;
            fireballWinCount = 1;
            swordsmanCount = 1;
            swordsmanWinCount = 1;
            wizardCount = 1;
            wizardWinCount = 1;
            soldierCount = 1;
            soldierWinCount = 1;
            staffCount = 1;
            staffWinCount = 1;
        }
    }


    public void treesearch() {
     
        List<GameObject> playingCards = new List<GameObject>();
        copy = instance._playerTwo.Clone(instance._playerTwo);
        playingCards = getBestCardPlay();
        //place cards
        timer = 0;

        foreach (var item in playingCards)
        {
            item.transform.SetParent(playArea.transform, false);
            instance.placeCardOnBoard(User.PlayerTwo, item);
            instance._playerTwo.Cards.Remove(item);
            instance._playerTwo.Powerlvl = instance._playerTwo.Powerlvl - item.GetComponent<baseCard>().powerCost;
        }

        InitiateSimulation();
        //boardClone = new Board(boa)
        /*while (timer < 2)
        {
            timer += Time.deltaTime;
        }*/
        }

    public MCTS(List<GameObject> _cards, int _powerlevel, int _health, User user) : base (_cards, _powerlevel, _health, user)
    {
        cards = _cards;
        powerlvl = _powerlevel;
        currentHp = _health;
    }

    public List<GameObject> getBestCardPlay() {
        List<GameObject> _availableCards = getCardsInCopyPowerCost(copy.Cards);
        //Debug.Log("AVAILALBE CARDS IS " + _availableCards.Count);
        List<GameObject> cardsToPlay = new List<GameObject>();
        GameObject card = null;
        while (_availableCards.Count != 0 && timer < 2)
        {
           
            foreach (var item in _availableCards)
            {

                if (card == null)
                {
                    card = item;
                   // cardsToPlay.Add(card);
                }
                else if (cardsToPlay.Count > 0) { 
                        if (getUCBValue( item.GetComponent<baseCard>().cardType, cardsToPlay[cardsToPlay.Count-1].GetComponent<baseCard>().cardType ) > getUCBValue(card.GetComponent<baseCard>().cardType, cardsToPlay[cardsToPlay.Count - 1].GetComponent<baseCard>().cardType))
                {
                    card = item;
                }}
            }
            cardsToPlay.Add(card);
            copy.Powerlvl = copy.Powerlvl - card.GetComponent<baseCard>().powerCost;
            card = null;

            timer += Time.deltaTime;
            _availableCards.Clear();
            _availableCards = getCardsInCopyPowerCost(copy.Cards);
        }
        return cardsToPlay;
    }

    public void InitiateSimulation() {
        playerCopyOne = null;
        playerCopyTwo = null;
        boardClone = null;
     
        playerCopyOne = new SimAI(instance._playerOne.Cards, instance._playerOne.Powerlvl, instance._playerOne.CurrentHp, User.PlayerOne);
        playerCopyTwo = new SimAI(instance._playerTwo.Cards, instance._playerTwo.Powerlvl, instance._playerTwo.CurrentHp, User.PlayerTwo);
       
        boardClone = new SimBoard(playerCopyOne, playerCopyTwo, this, instance.playerTurn, instance.round);

        boardClone.SetupSimulation();
       // boardClone.SetupSimulation();

    }

    public void updateStats(CardType type, int val) {
        if (val == 1)
        {
            switch (type)
            {
                case CardType.SOLDIER:
                    soldierCount = soldierCount + 1;
                    soldierWinCount = soldierWinCount + 1;
                    break;
                case CardType.SWORDSMAN:
                    swordsmanCount = swordsmanCount + 1;
                    swordsmanWinCount = swordsmanWinCount + 1;
                    break;
                case CardType.FIREBALL:
                    fireballCount = fireballCount + 1;
                    fireballWinCount = fireballWinCount + 1;
                    break;
                case CardType.STAFF:
                    staffCount = staffCount + 1;
                    staffWinCount = staffWinCount + 1;
                    break;
                case CardType.WIZARD:
                    wizardCount = wizardCount + 1;
                    wizardWinCount = wizardWinCount + 1;
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (type)
            {
                case CardType.SOLDIER:
                    soldierCount = soldierCount + 1;
                    break;
                case CardType.SWORDSMAN:
                    swordsmanCount = swordsmanCount + 1;
                    break;
                case CardType.FIREBALL:
                    fireballCount = fireballCount + 1;
                    break;
                case CardType.STAFF:
                    staffCount = staffCount + 1;
                    break;
                case CardType.WIZARD:
                    wizardCount = wizardCount + 1;
                    break;
                default:
                    break;
            }
        }
        writeFile();
        
    }

    public void Simulate() { 
    

    
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
        Debug.Log(numWin + " " + num + " " + numParent);
        return ((numWin/num) + (Mathf.Sqrt(2) * Mathf.Sqrt(Mathf.Log(numParent)/num)));



    }

    
    


   

    public GameData readFile()
    {
        GameData data = new GameData();
        // Does the file exist?
        if (File.Exists(saveFile))
        {
            //Debug.Log("exists");
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);
            //GameData data = new GameData();
           // Deserialize the JSON data 
           //  into a pattern matching the GameData class.
           data = JsonUtility.FromJson<GameData>(fileContents);
            Debug.Log(data);
            //return data;
        }
        return data;
    }

    public void writeFile()
    {
        GameData data = new GameData(fireballCount, fireballWinCount, swordsmanCount, swordsmanWinCount, wizardCount, wizardWinCount, soldierCount, soldierWinCount, staffCount, staffWinCount);
        // Serialize the object into JSON and save string.
        string jsonString = JsonUtility.ToJson(data);

        Debug.Log(jsonString);
        // Write JSON to file.
        File.WriteAllText(saveFile, jsonString);
    }

    



}
