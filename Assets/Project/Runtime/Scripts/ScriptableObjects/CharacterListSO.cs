using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Character List", menuName = "Scriptable Objects/Character List")]
public class CharacterListSO : ScriptableObject
{
    [SerializeField] private List<GameObject> characters;

    public List<GameObject> Characters => characters;
}
