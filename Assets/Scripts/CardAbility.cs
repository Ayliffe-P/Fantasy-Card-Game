using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAbility : MonoBehaviour
{
    public int dmg;
    public int heal;
    public int boostedDMG;
    public int boostedHeal;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool ability() {
        bool temp = false;
        switch (gameObject.tag) {
            case "soldier":

                break;
            case "swordsman":

                break;
            case "fireball":

                break;
            case "staff":

                break;
            case "wizard":

                break;


        }

        return temp;
    }
   /* public bool dealDamage(int dmg) {
        bool temp = false;
    if (gameObject.transform.parent.name.ToLower() == "playerdropzone")
    {
        temp = BattleManager._instance.enemy.TakeDamage(dmg);
        }

        if (gameObject.transform.parent.name.ToLower() == "enemydropzone")
        {
            temp = BattleManager._instance.enemy.TakeDamage(dmg);
        }
        return temp;
    }

    public void Heal() {
        if (gameObject.transform.parent.name.ToLower() == "playerdropzone")
        {
             BattleManager._instance.enemy.TakeDamage(dmg);
        }

        if (gameObject.transform.parent.name.ToLower() == "enemydropzone")
        {
             BattleManager._instance.enemy.TakeDamage(dmg);
        }

    }*/
    public void isCombo() { }

}
