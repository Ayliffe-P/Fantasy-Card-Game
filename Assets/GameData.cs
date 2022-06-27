using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData 
{
    [SerializeField] private int fireballCount = 0;
    [SerializeField] private int fireballWinCount = 0;

    [SerializeField] private int swordsmanCount = 0;
    [SerializeField] private int swordsmanWinCount = 0;

    [SerializeField] private int wizardCount = 0;
    [SerializeField] private int wizardWinCount = 0;

    [SerializeField] private int soldierCount = 0;
    [SerializeField] private int soldierWinCount = 0;

    [SerializeField] private int staffCount = 0;
    [SerializeField] private int staffWinCount = 0;

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
        fireballCount = 0;
        fireballWinCount = 0;

        swordsmanCount = 0;
        swordsmanWinCount = 0;

        wizardCount = 0;
        wizardWinCount = 0;

        soldierCount = 0;
        soldierWinCount = 0;

        staffCount = 0;
        staffWinCount = 0;
        }
    }
