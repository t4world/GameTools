using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/********************************************************************************
** auth： Zyz
** date： 11/1/2017 2:09:28 PM
** desc： 
*********************************************************************************/
public class Singleton<T> where T : class ,new()
{
    private static T m_instance;

    protected Singleton(){}

    public static void CreateInstance()
    {
        if (m_instance == null)
        {
            m_instance = Activator.CreateInstance<T>();
            (m_instance as Singleton<T>).Init();
        }
        
    }

    public static void DestroyInstance()
    {
        if(m_instance != null)
        {
             (m_instance as Singleton<T>).Reset();
            m_instance = null;
        }
    }

    public virtual void Init()
    {

    }

    public virtual void Reset()
    {

    }

    public static T Instance
    {
        get
        {
            if(m_instance == null)
            {
                CreateInstance();
            }
            return m_instance;
        }
    }
}
