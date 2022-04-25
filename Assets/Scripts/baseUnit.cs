using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseUnit : MonoBehaviour
{
    [SerializeField] 
    protected List<GameObject> cards = new List<GameObject>();

    public List<GameObject> Cards { get { return cards; }
        set { cards = value; }
    }
    public Transform deckArea;
    [SerializeField]
    protected int powerlvl;

    public baseUnit(Deck temp, Transform deckTemp) {
       
        deck = temp;
    
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
    

}
