using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bread Damaged SO", menuName = "Scriptable Objects/Bread Damaged")]
public class BreadDamagedSO : ScriptableObject
{
    [SerializeField] private float damagedSpeedMultiplier;
    [SerializeField] private float damagedSpeedTimeBeforeDecay;
    [SerializeField] private float damagedSpeedDecayTime;

    public float DamagedSpeedMultiplier => damagedSpeedMultiplier;
    public float DamagedSpeedTimeBeforeDecay => damagedSpeedTimeBeforeDecay;
    public float DamagedSpeedDecayTime => damagedSpeedDecayTime;
}
