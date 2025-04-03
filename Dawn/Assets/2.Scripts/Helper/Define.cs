using UnityEngine;

public static class Define 
{

}

#region EnumSet
public enum BGMType
{
    BGM_Title,
    BGM_Tutorial,
    BGM_Boss,
}

public enum SFXType
{
    SFX_Human_Foot_Grass,
    SFX_Human_Foot_Grass2,
    SFX_Human_Men_Death,
    SFX_Human_Men_Hurt,
    SFX_Human_Women_Breath,
    SFX_Human_Women_Death,
    SFX_Human_Women_gasp,
    SFX_Human_Women_HaHa,
    SFX_Human_Women_Hurt,
    SFX_Human_Women_Hurt2,
    SFX_UI_Accept,
    SFX_UI_Click,
    SFX_UI_Error,
    SFX_UI_Return,
    SFX_UI_Rover,
    SFX_Weapon_clash1,
    SFX_Weapon_clash2,
    SFX_Weapon_clash3,
    SFX_Weapon_clash4,
    SFX_Weapon_clash5,
    SFX_Weapon_shine,
    SFX_Weapon_whoosh,
    SFX_Weapon_whoosh2,
    SFX_Item_Equipped,
}

public enum GroundMoverType
{
    UpGround,
    RightGround,
}

public enum PlayerEventType
{
    Tutorial_Input_Arrow,
    Tutorial_Input_Jump,

}
#endregion
