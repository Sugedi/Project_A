using UnityEngine;
using UnityEngine.UI; // UI 네임스페이스를 사용합니다.

[RequireComponent(typeof(Button))] // 이 스크립트는 Button 컴포넌트가 필요합니다.
public class ButtonSound : MonoBehaviour
{
    public string soundEffectName; // 인스펙터에서 설정할 수 있도록 public으로 선언합니다.

    private Button button; // 버튼 컴포넌트에 대한 참조

    void Start()
    {
        button = GetComponent<Button>(); // 이 게임 오브젝트에 부착된 Button 컴포넌트를 가져옵니다.
        button.onClick.AddListener(PlaySound); // 버튼의 OnClick 이벤트에 PlaySound 메서드를 연결합니다.
    }

    private void PlaySound()
    {
        if (SoundManager.instance != null && !string.IsNullOrEmpty(soundEffectName))
        {
            SoundManager.instance.PlayAudio(soundEffectName, "SE"); // SoundManager를 사용하여 사운드 효과를 재생합니다.
        }
        else
        {
            Debug.LogError("SoundManager 인스턴스를 찾을 수 없거나 soundEffectName이(가) 비어 있습니다.");
        }
    }
}
