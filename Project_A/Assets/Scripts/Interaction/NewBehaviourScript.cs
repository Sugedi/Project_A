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

        // UIManager �ν��Ͻ��� ã���ϴ�.
        UIManager uiManager = FindObjectOfType<UIManager>();


        if (itemValue == 3) // itemValue�� ��� �������� �ö󰡳ĸ� �����غ��� ��(++�� ���� ���Ŀ� ������ �ǰ� �����ϱ�!!)
        {
            Debug.Log("����Ʈ Ŭ����");

        }
        else 
        {
            // UIManager�� RefreshItemCounter �޼��带 ȣ���մϴ�.
            if (uiManager != null)
            {
                uiManager.RefreshItemCounter();
            }
            else
            {
                Debug.LogError("UIManager�� ã�� �� �����ϴ�.");
            }

            questValue.text = itemValue.ToString(); // setactive���� false�ؼ� �ؿ� update�� ���� ���� ī���ø� �ǰ� uiâ�� ǥ��X
            gameObject.SetActive(false);
            Debug.Log("�Ծ���~");
            

        }


    }

}

