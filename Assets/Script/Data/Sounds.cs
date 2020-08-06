using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sounds", menuName = "SMB/Sounds")]
public class Sounds : ScriptableObject
{
    [Header("General")]
    public AudioClip SFX_Fall;
    public AudioClip SFX_Bounce;
    public AudioClip SFX_Roll;
    public AudioClip SFX_Explosion;
    [Header("OSt")]
    public AudioClip OST_Game;
    public AudioClip OST_GameOver;
    public AudioClip OST_Victory;
    [Header("Voice")]
    public AudioClip VOC_Fall;
    public AudioClip VOC_Hit;
    public AudioClip VOC_Go;

}
