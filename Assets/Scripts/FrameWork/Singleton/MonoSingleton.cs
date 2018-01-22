using UnityEngine;
using System.Collections;

/********************************************************************************
** auth： Zyz
** date： 11/1/2017 4:28:51 PM
** desc： 
*********************************************************************************/
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T m_Instance = null;
    private static bool _isInitialized;
    public static T Instance
    {
        get
        {
            Debug.Log("Instance start");
            if (m_Instance == null)
            {
                m_Instance = GameObject.FindObjectOfType(typeof(T)) as T;
                if (m_Instance == null)
                {
                    m_Instance = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
                    if (m_Instance == null)
                    {
                        Debug.LogError("Problem during the creation of " + typeof(T).ToString());
                    }
                }
                if (!_isInitialized)
                {
                    _isInitialized = true;
                    m_Instance.Init();
                }
            }
            Debug.Log("Instance end");
            return m_Instance;
        }
    }


    private void Awake()
    {
        Debug.Log("Awake start");

        if (m_Instance == null)
        {
            m_Instance = this as T;
        }
        else if (m_Instance != this)
        {
            Debug.LogError("Another instance of " + GetType() + " is already exist! Destroying self...");
            DestroyImmediate(this);
            return;
        }
        if (!_isInitialized)
        {
            DontDestroyOnLoad(gameObject);
            _isInitialized = true;
            m_Instance.Init();
        }
        Debug.Log("Awake End");

    }

    public static void DestroyInstance()
    {
        if (m_Instance != null)
        {
            Object.Destroy(m_Instance.gameObject);
        }
        m_Instance = null;
    }

    public virtual void Init() { }

    private void OnApplicationQuit()
    {
        m_Instance = null;
    }
}