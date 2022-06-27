using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combinations : MonoBehaviour
{
    public List<Combo> combinations;



    void Start()
    {
        UIManager.endRoundEvent += checkCombos;
        //combinations = new List<Combo>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    /*public void checkCombos() {

        List<GameObject> tempDec = Board._instance.returnCopyPlayerPlacedCards();

        Debug.Log("Checking");

        for (int i = 0; i < combinations.Count; i++)
        {
            foreach (var item in tempDec)
            {
                Debug.Log("Count is " + combinations[i].combo.Count);
                Debug.Log("item is " + item);
                Debug.Log("Item contain is : " + tempDec.Contains(item));
                
                if (combinations[i].containsItem(item.GetComponent<baseCard>().cardType))
                {
                    Debug.Log("continuing");
                    continue;
                } else { Debug.Log("Returning"); 
                    //return; 
                }
                
            }
        }
        Debug.Log("Combo");
    }*/

    public void checkCombos()
    {
        bool turn = Board._instance.playerTurn;
        List<GameObject> tempDec;
        if (turn == true)
        {
            tempDec = Board._instance.returnCopyPlayerPlacedCards();
        }
        else
        {
            tempDec = Board._instance.returnCopyEnemyPlacedCards();
        }
        

        if (tempDec.Count != 0)
        {
            for (int i = 0; i < combinations.Count; i++)
            {
                Debug.Log(i);
                int temp = checkCards(i, tempDec);
              
                if
                (temp != -1)
                {
                    if (turn == true)
                    {
                        Board._instance.attackPlayer(User.PlayerTwo, combinations[temp].addit_Dmg);
                        Board._instance.healPlayer(User.PlayerOne, combinations[temp].addit_Heal);
                    } else if (turn == false) {
                        Board._instance.attackPlayer(User.PlayerOne, combinations[temp].addit_Dmg);
                        Board._instance.healPlayer(User.PlayerTwo, combinations[temp].addit_Heal);
                    }
                    Debug.Log("is damage: " + combinations[temp].addit_Dmg);
                }
                

            }
        }
        else
        {

            return;
        }

        
        //Combo Here
    }

    public int checkCards(int combo, List<GameObject> tempDec)
    {
        Debug.Log(tempDec.Count + "is count");
        foreach (var item in tempDec)
        {
            Debug.Log(item.GetComponent<baseCard>().cardType);
            if (!combinations[combo].contains(item.GetComponent<baseCard>().cardType))
            {
                return -1;
                //continue;
            }
        }
        return combo;
    }

    public bool checkACombination(int combo, List<GameObject> tempDec)
    {
        foreach (var item in tempDec)
        {
            if (combinations[combo].containsItem(item.GetComponent<baseCard>().cardType))
            {

                return true;
                //continue;
            }


        }
        return false;
    }
}
