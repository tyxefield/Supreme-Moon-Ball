using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ANIM_Data", menuName = "SMB/ANIM_Data")]
public class ANIM_Data : ScriptableObject
{
    [Header("General")]
    public RuntimeAnimatorController melvin;
    public RuntimeAnimatorController plHand;
    [Header("GUI")]
    public RuntimeAnimatorController GUI_YouDied;
    public RuntimeAnimatorController GUI_GO;
    public RuntimeAnimatorController GUI_Head;
    public RuntimeAnimatorController GUI_LeftTextSpam;
    public RuntimeAnimatorController GUI_RightTextSpam;
    [Header("Platforms")]
    public RuntimeAnimatorController PLAT_Xmove;


}
