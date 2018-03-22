using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_Player : MonoBehaviour {

    [SerializeField]
    private bool isRealPlayer;
    [SerializeField]
    private int playerNumber;
    [SerializeField]
    private string difficultLevel;
    [SerializeField]
    private string personality;
    [SerializeField]
    private bool isAlive;

    public bool IsRealPlayer
    {
        get
        {
            return isRealPlayer;
        }

        set
        {
            isRealPlayer = value;
        }
    }

    public int PlayerNumber
    {
        get
        {
            return playerNumber;
        }

        set
        {
            playerNumber = value;
        }
    }

    public string DifficultLevel
    {
        get
        {
            return difficultLevel;
        }

        set
        {
            difficultLevel = value;
        }
    }

    public string Personality
    {
        get
        {
            return personality;
        }

        set
        {
            personality = value;
        }
    }

    public bool IsAlive
    {
        get
        {
            return isAlive;
        }

        set
        {
            isAlive = value;
        }
    }


    //////----------------МЕТОДЫ КЛАССА

}
