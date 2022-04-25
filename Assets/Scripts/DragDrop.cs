using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public GameObject Canvas;
    private bool isDragging = false;
    private bool isOverDropZone = false;
    private GameObject dropZone;
    private GameObject startParent;
    private Vector2 startPosition;
    private baseCard _bsCard;
    // Start is called before the first frame update
    void Start()
    {
        _bsCard = GetComponent<baseCard>();
    }
    private void Awake()
    {
        Canvas = GameObject.Find("Main Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
    }
    public void StartDrag() {

        startPosition = transform.position;
        startParent = transform.parent.gameObject;
        isDragging = true;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("true");
        isOverDropZone = true;
        dropZone = collision.gameObject;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("false");
        isOverDropZone = false;
        dropZone = null;
    }
    // public void playCard(player temp, GameObject card) {
    //   temp.cmdPlaceCard(card);

    //   }
    public void EndDrag() {
        isDragging = false;
        //Debug.Log(Board._instance._playerOne.checkPowerCost(_bsCard));
       

        if (isOverDropZone && Board._instance.playerTurn && gameObject.GetComponent<baseCard>().owner == User.PlayerOne && dropZone.tag == "playerdropzone" && Board._instance._playerOne.checkPowerCost(_bsCard))
        {
            Debug.Log("Let Go");
            transform.SetParent(dropZone.transform, false);
            Board._instance.placeCardOnBoard(User.PlayerOne, gameObject);
            Board._instance._playerOne.Cards.Remove(gameObject);
            Board._instance._playerOne.Powerlvl = Board._instance._playerOne.Powerlvl - _bsCard.powerCost;
        }
        else if (isOverDropZone && !Board._instance.playerTurn && gameObject.GetComponent<baseCard>().owner == User.PlayerTwo && dropZone.tag == "enemydropzone" && Board._instance._playerTwo.checkPowerCost(_bsCard)) {
            transform.SetParent(dropZone.transform, false);
            Board._instance.placeCardOnBoard(User.PlayerTwo, gameObject);
            Board._instance._playerTwo.Cards.Remove(gameObject);
            Board._instance._playerTwo.Powerlvl = Board._instance._playerTwo.Powerlvl - _bsCard.powerCost;
        }
        else
        {
            Debug.Log("failed");
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);  //  Resets to Original Position
        }
    }
                                             //USE END DRAG3 
   /* public void EndDrag3()
    {

        isDragging = false;
        if (isOverDropZone)
        {
            Debug.Log(transform.gameObject.tag);
            if (transform.gameObject.tag == "playercard")
            {
                Debug.Log(dropZone.gameObject.tag);
                if (dropZone.gameObject.tag == "playerdropzone")
                {
                    Debug.Log(Board._instance.playerTurn);
                    if (BattleManager._instance.isBattleState(BattleState.PLAYERTURN))
                    {
                        if (BattleManager._instance.player.getPowerLevel() - transform.gameObject.GetComponent<baseCard>().powerCost > 0)
                        {
                            Debug.Log("High enough power level");
                            transform.SetParent(dropZone.transform, false);
                            playCard(BattleManager._instance.player, transform.gameObject);
                        }
                        else { Debug.Log("Need More Power"); }

                    }
                }


            }


            if (transform.gameObject.tag == "enemycard")
            {
                Debug.Log(dropZone.gameObject.tag);
                if (dropZone.gameObject.tag == "enemydropzone")
                {
                    Debug.Log(BattleManager._instance.isBattleState(BattleState.ENEMYTURN));
                    if (BattleManager._instance.isBattleState(BattleState.ENEMYTURN))
                    {
                        transform.SetParent(dropZone.transform, false);
                        playCard(BattleManager._instance.enemy, transform.gameObject);
                    }
                }


            }

        }
        else
        {
            Debug.Log("reset");
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);
        }

    }
   */
    /*
    public void EndDrag() {
        isDragging = false;
        if (isOverDropZone) {

            Debug.Log(dropZone.gameObject.tag);
            if (BattleManager._instance.isBattleState(BattleState.PLAYERTURN) && transform.gameObject.tag == "playercard")
            {
                if (BattleManager._instance.player.getPowerLevel() - transform.gameObject.GetComponent<baseCard>().powerCost > 0)
                {
                    Debug.Log("High enough power level");
                    transform.SetParent(dropZone.transform, false);
                    playCard(BattleManager._instance.player, transform.gameObject);
                    BattleManager._instance.player.takePower(transform.gameObject.GetComponent<baseCard>().powerCost);

                }
                else {
                    
                    Debug.Log(BattleManager._instance.player.getPowerLevel());
                    reset();
                }
            }
            else if (BattleManager._instance.isBattleState(BattleState.ENEMYTURN) && transform.gameObject.tag == "enemycard")
            {
                if (Settings.opponentType == true)
                {
                    if (BattleManager._instance.enemy.getPowerLevel() - transform.gameObject.GetComponent<baseCard>().powerCost > 0)
                    {
                        transform.SetParent(dropZone.transform, false);
                        playCard(BattleManager._instance.enemy, transform.gameObject);
                        BattleManager._instance.enemy.takePower(transform.gameObject.GetComponent<baseCard>().powerCost);
                    }
                    else { reset(); }
                }
                if (Settings.opponentType == false)
                {
                    if (BattleManager._instance.player.getPowerLevel() - transform.gameObject.GetComponent<baseCard>().powerCost > 0)
                    {
                        transform.SetParent(dropZone.transform, false);
                        playCard(BattleManager._instance.player, transform.gameObject);
                        BattleManager._instance.player.takePower(transform.gameObject.GetComponent<baseCard>().powerCost);
                    }
                    else { reset(); }
                }
                
                else { reset(); }
            }
            else
            {
                transform.position = startPosition;
                transform.SetParent(startParent.transform, false);
            }

        }
        else
        {
            Debug.Log("resetting");
            reset();
            //transform.position = startPosition;
            //transform.SetParent(startParent.transform, false);
        }

    }

    

    public void reset()
    {
        transform.position = startPosition;
        transform.SetParent(startParent.transform, false);
    }
    */
   /* public void EndDrag2() {

        
        
        isDragging = false;
        if (isOverDropZone)
        {
            Debug.Log(transform.name + " " + dropZone.gameObject.tag);
            if ((transform.gameObject.tag == "playercard" && dropZone.gameObject.tag == "playerdropzone"))
            {
                Debug.Log("Check 1");
                if (isOverDropZone)
                {

                    transform.SetParent(dropZone.transform, false);
                    BattleManager._instance.placePlayerCard(transform.gameObject);
                    //Debug.Log(BattleManager._instance.player.getIndexOf(transform.gameObject));
                    //Debug.Log(BattleManager._instance.player.contain(transform.gameObject));
                     BattleManager._instance.player.RemoveFromDeck(transform.gameObject);




                }
                else
                {
                    Debug.Log("Check 2");
                    transform.position = startPosition;
                    transform.SetParent(startParent.transform, false);
                }
            }
            if ((transform.CompareTag("enemycard") && dropZone.CompareTag("enemydropzone")))
            {
                if (isOverDropZone)
                {

                    transform.SetParent(dropZone.transform, false);
                    BattleManager._instance.placeEnemyCard(startParent);

                }
                else
                {

                    transform.position = startPosition;
                    transform.SetParent(startParent.transform, false);
                }
            }

        }
        else
        {

            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);
        }


        /*
        /*if ((startParent.CompareTag("playercard") && dropZone.CompareTag("playerdropzone")) || (startParent.CompareTag("enemycard") && dropZone.CompareTag("enemydropzone")))
        {
            if (isOverDropZone)
            {

                transform.SetParent(dropZone.transform, false);

            }
        }

        else {

            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);
        }
    }*/
}
