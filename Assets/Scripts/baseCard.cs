using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CardType { SOLDIER, SWORDSMAN, FIREBALL, STAFF, WIZARD }
public class baseCard : MonoBehaviour
{
    

    public enum BuffType { NONE, EXPLOSIVE, ARCANE, POISON, ILLUSION }

    public User owner;

    public CardType cardType;
    public BuffType buff;

    public int dmg;
    public int heal;
    public int powerCost;
   

    public Sprite none;
    public Sprite explosion;
    public Sprite arcane;
    public Sprite illusion;
    public Sprite poison;

    public virtual void activate() {
        switch (owner)
        {
            case User.PlayerOne:
                 Board._instance.checkCombos(User.PlayerOne);
                break;
            case User.PlayerTwo:
                Board._instance.checkCombos(User.PlayerTwo);
                break;
            default:
                break;
        }
       
        if (dmg > 0 && owner == User.PlayerOne)
        {
            Board._instance.attackPlayer(User.PlayerTwo, dmg);
            
        }else if (dmg > 0 && owner == User.PlayerTwo)
        {
            Board._instance.attackPlayer(User.PlayerOne, dmg);

        }
        if (heal > 0 && owner == User.PlayerOne)
        {
            Board._instance.healPlayer(User.PlayerOne, heal);
        }
        else
        if (heal > 0 && owner == User.PlayerTwo)
        {
            Board._instance.healPlayer(User.PlayerTwo, heal);
        }
       // Debug.Log("Needs destroying");
        Destroy(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
