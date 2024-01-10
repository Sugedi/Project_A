using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    //private static DontDestroyOnLoad s_Instance = null;

    void Awake()
    {
        //if (s_Instance)
        //{
            //DestroyImmediate(this.gameObject);
            //return;
        //}

        //s_Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

   
}