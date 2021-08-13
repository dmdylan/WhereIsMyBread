using UnityEngine;

[CreateAssetMenu(fileName = "New String Object", menuName = "Scriptable Objects/String Object")]
public class StringSO : ScriptableObject
{
    [SerializeField] private string newString;

    public string Value
    {
        get => newString;
        set => newString = value;
    }
}
