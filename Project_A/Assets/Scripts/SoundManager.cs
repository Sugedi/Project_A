using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class AudioInformation
{
    public string name; // ����� �̸�
    public float volume; // ����� ����
    public bool isLoop; // �ݺ� ��� ���� �Ǵ�
    public AudioClip clip; // ����� ����
}

//������� ���� �ϴ� Ŭ����(�̱���) - �������, ȿ����, ������ ���� ����
public class SoundManager : MonoBehaviour
{
    //�̱��� ���� �غ�
    public static SoundManager instance;
    [SerializeField]
    private AudioInformation[] bgmInfo = null; // ������� ����
    private AudioSource bgmPlayer;
    private int currentBGMIndex; // ���� �÷��� ���� ������� �ε���

    [SerializeField] AudioInformation[] seInfo = null;//ȿ��������
    private List<AudioSource> sePlayer; // ȿ���� ����� �÷��̾�

    private AudioSource voicePlayer;
    [SerializeField] float defaultVolume = 1f;

    // �̱����� ���� �ʱ�ȭ ����
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;  // �̺�Ʈ ������ �߰�

        // instance �� null�� ���¶��
        if (instance == null)
        {
            instance = this; // �ν��Ͻ��� �ڱ��ڽ�
            DontDestroyOnLoad(gameObject); // �ٸ��������� ��� �� �� �ֵ��� ����
        }
        else
        {
            Destroy(gameObject); // �ν��Ͻ��� �̹� ���� ���̶�� �ı��ϰ� ���� �����
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
        // AudioSource ������Ʈ�� null üũ
        for (int i = 0; i < sePlayer.Count; i++)
        {
            if (sePlayer[i] == null)
            {
                Debug.LogError("AudioSource component on SoundManager is null after scene load.");
            }
        }

        // 'Loading' ��, 'LoadingBackstage'��, 'LoadingStage' ���� ó���ϴ� ��츦 �����ؾ� �ϹǷ�,
        // 'Loading' ��, 'LoadingBackstage'��, 'LoadingStage' �� �̸��� üũ�ϴ� ���ǹ��� �߰��մϴ�.
        if (scene.name == "Loading" || scene.name == "LoadingBackstage" || scene.name == "LoadingStage")
        {
            // 'Loading' ��, 'LoadingBackstage', 'LoadingStage' �������� �ƹ� �۾��� ���� �ʽ��ϴ�.
            return;
        }

        switch (scene.name)
        {
            case "01_Title": // ����ٰ��� ���� �̸� ����
                PlayBGM("01_Title");  // ��1�� �ش��ϴ� ������� �̸�
                break;
            case "Backstage_0114": // ����ٰ��� ���� �̸� ����
                PlayBGM("Backstage_0114");  // ��2�� �ش��ϴ� ������� �̸�                
                break;
            case "Stage": // ����ٰ��� ���� �̸� ����
                PlayBGM("Stage");  // ��3�� �ش��ϴ� ������� �̸�                
                break;
            default:
                // �ε� ������ �ƴϰ�, �ٸ� �� �̸��� �߸��Ǿ��� ���� ������ �α��մϴ�.
                Debug.LogError("Unknown scene name: " + scene.name);
                break;
        }
    }

    private void PlayBGM(string p_name)
    {
        // bgmInfo�� �迭��ŭ �ݺ� ���
        for (int i = 0; i < bgmInfo.Length; i++)
        {
            if (p_name == bgmInfo[i].name)
            {
                // bgminfo�� ��� �ִ� ����� Ŭ���� ����ϰ� �ݺ����� ����������
                bgmPlayer.clip = bgmInfo[i].clip;
                bgmPlayer.volume = bgmInfo[i].volume;
                bgmPlayer.loop = bgmInfo[i].isLoop;
                bgmPlayer.Play();
                return;
            }
        }
        Debug.LogError(p_name + "�� �ش��ϴ� ��������� �����ϴ�.");
    }

    private void StopBGM() // BGM ���� �ϴ� �Լ�
    {
        bgmPlayer.Stop();
    }
    private void PauseBGM() // BGM ���ߴ� �Լ�
    {
        bgmPlayer.Pause();
    }
    private void UnPauseBGM() // BGM ���� ���¸� ���� �ϴ� �Լ�
    {
        bgmPlayer.UnPause();
    }

    private void PlaySE(string p_name) // ȿ���� ����� �Ķ���ͷ� �̸��� �޴´�
    {
        bool isPlayed = false; // ȿ���� ��� ���θ� üũ�ϴ� �÷���
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
                        break; // ȿ���� ��� �� �ݺ��� ����
                    }
                }
                if (isPlayed) break; // ȿ������ ����Ǿ��ٸ� �� �̻��� Ȯ���� �ʿ� ����
            }
        }
        if (!isPlayed)
        {
            Debug.LogError("SE " + p_name + " could not be played. All channels are busy or SE not found.");
        }
    }

    private void StopAllSE() // ������� ��� ȿ������ �����
    {
        // ȿ������ ������ŭ �ݺ� ����
        for (int i = 0; i < sePlayer.Count; i++)
        {
            sePlayer[i].Stop(); // ȿ���� ��� ����
        }
    }

    // ���ϴ� �ܺο��� ȣ�� �ϴ� �޼ҵ� �Դϴ�
    // �����̸��� � Ÿ���� ����������� �μ��� �Ѱ� �޴´�

    ///
    ///p_Type : BGM -> BGM ������� ���
    ///p_Type : SE -> SE ȿ���� ���
    ///p_Type : VOICE -> Voice ���� ��� (�츮�� �Ⱦ� �� ������)

    public void PlayAudio(string p_name, string p_type)
    {
        PlayAudio(p_name, p_type, defaultVolume);

    }

    // �������� ������ ��� �ش� �������� ���
    public void PlayAudio(string p_name, string p_type, float volume)
    {
        // �Ѱܹ��� Ÿ�� ���� ���� ���� �ش� �÷��̾ ��� ��Ų��
        if (p_type == "BGM") PlayBGM(p_name);
        else if (p_type == "SE") PlaySE(p_name);
        else Debug.Log("�ش� ������ ����� �÷��̾ �����ϴ�.");
    }

    // ����� ����� �����ϴ� �޼ҵ�
    public void StopAudio(string p_type)
    {
        if (p_type == "BGM") StopBGM();
        else if (p_type == "SE") StopAllSE();
        else Debug.Log("�ش��ϴ� Ÿ���� ����� �÷��̾ �����ϴ�.");
    }

    // ��� ����� ����� �����ϴ� �Լ�
    public void StopAllAudio()
    {
        StopBGM();
        StopAllSE();
    }

}
