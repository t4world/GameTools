public enum GlobalEvents : int
{
    ChangeHp = 1, //��ɫѪ���ı�
    ChangeLevel,  //��ɫ�ȼ��ı�
    ChangeForce,
    Death,        //��ɫ����
    GetItem,      //��ɫ��õ���
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



