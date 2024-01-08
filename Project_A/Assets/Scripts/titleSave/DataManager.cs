using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Datas
{
    public int maxHP = 3;
    public int skillLV = 1;
    public bool bossAClear = false;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public Datas datas;  // ----------------------------------------------- A
                                                                        
    private string KeyName = "Datas";
    private string fileName = "SaveData.txt";

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DataLoad();
    }


    public void DataSave() 
    {
        // Datas 클래스에 있는 모든 변수값을 저장
        ES3.Save(KeyName,datas);  // <-------------------------------------- A
    }
    public void DataLoad()
    {
        if (ES3.FileExists(fileName))
        {
            ES3.LoadInto(KeyName, datas);
        }
        else
        {
            DataSave();
        }
    }
}
