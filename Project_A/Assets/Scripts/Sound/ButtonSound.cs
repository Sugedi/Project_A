using UnityEngine;
using UnityEngine.UI; // UI ���ӽ����̽��� ����մϴ�.

[RequireComponent(typeof(Button))] // �� ��ũ��Ʈ�� Button ������Ʈ�� �ʿ��մϴ�.
public class ButtonSound : MonoBehaviour
{
    public string soundEffectName; // �ν����Ϳ��� ������ �� �ֵ��� public���� �����մϴ�.

    private Button button; // ��ư ������Ʈ�� ���� ����

    void Start()
    {
        button = GetComponent<Button>(); // �� ���� ������Ʈ�� ������ Button ������Ʈ�� �����ɴϴ�.
        button.onClick.AddListener(PlaySound); // ��ư�� OnClick �̺�Ʈ�� PlaySound �޼��带 �����մϴ�.
    }

    private void PlaySound()
    {
        if (SoundManager.instance != null && !string.IsNullOrEmpty(soundEffectName))
        {
            SoundManager.instance.PlayAudio(soundEffectName, "SE"); // SoundManager�� ����Ͽ� ���� ȿ���� ����մϴ�.
        }
        else
        {
            Debug.LogError("SoundManager �ν��Ͻ��� ã�� �� ���ų� soundEffectName��(��) ��� �ֽ��ϴ�.");
        }
    }
}
