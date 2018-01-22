using System;
using System.Collections.Generic;
using UnityEngine;


/********************************************************************************
** auth： Zyz
** date： 11/1/2017 2:50:42 PM
** desc： 
*********************************************************************************/
public class TestSingletonOne : Singleton<TestSingletonOne>
{
    public void Test()
    {
        Debug.Log("TestSingletonOne..........");
    }

    public override void Init()
    {
        Debug.Log("TestSingletonOne Init");
    }

    public override void Reset()
    {
        Debug.Log("TestSingletonOne Reset");
    }
}

public class TestSingletonTwo : Singleton<TestSingletonTwo>
{
    public void Test()
    {
        Debug.Log("TestSingletonTwo..........");
    }


    public override void Init()
    {
        Debug.Log("TestSingletonTwo Init");
    }

    public override void Reset()
    {
        Debug.Log("TestSingletonTwo Reset");
    }
}

public class TestSingletonThree : MonoSingleton<TestSingletonThree>
{
    public void Test()
    {
        Debug.Log("TestSingletonThree..........");
    }

    public void Test2()
    {
        Debug.Log("TestSingletonThree..........");
    }

    public override void Init()
    {
        Debug.Log("TestSingletonThree Init");
    }


}
