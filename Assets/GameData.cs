using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData 
{
    [SerializeField] public int fireballCount ;
    [SerializeField] public int fireballWinCount ;

    [SerializeField] public int swordsmanCount ;
    [SerializeField] public int swordsmanWinCount ;

    [SerializeField] public int wizardCount ;
    [SerializeField] public int wizardWinCount ;

    [SerializeField] public int soldierCount ;
    [SerializeField] public int soldierWinCount ;

    [SerializeField] public int staffCount ;
    [SerializeField] public int staffWinCount ;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameData(int fbCount, int fbWinCount, int swCount, int swWinCount, int wizCount, int wizWinCount, int solCount, int solWinCount, int stCount, int stWinCount)
    {
        fireballCount = fbCount;
        fireballWinCount = fbWinCount;

        swordsmanCount = swCount;
        swordsmanWinCount = swWinCount;

        wizardCount = wizCount;
        wizardWinCount = wizWinCount;

        soldierCount = solCount;
        soldierWinCount = solWinCount;

        staffCount = stCount;
        staffWinCount = stWinCount;

    }
    public GameData()
        { 
        fireballCount = 1;
        fireballWinCount = 1;

        swordsmanCount = 1;
        swordsmanWinCount = 1;

        wizardCount = 1;
        wizardWinCount = 1;

        soldierCount = 1;
        soldierWinCount = 1;

        staffCount = 1;
        staffWinCount = 1;
        }
    }
