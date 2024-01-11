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

    // ��ź ��� ��ų�� ���� ���ο� �Ӽ�
    public bool isBuckShot = false; // ��ź ��� ��ų Ȱ��ȭ �����Դϴ�.
    public int buckShotCount = 3; // �� ���� �߻�Ǵ� �Ѿ��� ���Դϴ�.
    public float buckShotSpreadAngle = 30f; // �Ѿ� ������ �����Դϴ�.

    // �ٸ��μ��� ��ų �Ӽ�
    public bool isLegBreak = false; // �ٸ��μ��� ��ų Ȱ��ȭ ����
    public int legBreakCount = 5; // �߻�� �Ѿ��� ��
    public float legBreakSpreadAngle = 45f; // �Ѿ� ������ ����    
}