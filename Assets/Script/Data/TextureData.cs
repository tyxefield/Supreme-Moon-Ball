using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextureData", menuName = "SMB/TextureData")]
public class TextureData : ScriptableObject
{
    [Header("Main")]
    public Sprite Shadow;
    public Material Bubble;
    public Sprite plHand;
    public Sprite goal;
    public Sprite score;
    public Sprite fan;
    public PhysicMaterial PYS_Player;
    [Space]
    [Header("HUD")]
    public TMPro.TMP_FontAsset FONT;
    public Sprite GUI_LevelFinishP1;
    public Sprite GUI_LevelFinishP2;
    public Sprite GUI_GameOverP1;
    public Sprite GUI_GameOverP2;
    public Sprite GUI_YouDied;
    public Sprite GUI_Ready;
    public Sprite GUI_Set;
    public Sprite GUI_Go;
    public Sprite GUI_Indicator;
    public Sprite GUI_Scale;
    public Sprite GUIFACE_Melvin;
    public Sprite GUIFACE_MelvinHappy;
    public Sprite GUIFACE_MelvinDeath;

}
