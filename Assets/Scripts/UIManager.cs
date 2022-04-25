using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public GameObject playerOne_HealthIcon;
    public Text playerOne_HealthText;
    public GameObject playerOne_PowerIcon;
    public Text playerOne_PowerText;

    public GameObject playerTwo_HealthIcon;
    public Text playerTwo_HealthText;
    public GameObject playerTwo_PowerIcon;
    public Text playerTwo_PowerText;

    public GameObject text;
    public GameObject textHighlight;

    public delegate void endRound();
    public static endRound endRoundEvent;

    public delegate void announcement();
    public static announcement message;

    public static int playerOne_HealthPoints, playerOne_PowerPoints, playerTwo_HealthPoints, playerTwo_PowerPoints;


    void Start()
    {
        
    }

    public void endRoundButtonPressed() {

        if (endRoundEvent != null)
        {
            endRoundEvent();
        }
    

    }

    public void sendMessage(string mes)
    {
        Debug.Log(textHighlight.activeInHierarchy);
        if (textHighlight.activeInHierarchy == false)
        {
            Debug.Log("setting active");
            textHighlight.SetActive(true);
        }
        StartCoroutine("startMessage",mes);
    }

    public IEnumerator startMessage(string str) {

        Debug.Log(str);

        

        text.GetComponent<Text>().text = str;

        yield return new WaitForSecondsRealtime(2);

        textHighlight.SetActive(false);
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        playerOne_HealthText.text = playerOne_HealthPoints.ToString();
        playerOne_PowerText.text = playerOne_PowerPoints.ToString();
        playerTwo_HealthText.text = playerTwo_HealthPoints.ToString();
        playerTwo_PowerText.text = playerTwo_PowerPoints.ToString();
    }
}
