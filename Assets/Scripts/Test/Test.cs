using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FrameWork;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        zyz.GccRandom random = new zyz.GccRandom();
        random.SetRandomSeed(1000);
        for (int i = 0; i < 10; i++)
        {
            //Debug.Log(random.GetRandom(100));
        }
        zyz.GccRandom random2 = new zyz.GccRandom();
        random2.SetRandomSeed(1000);
        for (int i = 0; i < 10; i++)
        {
            //Debug.LogError(random2.GetRandom(100));
        }
        zyz.PrimeSearch search = new zyz.PrimeSearch(100);
        int count = 0;
        int resrult = 0;
        List<int> list = new List<int>();

        while( (resrult = search.GetNext()) != -1)
        {
            count++;
            list.Add(resrult);
            //Debug.Log(resrult);
        }
        //Debug.LogError("count" + count);
        list.Sort();
        foreach (var item in list)
        {
            //Debug.LogError(item);
        }
        //Singleton<TestSingletonOne>.Instance.Test();
        //Singleton<TestSingletonOne>.DestroyInstance();
        //Singleton<TestSingletonOne>.Instance.Init();
       // Singleton<TestSingletonOne>.Instance.Reset();


        //Singleton<TestSingletonTwo>.Instance.Test();
        //Singleton<TestSingletonTwo>.DestroyInstance();
        //MonoSingleton<TestSingletonThree>.Instance.Test();
        //MonoSingleton<TestSingletonThree>.Instance.Test2();

        //Singleton<TestSingletonTwo>.Instance.Init();
        //Singleton<TestSingletonTwo>.Instance.Reset();
        LoggerHelper.Debug("Test........");
        Debug.Log("Debug Test");

	}
    int i = 0;
	// Update is called once per frame
	void Update () {
	    
        if(Input.GetMouseButtonUp(0))
        {
            Debug.LogError("GetMouseButtonUp" + i);
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Debug.LogError("GetMouseButtonDown" + i);
        }
        else if (Input.GetMouseButton(0))
        {
            Debug.LogError("GetMouseButton" + i);
        }
        i++;
	}
}


