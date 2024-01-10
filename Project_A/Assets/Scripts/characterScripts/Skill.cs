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
}