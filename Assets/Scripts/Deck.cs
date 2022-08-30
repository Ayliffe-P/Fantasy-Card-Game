using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    

    System.Random rnd = new System.Random();

    public List<GameObject> deckOfCards = new List<GameObject>();

    public GameObject generateCard()
    {

       GameObject card = Instantiate(deckOfCards[rnd.Next(0, deckOfCards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
        return card;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
