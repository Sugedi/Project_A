using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ��ũ��Ʈ�� ��ų �����͸� �����ϱ� ���� ScriptableObject�Դϴ�.
// ScriptableObject�� ������ �����̳� ������ �Ͽ� ���� ������Ʈ�� ���� �ʰ� �����͸� ������ �� �ֽ��ϴ�.
[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]

// CreateAssetMenu :�� ��Ʈ����Ʈ�� ����ϸ� ����Ƽ �������� �޴��� ���ο� �ɼ��� �߰��� �� �ֽ��ϴ�.
// fileName : ������ �� ������ �⺻ ���� �̸��� �����մϴ�. ���氡��
// menuName : ����Ƽ �������� �޴����� �� ������ ������ �� �ִ� �ɼ��� �̸��� �����մϴ�.

public class Skill : ScriptableObject
{
    // ��ų�� �̸��� �����մϴ�.
    // ���� ��� "Power Boost", "Speed Surge" �� ��ų�� �̸��� ���⿡ �����մϴ�.
    public string skillName;

    // ��ų�� ����� �� �÷��̾� �Ǵ� ������ �������� �������� �����Դϴ�.
    // ���� ��ų�� ������ ������ ���⼭ ���°� �ƴϰ� ������ ���� �װſ� ����
    public float damageMultiplier = 1f;

    // ��ų�� ����� �� �÷��̾� �Ǵ� ������ ���� �ӵ��� �������� �����Դϴ�.
    // ���� ��ų�� ������ ������ ���⼭ ���°� �ƴϰ� ������ ���� �װſ� ����
    public float attackSpeedMultiplier = 1f;

    // ������ �ð� ������ �߰��մϴ�.
    public float reloadTimeMultiplier = 1f;

    // źâ ������ �߰�
    public int ammoIncrease;

    // ���� ��ų�� ���� ���ο� �Ӽ�
    public bool isShotGun1 = false; // ����1 ��ų Ȱ��ȭ �����Դϴ�.
    public int shotGun1Count = 2; // �� ���� �߻�Ǵ� �Ѿ��� ���Դϴ�.
    public float shotGun1SpreadAngle = 45f; // �Ѿ� ������ �����Դϴ�.
    
    public bool isShotGun2 = false; // ����2 ��ų Ȱ��ȭ ����
    public int shotGun2Count = 3; // �߻�� �Ѿ��� ��
    public float shotGun2SpreadAngle = 45f; // �Ѿ� ������ ����

    public bool isShotGun3 = false; // ����3 ��ų Ȱ��ȭ ����
    public int shotGun3Count = 4; // �߻�� �Ѿ��� ��
    public float shotGun3SpreadAngle = 45f; // �Ѿ� ������ ����   

    public bool isShotGun4 = false; // ����4 ��ų Ȱ��ȭ ����
    public int shotGun4Count = 5; // �߻�� �Ѿ��� ��
    public float shotGun4SpreadAngle = 45f; // �Ѿ� ������ ����

    // ���뼦 ��ų Ȱ��ȭ �����Դϴ�.
    public bool isPierceShot = false;
}