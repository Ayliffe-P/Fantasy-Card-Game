using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public Board instance;
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
        instance = _bsCard.instance;
    }
    private void Awake()
    {
        Canvas = GameObject.Find("Main Canvas");
    }


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

    public void EndDrag() {
        isDragging = false;
      
       

        if (isOverDropZone && instance.playerTurn && gameObject.GetComponent<baseCard>().owner == User.PlayerOne && dropZone.tag == "playerdropzone" && instance._playerOne.checkPowerCost(_bsCard))
        {
            Debug.Log("Let Go");
            transform.SetParent(dropZone.transform, false);
            instance.placeCardOnBoard(User.PlayerOne, gameObject);
            instance._playerOne.Cards.Remove(gameObject);
            instance._playerOne.Powerlvl = instance._playerOne.Powerlvl - _bsCard.powerCost;
        }
        else if (isOverDropZone && !instance.playerTurn && gameObject.GetComponent<baseCard>().owner == User.PlayerTwo && dropZone.tag == "enemydropzone" && instance._playerTwo.checkPowerCost(_bsCard)) {
            transform.SetParent(dropZone.transform, false);
            instance.placeCardOnBoard(User.PlayerTwo, gameObject);
            instance._playerTwo.Cards.Remove(gameObject);
            instance._playerTwo.Powerlvl = instance._playerTwo.Powerlvl - _bsCard.powerCost;
        }
        else
        {
            Debug.Log("failed");
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);  //  Resets to Original Position
        }
    }
                                           
   
}
