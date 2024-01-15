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

        // UIManager 인스턴스를 찾습니다.
        UIManager uiManager = FindObjectOfType<UIManager>();


        if (itemValue == 3) // itemValue가 어느 시점에서 올라가냐를 생각해보면 됨(++가 조건 이후에 증가가 되고 있으니까!!)
        {
            Debug.Log("퀘스트 클리어");

        }
        else 
        {
            // UIManager의 RefreshItemCounter 메서드를 호출합니다.
            if (uiManager != null)
            {
                uiManager.RefreshItemCounter();
            }
            else
            {
                Debug.LogError("UIManager를 찾을 수 없습니다.");
            }

            questValue.text = itemValue.ToString(); // setactive에서 false해서 밑에 update에 썼을 때는 카운팅만 되고 ui창에 표시X
            gameObject.SetActive(false);
            Debug.Log("먹었당~");
            

        }


    }

}

