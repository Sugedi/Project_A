using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target; // 카메라가 따라가야 할 타겟
    public Vector3 offset; // 카메라 위치로 정한 값을 유지하기 위해 사용되는 offset을 public 변수로 선언
                           // 카메라 위치 탑다운이니까 변경하면 inspector 창에서 offset도 값 똑같이 바꿔주세요!

    /* 유니티 창에서 하이어라키 부분 WorldSpace에다가 바닥~벽 다 넣은 이유:
     *      화면 시야를 사각형으로 그대로 두고 쓰면 너무 '정적'이게 보여서
     *      조금 스타일리쉬하게 45도 각도로 틀고자 함 - 골드메탈님 의견
     */


    void Update() // Update 함수는 매 프레임마다 호출되며, 해당 오브젝트(카메라)의 위치를 타겟의 위치에 오프셋을 더한 값으로 업데이트한다.
    {
        transform.position = target.position + offset; // 현재 오브젝트(카메라)의 위치를 타겟의 위치에 오프셋을 더한 값으로 설정한다.
    }
}
