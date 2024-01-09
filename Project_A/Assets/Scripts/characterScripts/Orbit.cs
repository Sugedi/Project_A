using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform target; // 타겟(Transform) 오브젝트
    public float orbitSpeed; // 공전 속도
    Vector3 offSet; // 초기 위치와 타겟 간의 오프셋

    void Start()
    {
        offSet = transform.position - target.position; // 초기 위치와 타겟 간의 오프셋 계산
    }

    void Update()
    {
        transform.position = target.position + offSet; // 현재 위치를 타겟 위치에 오프셋을 더한 값으로 설정
        transform.RotateAround(target.position,
            Vector3.up,orbitSpeed * Time.deltaTime); // 타겟을 중심으로 주어진 속도로 공전
        offSet = transform.position - target.position; // 업데이트 후 위치와 타겟 간의 새로운 오프셋 계산
    }
}