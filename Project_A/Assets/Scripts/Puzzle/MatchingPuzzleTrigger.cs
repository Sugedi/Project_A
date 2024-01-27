using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MatchingPuzzleTrigger : MonoBehaviour
{
    // MatchingPuzzleTrigger 클래스에 필요한 변수정의
    public MatchingPuzzle matchingPuzzle;    // MatchingPuzzle 스크립트에 접근할 수 있는 변수
    GameObject targetObject;    // 퍼즐을 해결하기 위해 상호작용할 대상 오브젝트
    PuzzleDoor puzzleDoor;     // 퍼즐을 해결했을 때 동작할 문을 나타내는 변수

    // Start 함수는 스크립트가 실행될 때 호출되며 초기화 작업을 수행합니다.
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

    // OnTriggerEnter 함수는 Collider가 트리거 영역에 진입했을 때 호출됩니다.
    void OnTriggerEnter(Collider other)
    {
        // 만약 충돌한 오브젝트가 targetObject와 일치한다면,
        if (other.gameObject == targetObject)
        {
            // 퍼즐 문의 상태를 변경하는 함수를 호출합니다.
            puzzleDoor.ChangeBool(matchingPuzzle);
        }
    }

    // OnTriggerExit 함수는 Collider가 트리거 영역을 벗어났을 때 호출됩니다.
    private void OnTriggerExit(Collider other)
    {
        // 만약 충돌한 오브젝트가 targetObject와 일치한다면,
        if (other.gameObject == targetObject)
        {
            // 퍼즐 문의 상태를 변경하는 함수를 호출합니다.
            puzzleDoor.ChangeBool(matchingPuzzle);
        }
    }
}
