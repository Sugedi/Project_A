using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class AudioInformation
{
    public string name; // 오디오 이름
    public float volume; // 오디오 볼륨
    public bool isLoop; // 반복 재생 여부 판단
    public AudioClip clip; // 오디오 파일
}

//오디오를 관리 하는 클래스(싱글톤) - 배경음악, 효과음, 음성을 따로 관리
public class SoundManager : MonoBehaviour
{
    //싱글톤 선언 준비
    public static SoundManager instance;
    [SerializeField]
    private AudioInformation[] bgmInfo = null; // 배경음악 정보
    private AudioSource bgmPlayer;
    private int currentBGMIndex; // 현재 플레이 중이 배경음악 인덱스

    [SerializeField] AudioInformation[] seInfo = null;//효과음정보
    private List<AudioSource> sePlayer; // 효과음 오디오 플레이어

    private AudioSource voicePlayer;
    [SerializeField] float defaultVolume = 1f;

    // 싱글톤을 위한 초기화 과정
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;  // 이벤트 리스너 추가

        // instance 가 null인 상태라면
        if (instance == null)
        {
            instance = this; // 인스턴스는 자기자신
            DontDestroyOnLoad(gameObject); // 다른씬에서도 사용 할 수 있도록 지정
        }
        else
        {
            Destroy(gameObject); // 인스턴스가 이미 생성 중이라면 파괴하고 새로 만들기
        }

        bgmPlayer = this.gameObject.AddComponent<AudioSource>();
        voicePlayer = this.gameObject.AddComponent<AudioSource>();
        sePlayer = new List<AudioSource>();
        for (int i = 0; i < seInfo.Length; i++)
        {
            sePlayer.Add(this.gameObject.AddComponent<AudioSource>());
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // AudioSource 컴포넌트의 null 체크
        for (int i = 0; i < sePlayer.Count; i++)
        {
            if (sePlayer[i] == null)
            {
                Debug.LogError("AudioSource component on SoundManager is null after scene load.");
            }
        }

        // 'Loading' 씬, 'LoadingBackstage'씬, 'LoadingStage' 씬을 처리하는 경우를 제외해야 하므로,
        // 'Loading' 씬, 'LoadingBackstage'씬, 'LoadingStage' 씬 이름을 체크하는 조건문을 추가합니다.
        if (scene.name == "Loading" || scene.name == "LoadingBackstage" || scene.name == "LoadingStage")
        {
            // 'Loading' 씬, 'LoadingBackstage', 'LoadingStage' 씬에서는 아무 작업도 하지 않습니다.
            return;
        }

        switch (scene.name)
        {
            case "01_Title": // 여기다가는 씬의 이름 적기
                PlayBGM("01_Title");  // 씬1에 해당하는 배경음악 이름
                break;
            case "Backstage_0114": // 여기다가는 씬의 이름 적기
                PlayBGM("Backstage_0114");  // 씬2에 해당하는 배경음악 이름                
                break;
            case "Stage": // 여기다가는 씬의 이름 적기
                PlayBGM("Stage");  // 씬3에 해당하는 배경음악 이름                
                break;
            default:
                // 로딩 씬들이 아니고, 다른 씬 이름이 잘못되었을 때만 오류를 로그합니다.
                Debug.LogError("Unknown scene name: " + scene.name);
                break;
        }
    }

    private void PlayBGM(string p_name)
    {
        // bgmInfo의 배열만큼 반복 재생
        for (int i = 0; i < bgmInfo.Length; i++)
        {
            if (p_name == bgmInfo[i].name)
            {
                // bgminfo에 담겨 있는 오디오 클립을 재생하고 반복문을 빠져나간다
                bgmPlayer.clip = bgmInfo[i].clip;
                bgmPlayer.volume = bgmInfo[i].volume;
                bgmPlayer.loop = bgmInfo[i].isLoop;
                bgmPlayer.Play();
                return;
            }
        }
        Debug.LogError(p_name + "에 해당하는 배경음악이 없습니다.");
    }

    private void StopBGM() // BGM 정지 하는 함수
    {
        bgmPlayer.Stop();
    }
    private void PauseBGM() // BGM 멈추는 함수
    {
        bgmPlayer.Pause();
    }
    private void UnPauseBGM() // BGM 멈춘 상태를 해제 하는 함수
    {
        bgmPlayer.UnPause();
    }

    private void PlaySE(string p_name) // 효과음 재생을 파라미터로 이름을 받는다
    {
        bool isPlayed = false; // 효과음 재생 여부를 체크하는 플래그
        for (int i = 0; i < seInfo.Length; i++)
        {
            if (p_name == seInfo[i].name)
            {
                for (int j = 0; j < sePlayer.Count; j++)
                {
                    if (!sePlayer[j].isPlaying)
                    {
                        sePlayer[j].clip = seInfo[i].clip;
                        sePlayer[j].volume = seInfo[i].volume;
                        sePlayer[j].PlayOneShot(sePlayer[j].clip);
                        isPlayed = true;
                        break; // 효과음 재생 후 반복문 종료
                    }
                }
                if (isPlayed) break; // 효과음이 재생되었다면 더 이상의 확인은 필요 없음
            }
        }
        if (!isPlayed)
        {
            Debug.LogError("SE " + p_name + " could not be played. All channels are busy or SE not found.");
        }
    }

    private void StopAllSE() // 재생중인 모든 효과음을 멈춘다
    {
        // 효과음의 개수만큼 반복 실행
        for (int i = 0; i < sePlayer.Count; i++)
        {
            sePlayer[i].Stop(); // 효과음 재생 정지
        }
    }

    // 이하는 외부에서 호출 하는 메소드 입니다
    // 파일이름과 어떤 타입의 오디오인지를 인수로 넘겨 받는다

    ///
    ///p_Type : BGM -> BGM 배경음악 재생
    ///p_Type : SE -> SE 효과음 재생
    ///p_Type : VOICE -> Voice 음성 재생 (우리는 안쓸 거 같지만)

    public void PlayAudio(string p_name, string p_type)
    {
        PlayAudio(p_name, p_type, defaultVolume);

    }

    // 볼륨값을 지정한 경우 해당 볼륨으로 재생
    public void PlayAudio(string p_name, string p_type, float volume)
    {
        // 넘겨받은 타입 변수 값에 따라서 해당 플레이어를 재생 시킨다
        if (p_type == "BGM") PlayBGM(p_name);
        else if (p_type == "SE") PlaySE(p_name);
        else Debug.Log("해당 범위의 오디오 플레이어가 없습니다.");
    }

    // 오디오 재생을 정지하는 메소드
    public void StopAudio(string p_type)
    {
        if (p_type == "BGM") StopBGM();
        else if (p_type == "SE") StopAllSE();
        else Debug.Log("해당하는 타입의 오디오 플레이어가 없습니다.");
    }

    // 모든 오디오 재생을 정지하는 함수
    public void StopAllAudio()
    {
        StopBGM();
        StopAllSE();
    }

}
