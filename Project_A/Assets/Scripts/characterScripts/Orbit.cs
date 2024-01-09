using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform target; // Ÿ��(Transform) ������Ʈ
    public float orbitSpeed; // ���� �ӵ�
    Vector3 offSet; // �ʱ� ��ġ�� Ÿ�� ���� ������

    void Start()
    {
        offSet = transform.position - target.position; // �ʱ� ��ġ�� Ÿ�� ���� ������ ���
    }

    void Update()
    {
        transform.position = target.position + offSet; // ���� ��ġ�� Ÿ�� ��ġ�� �������� ���� ������ ����
        transform.RotateAround(target.position,
            Vector3.up,orbitSpeed * Time.deltaTime); // Ÿ���� �߽����� �־��� �ӵ��� ����
        offSet = transform.position - target.position; // ������Ʈ �� ��ġ�� Ÿ�� ���� ���ο� ������ ���
    }
}