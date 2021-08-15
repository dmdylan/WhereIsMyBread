using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerCharacter { Gura = 0, Ame, Calli, Ina, Kiara}

[CreateAssetMenu(fileName = "New Player Info", menuName = "Scriptable Objects/Player Info")]
public class PlayerInfoSO : ScriptableObject
{
    private PlayerCharacter playerCharacter;
    private string playerName;

    public PlayerCharacter PlayerCharacter 
    {
        get => playerCharacter;
        set => playerCharacter = value;
    }

    public string PlayerName 
    {
        get => playerName;
        set => playerName = value;
    }
}
