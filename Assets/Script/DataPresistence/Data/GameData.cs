using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class GameData 
{
    public Vector3 playerPosistion;

    public GameData()
    {
        playerPosistion = new Vector3(0f, 0f, 0f);
    }
}
