using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MatchingPuzzleTrigger : MonoBehaviour
{
    // MatchingPuzzleTrigger Ŭ������ �ʿ��� ��������
    public MatchingPuzzle matchingPuzzle;    // MatchingPuzzle ��ũ��Ʈ�� ������ �� �ִ� ����
    GameObject targetObject;    // ������ �ذ��ϱ� ���� ��ȣ�ۿ��� ��� ������Ʈ
    PuzzleDoor puzzleDoor;     // ������ �ذ����� �� ������ ���� ��Ÿ���� ����

    // Start �Լ��� ��ũ��Ʈ�� ����� �� ȣ��Ǹ� �ʱ�ȭ �۾��� �����մϴ�.
    private void Start()
    {
        if (matchingPuzzle != null)
        {
            targetObject = matchingPuzzle.target;
            puzzleDoor = matchingPuzzle.door;
        }
        else
        {
            // Log an error or handle the case where matchingPuzzle is null
            Debug.LogError("matchingPuzzle initiated.");
        }
    }

    // OnTriggerEnter �Լ��� Collider�� Ʈ���� ������ �������� �� ȣ��˴ϴ�.
    void OnTriggerEnter(Collider other)
    {
        // ���� �浹�� ������Ʈ�� targetObject�� ��ġ�Ѵٸ�,
        if (other.gameObject == targetObject)
        {
            // ���� ���� ���¸� �����ϴ� �Լ��� ȣ���մϴ�.
            puzzleDoor.ChangeBool(matchingPuzzle);
        }
    }

    // OnTriggerExit �Լ��� Collider�� Ʈ���� ������ ����� �� ȣ��˴ϴ�.
    private void OnTriggerExit(Collider other)
    {
        // ���� �浹�� ������Ʈ�� targetObject�� ��ġ�Ѵٸ�,
        if (other.gameObject == targetObject)
        {
            // ���� ���� ���¸� �����ϴ� �Լ��� ȣ���մϴ�.
            puzzleDoor.ChangeBool(matchingPuzzle);
        }
    }
}
