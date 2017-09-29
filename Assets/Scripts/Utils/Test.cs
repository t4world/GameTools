using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
            Debug.Log(resrult);
        }
        Debug.LogError("count" + count);
        list.Sort();
        foreach (var item in list)
        {
            Debug.LogError(item);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
