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

        if (itemValue == 3) // itemValue�� ��� �������� �ö󰡳ĸ� �����غ��� ��(++�� ���� ���Ŀ� ������ �ǰ� �����ϱ�!!)
        {
            Debug.Log("����Ʈ Ŭ����");

        }
        else 
        {
            questValue.text = itemValue.ToString(); // setactive���� false�ؼ� �ؿ� update�� ���� ���� ī���ø� �ǰ� uiâ�� ǥ��X
            gameObject.SetActive(false);
            Debug.Log("�Ծ���~");

        }


    }

}

