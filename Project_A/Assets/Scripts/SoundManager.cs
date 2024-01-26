using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    // 먼저 다른 스크립트에서 접근하기 쉽게 싱글톤 패턴으로 만들어 줌
    // 어디에서나 쓸 수 있도록 정적 메모리에 담기 위한 instance 변수 선언
    public static SoundManager instance;

    [Header("#BGM")]
    // 배경음과 관련된 클립, 볼륨, 오디오소스 변수 선언
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;

    [Header("#SFX")]
    // 효과음과 관련된 클립, 볼륨, 오디오소스 변수 선언
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels; // 다량의 효과음을 낼 수 있도록 채널 개수 변수 선언
    AudioSource[] sfxPlayers;
    int channelIndex; // 지금 현재 재생하고 있는 채널의 인덱스가 몇 번째인지?

    public enum Sfx { BoxOpen_1, Button2, Button1, Door1, Door2, PlayerFootStep, PlayerRoll_1, PlayerRoll_2, Reload_1, Reload_2, Reloda_3, Shoot_1, Shoot_2}

    private void Awake()
    {
        instance = this;
        Init();
    }

    void Init() 
    {
        // 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer"); // 이름까지 지정해주려면 매개변수 안에다가 "이름" 지정해주면 됨
        bgmObject.transform.parent = transform;
        // AddComponent 함수로 오디오소스를 생성하고 변수에 저장
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;


        // 효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SfxPlayer"); // 이름까지 지정해주려면 매개변수 안에다가 "이름" 지정해주면 됨
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels]; // 채널 값을 사용하여 오디오소스 배열 초기화

        for (int index=0; index < sfxPlayers.Length; index++)
        {
            // 반복문으로 모든 효과음 오디오소스 생성하면서 저장
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
        }
    }

    public void PlaySfx(Sfx sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;
        }
        sfxPlayers[0].clip = sfxClips[(int)sfx];
        sfxPlayers[0].Play();
    }

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);

    //        // 씬이 이동되면 생성한 BgSoundPlay 메서드를 호츌하게 만들어 줌
    //        // 이건 SceneManager 클래스의 sceneLoaded 이벤트를 통해서 가능함
    //        // 이거 하려면 Dev GomDol님 영상에서 씬매니저 클래스에 어떤게 들어가는지 찾아보거나
    //        // GPT에게 물어서 따로 하는 수 밖에 없는 듯
    //        SceneManager.sceneLoaded += OnSceneLoaded;

    //        // bglist에서 오디오 클립과 씬 이름을 매핑하여 딕셔너리를 초기화
    //        bgDictionary = new Dictionary<string, AudioClip>();
    //        foreach (var clip in bglist)
    //        {
    //            // bglist에 있는 클립의 이름은 씬의 이름과 정확히 일치해야 합니다.
    //            bgDictionary[clip.name] = clip;
    //        }
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //// 씬이 로드될 때 호출되는 메서드
    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    //{
    //    // 딕셔너리를 사용하여 현재 씬에 맞는 오디오 클립 찾기
    //    if (bgDictionary.TryGetValue(scene.name, out AudioClip clip))
    //    {
    //        BgSoundPlay(clip);
    //    }
    //}


    //public void SFXPlay(string sfxName, AudioClip clip) // 매개변수로 이름을 받아와서 생성한 오브젝트에 붙여주기
    //{
    //    GameObject go = new GameObject(sfxName + "Sound");
    //    AudioSource audiosource = go.AddComponent<AudioSource>();
    //    audiosource.clip = clip;
    //    audiosource.Play();

    //    // 효과음 재생이 끝나면 생성했던 오브젝트가 파괴될 수 있게 Destroy 사용
    //    // Destroy(파괴할 오브젝트, 지연시간);
    //    // 두 번째 인자 값을 적어주면 해당 시간이 지난 후에 오브젝트가 파괴됨
    //    Destroy(go, clip.length);

    //    //이렇게까지 적고 나서 효과음을 넣고 싶은 행위가 이루어지는 스크립트로 가서
    //    // 해당 행위가 이루어지는 부분에 재생할 클립을 넘겨주기
    //    // SoundManager.instance.SFXPlay("Hook",clip);
    //    // 그 스크립트에 변수 public AudioClip clip; 적어주기
    //}


    //public void BgSoundPlay(AudioClip clip)
    //{
    //    bgSound.clip = clip; // 오디오소스에 클립을 넣어주기
    //    bgSound.loop = true; // 계속 반복재생 하게끔 루프는 참으로 해줌
    //    bgSound.volume = 0.1f;
    //    bgSound.Play();
    //}
}

