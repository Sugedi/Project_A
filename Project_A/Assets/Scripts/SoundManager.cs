using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    // ���� �ٸ� ��ũ��Ʈ���� �����ϱ� ���� �̱��� �������� ����� ��
    // ��𿡼��� �� �� �ֵ��� ���� �޸𸮿� ��� ���� instance ���� ����
    public static SoundManager instance;

    [Header("#BGM")]
    // ������� ���õ� Ŭ��, ����, ������ҽ� ���� ����
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;

    [Header("#SFX")]
    // ȿ������ ���õ� Ŭ��, ����, ������ҽ� ���� ����
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels; // �ٷ��� ȿ������ �� �� �ֵ��� ä�� ���� ���� ����
    AudioSource[] sfxPlayers;
    int channelIndex; // ���� ���� ����ϰ� �ִ� ä���� �ε����� �� ��°����?

    public enum Sfx { BoxOpen_1, Button2, Button1, Door1, Door2, PlayerFootStep, PlayerRoll_1, PlayerRoll_2, Reload_1, Reload_2, Reloda_3, Shoot_1, Shoot_2}

    private void Awake()
    {
        instance = this;
        Init();
    }

    void Init() 
    {
        // ����� �÷��̾� �ʱ�ȭ
        GameObject bgmObject = new GameObject("BgmPlayer"); // �̸����� �������ַ��� �Ű����� �ȿ��ٰ� "�̸�" �������ָ� ��
        bgmObject.transform.parent = transform;
        // AddComponent �Լ��� ������ҽ��� �����ϰ� ������ ����
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;


        // ȿ���� �÷��̾� �ʱ�ȭ
        GameObject sfxObject = new GameObject("SfxPlayer"); // �̸����� �������ַ��� �Ű����� �ȿ��ٰ� "�̸�" �������ָ� ��
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels]; // ä�� ���� ����Ͽ� ������ҽ� �迭 �ʱ�ȭ

        for (int index=0; index < sfxPlayers.Length; index++)
        {
            // �ݺ������� ��� ȿ���� ������ҽ� �����ϸ鼭 ����
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

    //        // ���� �̵��Ǹ� ������ BgSoundPlay �޼��带 ȣ���ϰ� ����� ��
    //        // �̰� SceneManager Ŭ������ sceneLoaded �̺�Ʈ�� ���ؼ� ������
    //        // �̰� �Ϸ��� Dev GomDol�� ���󿡼� ���Ŵ��� Ŭ������ ��� ������ ã�ƺ��ų�
    //        // GPT���� ��� ���� �ϴ� �� �ۿ� ���� ��
    //        SceneManager.sceneLoaded += OnSceneLoaded;

    //        // bglist���� ����� Ŭ���� �� �̸��� �����Ͽ� ��ųʸ��� �ʱ�ȭ
    //        bgDictionary = new Dictionary<string, AudioClip>();
    //        foreach (var clip in bglist)
    //        {
    //            // bglist�� �ִ� Ŭ���� �̸��� ���� �̸��� ��Ȯ�� ��ġ�ؾ� �մϴ�.
    //            bgDictionary[clip.name] = clip;
    //        }
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //// ���� �ε�� �� ȣ��Ǵ� �޼���
    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    //{
    //    // ��ųʸ��� ����Ͽ� ���� ���� �´� ����� Ŭ�� ã��
    //    if (bgDictionary.TryGetValue(scene.name, out AudioClip clip))
    //    {
    //        BgSoundPlay(clip);
    //    }
    //}


    //public void SFXPlay(string sfxName, AudioClip clip) // �Ű������� �̸��� �޾ƿͼ� ������ ������Ʈ�� �ٿ��ֱ�
    //{
    //    GameObject go = new GameObject(sfxName + "Sound");
    //    AudioSource audiosource = go.AddComponent<AudioSource>();
    //    audiosource.clip = clip;
    //    audiosource.Play();

    //    // ȿ���� ����� ������ �����ߴ� ������Ʈ�� �ı��� �� �ְ� Destroy ���
    //    // Destroy(�ı��� ������Ʈ, �����ð�);
    //    // �� ��° ���� ���� �����ָ� �ش� �ð��� ���� �Ŀ� ������Ʈ�� �ı���
    //    Destroy(go, clip.length);

    //    //�̷��Ա��� ���� ���� ȿ������ �ְ� ���� ������ �̷������ ��ũ��Ʈ�� ����
    //    // �ش� ������ �̷������ �κп� ����� Ŭ���� �Ѱ��ֱ�
    //    // SoundManager.instance.SFXPlay("Hook",clip);
    //    // �� ��ũ��Ʈ�� ���� public AudioClip clip; �����ֱ�
    //}


    //public void BgSoundPlay(AudioClip clip)
    //{
    //    bgSound.clip = clip; // ������ҽ��� Ŭ���� �־��ֱ�
    //    bgSound.loop = true; // ��� �ݺ���� �ϰԲ� ������ ������ ����
    //    bgSound.volume = 0.1f;
    //    bgSound.Play();
    //}
}

