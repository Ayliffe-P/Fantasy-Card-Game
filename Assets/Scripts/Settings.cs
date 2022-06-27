using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Mode { Hotseat, AI_Type1, AI_Type2 }
public enum Difficulty { None, Easy, Hard, Medium }
public class Settings : MonoBehaviour
{
    public static Settings _instance;
    public Mode mode;
    public Difficulty difficulty;
    
    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
