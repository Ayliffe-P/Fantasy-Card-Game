using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    

    System.Random rnd = new System.Random();

    public List<GameObject> deckOfCards = new List<GameObject>();

    public GameObject generateCard()
    {
        
       
        //if (BattleManager._instance.isUnitTypePlayer(UnitType))
       // {
       GameObject card = Instantiate(deckOfCards[rnd.Next(0, deckOfCards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
        return card;
            //card.gameObject.tag = "playercard";
           
            //playerCard.GetComponent<baseCard>().setOwner(BattleManager._instance.Player.GetComponent<player>());

            //Debug.Log(BattleManager._instance.player.getUnitType());
            //playerCard.GetComponent<baseCard>().setOpponent(BattleManager._instance.enemy);
            //playerCard.transform.SetParent(playerArea.transform, false);


      //  }
        /*else
        {

            playerCard = Instantiate(AllCards[rnd.Next(0, AllCards.Count - 1)], new Vector3(0, 0, 0), Quaternion.identity);
            playerCard.gameObject.tag = "enemycard";
            playerCard.GetComponent<baseCard>().setOwner(BattleManager._instance.Opponent.GetComponent<player>());
            // playerCard.GetComponent<baseCard>().setOpponent(BattleManager._instance.player);

            playerCard.transform.SetParent(enemyArea.transform, false);


        }
        */
       
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
