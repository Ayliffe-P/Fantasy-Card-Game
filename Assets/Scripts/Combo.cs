using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    public List<CardType> combo;
    public int addit_Dmg,addit_Heal;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool containsItem(CardType cardType)
    {

        foreach (CardType i in combo)
        {
            Debug.Log(i + " is i and cardtype is  " + cardType);
            if (i == cardType)
            {

                return true;

            }
            else { break; }
        }
        return false;
    }
    public bool contains(CardType cardType) {

        if (combo.Contains(cardType))
        {
            return true;
        }
        return false;

    }

    public int getDmg() {
        return addit_Dmg;
        
    }

    public int getHeal() {
        return addit_Heal;
    }
}
