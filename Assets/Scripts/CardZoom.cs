using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardZoom : MonoBehaviour
{

    public GameObject Canvas;

    private GameObject zoomCard;


    public void Awake()
    {
        Canvas = GameObject.Find("Main Canvas");
    }


    public void onHoverEnter() {

       
        if ( Input.mousePosition.y > Screen.height / 2.0f)
        {
            zoomCard = Instantiate(gameObject, new Vector2(Input.mousePosition.x, Input.mousePosition.y - 200), Quaternion.identity);
            zoomCard.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
        }
        else if (Input.mousePosition.y < Screen.height / 2.0f)
        {
            zoomCard = Instantiate(gameObject, new Vector2(Input.mousePosition.x, Input.mousePosition.y + 200), Quaternion.identity);
            zoomCard.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
        }
        zoomCard.transform.SetParent(Canvas.transform, true);
        zoomCard.layer = LayerMask.NameToLayer("Zoom");

        RectTransform rect = zoomCard.GetComponent<RectTransform>();

        rect.sizeDelta = new Vector2(240, 344);

    }

    public void OnHoverExit() {

        Destroy(zoomCard);

    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
