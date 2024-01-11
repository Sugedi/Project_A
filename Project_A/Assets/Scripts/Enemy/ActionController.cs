using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    float range; // ���� ������ �ִ� �Ÿ�
    [SerializeField]
    bool pickupActivated = false; // ���� ������ �� true
    [SerializeField]
    RaycastHit hitInfo; // �浹ü ���� ����

    // ������ ���̾�� �����ϵ��� ���̾� ����ũ�� ����
    [SerializeField]
    LayerMask layerMask;

    void Update()
    {
        CheckItem();
        TryAction();
    }
    void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.P)) // P Ű�� ������ ��
        {
            CheckItem();
            CanPickUp();
        }
    }
    void CanPickUp()
    {
        if(pickupActivated)
        {
            if(hitInfo.transform != null) // hitInfo �� ���̾���̿� ���� �ϴ��� �ѹ� �� �˻�
            {
                // ==============================================================
                // ������ ������ ���� �ڵ�� �����ؾ��� - ȿ�� ���� �ڵ�� ������ �������
                // ==============================================================


                // hitInfo.transform.GetComponent<ItemPickUp>().item.itemName
                // hitInfo��¾� -> ItemPickUp Ŭ���� ���� -> �ű⿡�� item�̶�� �� ���� -> item�� ItemDrop Ŭ���� ��� -> �׷��� itemName (�갡 ����)�� �����Ͷ�
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "ȹ���߽��ϴ�");

                // ȹ���� ������ �� ������ ������Ʈ�� �ı��ض�
                Destroy(hitInfo.transform.gameObject);
            }
        }
    }

    // ������ ��ġ�� �� �ٴڿ��� �߸� �ȵ� ����
    void CheckItem()
    {
        // �̰� �޸� �� �������� ���̸� ��� // �� �ֵ� �� layerMask�� ��ġ�ϴ� ������ ���ο� ���� true/false ��ȯ // range��ŭ�� �Ÿ�
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask))
        {
            if(hitInfo.transform.tag == "Item")
            {
                pickupActivated = true;
            }
        }
    }

}