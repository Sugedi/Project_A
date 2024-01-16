using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro ����� ����ϱ� ���� �� ���ӽ����̽��� �����մϴ�

public class UIManager : MonoBehaviour
{
    public GameObject questIconButton; // ����Ʈ ������ ��ư
    public GameObject uiElement1;
    public GameObject uiElement2;
    public GameObject uiElement3;

    private void Start() // ���� ���� �� ȣ��Ǵ� Start �Լ�
    {
        questIconButton.SetActive(false); // ��ȭ �˾��� �ʱ� ����(��Ȱ��ȭ ����)�� ����
    }


    // 'Next Button'�� Ŭ���Ǿ��� �� ȣ��Ǵ� �޼���
    public void ShowQuestIcon()
    {
        questIconButton.SetActive(true); // ����Ʈ ������ ��ư�� Ȱ��ȭ
    }

    public void DestroyUIElements()
    {
        // �� UI ��Ҹ� �ı��Ѵ�.
        Destroy(uiElement1);
        Destroy(uiElement2);
        Destroy(uiElement3);
    }

}