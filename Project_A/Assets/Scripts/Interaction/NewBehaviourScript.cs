using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI questValue;
    public int itemValue;



    private void OnTriggerEnter(Collider other)
    {
        itemValue++;

        if (itemValue == 3) // itemValue가 어느 시점에서 올라가냐를 생각해보면 됨(++가 조건 이후에 증가가 되고 있으니까!!)
        {
            Debug.Log("퀘스트 클리어");

        }
        else 
        {
            questValue.text = itemValue.ToString(); // setactive에서 false해서 밑에 update에 썼을 때는 카운팅만 되고 ui창에 표시X
            gameObject.SetActive(false);
            Debug.Log("먹었당~");

        }


    }

}

