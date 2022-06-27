using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum User {None, PlayerOne, PlayerTwo}
public enum Effect { None, Poison, Illusion, Explosion, Arcane }
public  class Board : MonoBehaviour
{
    public static Board _instance ;

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public Board(baseUnit p1, baseUnit p2) {
        _playerOne = p1;
        _playerTwo = p2;
    }
   
    public bool playerTurn;

    public Deck boardDeck;

    delegate void setupBoard();
    setupBoard setupEvent;

    public baseUnit _playerOne;
    public baseUnit _playerTwo;
    

    public Transform playerOne_DeckArea;
    public Transform playerTwo_DeckArea;
    public GameObject playerOne;

    public int round;

    public Effect effect1;
    public Effect effect2;

    [SerializeField] private List<GameObject> EnemyPlacedcards = new List<GameObject>();
    [SerializeField] private List<GameObject> PlayerPlacedcards = new List<GameObject>();

    private List<GameObject> EnemySimulationCards = new List<GameObject>();
    private List<GameObject> PlayerSimulationCards = new List<GameObject>();



    public List<GameObject> returnCopyPlayerPlacedCards() {  List < GameObject > temporary = new List<GameObject>(PlayerPlacedcards);
        return temporary; 
    }
    public List<GameObject> returnCopyEnemyPlacedCards()
    {
        List<GameObject> temporary = new List<GameObject>(EnemyPlacedcards);
        return temporary;
    }

    public void endSimulationRound()
    { 



    }


        void Start()
    {
          
        playerOne = new GameObject();
        setupEvent = setupBoardHotseat;
        round = 1;
        UIManager.endRoundEvent += endRound;        
        if (setupEvent != null)
        {
            setupEvent();
        }
        stateTurn();
    }

    public void stateTurn() {
        if(playerTurn == true)
        {
            UIManager._instance.sendMessage("It is Player One's Turn");
        }
        else { UIManager._instance.sendMessage("It is Player Two's Turn"); }
    }

    // Update is called once per frame
    void Update()
    {
        UIManager.playerOne_HealthPoints = _playerOne.CurrentHp;
        UIManager.playerOne_PowerPoints = _playerOne.Powerlvl;

        UIManager.playerTwo_HealthPoints = _playerTwo.CurrentHp;
        UIManager.playerTwo_PowerPoints = _playerTwo.Powerlvl;

    }
    public void attackPlayer(User playerToAttack, int amount) {
        if (playerToAttack == User.PlayerOne)
        {
            _playerOne.CurrentHp = _playerOne.CurrentHp - amount;
        } else if (playerToAttack == User.PlayerTwo)
        {
            _playerTwo.CurrentHp = _playerTwo.CurrentHp - amount;
        }
    }


    public void healPlayer(User playerToHeal, int amount) {
        if (playerToHeal == User.PlayerOne)
        {
            _playerOne.CurrentHp = _playerOne.CurrentHp + amount;
        }
        else if (playerToHeal == User.PlayerTwo)
        {
            _playerTwo.CurrentHp = _playerTwo.CurrentHp + amount;
        }
    }
   /* public object clone() {

       // var clone = new Board((baseUnit)_playerOne.Clone(), (baseUnit) _playerTwo.Clone());
       // return clone;

    
    }
   */



    public void checkCombos(User playerToCheckCombos) {
        if (playerToCheckCombos == User.PlayerOne)
        {
            List<GameObject> temporary = new List<GameObject>(PlayerPlacedcards);



        }
    }

    public void generateBoard() {




        switch (effect1)
        {
            case Effect.None:
                _playerOne.GetComponent<baseUnit>().playArea.GetComponent<Image>().color = Color.white;
                break;
            case Effect.Poison:
                _playerOne.GetComponent<baseUnit>().playArea.GetComponent<Image>().color = Color.green;
                break;
            case Effect.Illusion:
                _playerOne.GetComponent<baseUnit>().playArea.GetComponent<Image>().color = Color.blue;
                break;
            case Effect.Explosion:
                _playerOne.GetComponent<baseUnit>().playArea.GetComponent<Image>().color = Color.yellow;
                break;
            case Effect.Arcane:
                _playerOne.GetComponent<baseUnit>().playArea.GetComponent<Image>().color = Color.magenta;
                break;
            default:
                break;
        }
        switch (effect2)
        {
            case Effect.None:
                _playerTwo.GetComponent<baseUnit>().playArea.GetComponent<Image>().color = Color.white;
                break;
            case Effect.Poison:
                _playerTwo.GetComponent<baseUnit>().playArea.GetComponent<Image>().color = Color.green;
                break;
            case Effect.Illusion:
                _playerTwo.GetComponent<baseUnit>().playArea.GetComponent<Image>().color = Color.blue;
                break;
            case Effect.Explosion:
                _playerTwo.GetComponent<baseUnit>().playArea.GetComponent<Image>().color = Color.yellow;
                break;
            case Effect.Arcane:
                _playerTwo.GetComponent<baseUnit>().playArea.GetComponent<Image>().color = Color.magenta;
                break;
            default:
                break;
        }




    }


    void setupBoardHotseat() {

        playerTurn = (Random.value > 0.5f);

        //_playerOne = playerOne.GetComponent<Player>();
        //playerOne.AddComponent<Player>().deckArea = playerOne_DeckArea;
        //playerOne.GetComponent<Player>().deck = boardDeck;
        //playerOne = new Player(boardDeck, playerOne_DeckArea);
       // playerTwo = new Player(boardDeck, playerTwo_DeckArea);

    }

    public void endRound() {
        
        if (playerTurn == true)
        {
            foreach (GameObject item in PlayerPlacedcards)
            {
                item.GetComponent<baseCard>().activate();
                
            }clearPlayArea();
            playerTurn = false;

            if (Settings._instance.mode == Mode.AI_Type1)
            {
            _playerTwo.GetComponent<AI>().startTurn();
            }
            
        } else 
        {
            
            foreach (GameObject item in EnemyPlacedcards)
            {
                item.GetComponent<baseCard>().activate();
                
            }clearPlayArea();
            playerTurn = true; 
        }
        if (!checkEndGame())
        {
        
        stateTurn();
            if (round % 2 == 0)
            {
            givePower();
                generateBoard();
            }
        }
        round = round + 1;
        
    }

    public void givePower() {
        _playerOne.Powerlvl = _playerOne.Powerlvl + 5;
        _playerTwo.Powerlvl = _playerTwo.Powerlvl + 5;


    }

    public void clearPlayArea() {
        if (playerTurn == true)
        {
            
            PlayerPlacedcards.Clear();
        }
        else if (playerTurn == false)
        {
            EnemyPlacedcards.Clear();
        }
    
    }

    public bool checkEndGame() {
        Debug.Log("checkig");
        if ( _playerTwo.CurrentHp <= 0)
        {
            Debug.Log("Dead");
            UIManager._instance.sendMessage("The Game is over, Player One Won");
            return true;
        } else if (_playerOne.CurrentHp <= 0 ) {
            UIManager._instance.sendMessage("The Game is over, Player Two Won");
            return true;
        }
        return false;
        //Time.timeScale = 0;

    }

    

    public void placeCardOnBoard(User user, GameObject card) {

        if (user == User.PlayerOne)
        {
            PlayerPlacedcards.Add(card);
        }
        else if (user == User.PlayerTwo)
        {
            EnemyPlacedcards.Add(card);
        }

    }
    
}
