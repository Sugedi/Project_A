using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    float range; // 습득 가능한 최대 거리
    [SerializeField]
    bool pickupActivated = false; // 습득 가능할 시 true
    [SerializeField]
    RaycastHit hitInfo; // 충돌체 정보 저장

    // 아이템 레이어에만 반응하도록 레이어 마스크를 설정
    [SerializeField]
    LayerMask layerMask;

    void Update()
    {
        CheckItem();
        TryAction();
    }
    void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.P)) // P 키를 눌렀을 때
        {
            CheckItem();
            CanPickUp();
        }
    }
    void CanPickUp()
    {
        if(pickupActivated)
        {
            if(hitInfo.transform != null) // hitInfo 가 하이어라이에 존재 하는지 한번 더 검사
            {
                // ==============================================================
                // 실제로 데이터 저장 코드로 수정해야함 - 효과 적용 코드로 삽입을 해줘야함
                // ==============================================================


                // hitInfo.transform.GetComponent<ItemPickUp>().item.itemName
                // hitInfo라는애 -> ItemPickUp 클래스 접근 -> 거기에서 item이라는 애 접근 -> item은 ItemDrop 클래스 상속 -> 그래서 itemName (얘가 있음)을 가져와라
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "획득했습니다");

                // 획득을 했으니 그 아이템 오브젝트를 파괴해라
                Destroy(hitInfo.transform.gameObject);
            }
        }
    }

    // 아이템 배치할 때 바닥에서 뜨면 안됨 ㅇㅇ
    void CheckItem()
    {
        // 이거 달린 애 기준으로 레이를 쏘고 // 쏜 애들 중 layerMask랑 일치하는 애인지 여부에 따라 true/false 반환 // range만큼의 거리
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask))
        {
            if(hitInfo.transform.tag == "Item")
            {
                pickupActivated = true;
            }
        }
    }

}