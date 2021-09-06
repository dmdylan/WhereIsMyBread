using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Ability", menuName = "Scriptable Objects/New Ability")]
public class AbilitySO : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private Sprite iconCD;
    [SerializeField] private AudioClip abilitySFX;
    [SerializeField] private float abilityDelayTimer;
    [SerializeField] private float abilityCooldown;
    [SerializeField] private float abilityDuration;
    [SerializeField] private float abilityEffectFloat;

    public Sprite Icon => icon;
    public Sprite IconCD => iconCD;
    public AudioClip AbilitySFX => abilitySFX;
    public float AbilityDelayTimer => abilityDelayTimer;
    public float AbilityCooldown => abilityCooldown;
    public float AbilityDuration => abilityDuration;
    public float AbilityEffectFloat => abilityEffectFloat;
}
