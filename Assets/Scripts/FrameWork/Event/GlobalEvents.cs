public enum GlobalEvents : int
{
    ChangeHp = 1, //角色血量改变
    ChangeLevel,  //角色等级改变
    ChangeForce,
    Death,        //角色死亡
    GetItem,      //角色获得道具
    GetTask,      
    FinishTask,
    OpenFunction,
    GetActivity,
    EnterInstance,
    LeaveInstance,
    OpenGUI,
    ButtonClick,
    SkillAvailable,
    TimeLimitEvent,
    Achiement,
    ArenicCredit
}

public enum SignalEvents : byte
{
    AnyClick = 1,
    ButtonClick =2,
    KillMonster =3,
    ClickItem = 4,
    ArrivePos = 5,
    FinishDialog = 6,
    Sleep = 7,
    StartGuide = 8
}



