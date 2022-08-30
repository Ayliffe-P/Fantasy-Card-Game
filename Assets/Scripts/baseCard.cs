using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CardType { SOLDIER, SWORDSMAN, FIREBALL, STAFF, WIZARD }
public class baseCard : MonoBehaviour
{

    public Board instance;

    //public enum BuffType { NONE, EXPLOSIVE, ARCANE, POISON, ILLUSION }

    public User owner;

    public CardType cardType;
    //public BuffType buff;
    public Effect effect;

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
                 instance.checkCombos(User.PlayerOne);
                break;
            case User.PlayerTwo:
                instance.checkCombos(User.PlayerTwo);
                break;
            default:
                break;
        }
        if (owner == User.PlayerOne && effect == instance.effect1 && instance.effect1 != Effect.None)
        {
            dmg = dmg + 1;
            heal = heal + 1;
        }
        if (owner == User.PlayerTwo && effect == instance.effect2 && instance.effect1 != Effect.None)
        {
            dmg = dmg + 1;
            heal = heal + 1;
        }

        if (dmg > 0 && owner == User.PlayerOne)
        {
            instance.attackPlayer(User.PlayerTwo, dmg);
            
        }else if (dmg > 0 && owner == User.PlayerTwo)
        {
            instance.attackPlayer(User.PlayerOne, dmg);

        }
        if (heal > 0 && owner == User.PlayerOne)
        {
            instance.healPlayer(User.PlayerOne, heal);
        }
        else
        if (heal > 0 && owner == User.PlayerTwo)
        {
            instance.healPlayer(User.PlayerTwo, heal);
        }
       // Debug.Log("Needs destroying");
        Destroy(gameObject);
    }
    public  int[] activate2()
    {
        int[] arr = new int[3];
        arr[0] = dmg;
        arr[1] = heal;
        arr[2] = powerCost;
        return arr;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
