public enum Cards
{
    Swordsman,
    Crossbowman,
    Healing,
    Rage,
    FireExplosion,
    IceBlast
}
public enum Scenes
{
    Main_Menu,
    Level1
}
public enum Language
{
    English,
    Russian,
    German
}
public enum ScreenName 
{ 
    Start,
    Castle,
    Campaign,
    InGame,
    Shop,
    Collection,
    Deck, 
    Levels,
    Library
}
public enum WindowName
{
    Castle_Mails,
    Castle_Mails_Messenger,
    Castle_Options,
    Castle_Profile,
    Castle_Profile_Edit,
    Castle_Profile_Friends,
    Castle_Shop_Cards,
    Castle_Shop_Resources,

    Levels_Start,

    Current,

    Win,
    Lose
}

public enum LocalizationButtonKey
{
    Play,
    Exit,
    Shop,
    Settings
}
public enum DataPrimaryKey
{
    Mana,
    Global,
    Units,
    EnemyUnits,
    Enhancements,
    Spells
}
public enum DataFieldKey
{
    //-----Mana-----//
    ManaRegenerationRate,
    ManaPerTick,
    ManaStartValue,

    //-----Global-----//
    MaxNumCardsInHand,
    MeleeAtackRange,
    MinAttackSpeed,

    //-----Units-----//
    /*-----Swordsman------*/
    SwordsmanHealth, 
    SwordsmanDamage,
    SwordsmanAttackSpeed,
    SwordsmanAttackDistance,
    SwordsmanDamageAbsorption,
    SwordsmanManaCost,
    /*-----Crossbowman-----*/
    CrossbowmanHealth,
    CrossbowmanDamage,
    CrossbowmanAttackSpeed,
    CrossbowmanArrowSpeed,
    CrossbowmanArmorPenetration,
    CrossbowmanManaCost,

    //-----EnemyUnits-----//
    /*-----Skeleton-----*/
    SkeletonHealth,
    SkeletonDamage,
    SkeletonAttackSpeed,
    SkeletonAttackDistance,
    SkeletonSpeed,
    SkeletonArmor,

    //-----Enhancements-----//
    /*-----Heal-----*/
    HealQuantity,
    HealCardManaCost,
    /*-----Rage-----*/
    RageAttackSpeed,
    RageDuration,
    RageCardManaCost,

    //-----Spells-----//
    /*-----FireExplosion-----*/
    FireExplosionWidth,
    FireExplosionHeight,
    FireExplosionDamage,
    FireExplosionDOTDamage,
    FireExplosionDuration,
    FireExplosionManaCost,
    /*-----IceBlast-----*/
    IceBlastWidth,
    IceBlastHeight,
    IceBlastDamage,
    IceBlastSpeedDebuff,
    IceBlastDuration,
    IceBlastManaCost
}