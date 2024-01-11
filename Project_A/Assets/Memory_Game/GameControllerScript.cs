using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;




public class GameControllerScript : MonoBehaviour
{

    // ���� ������ ���� ���� ��
    public const int columns = 2;
    public const int rows = 2;

    // �̹����� ��ġ�� �θ� ��ü
    public Transform parent;

    // �̹��� ������ ���� ����
    public const float Xspace = 260f;
    public const float Yspace = -300f;

    // ���� ���� �̹��� �� ���� ��������Ʈ �迭
    [SerializeField] private MainImageScript startObject;
    [SerializeField] private Sprite[] images;

    // ��ġ�� �������� ���� �Լ�
    private int[] Randomiser(int[] locations)
    {
        int[] array = locations.Clone() as int[];
        for (int i = 0; i < array.Length; i++)
        {
            int newArray = array[i];
            int j = Random.Range(i, array.Length);
            array[i] = array[j];
            array[j] = newArray;
        }
        return array;
    }

    // ������ ���۵� �� ȣ��Ǵ� �Լ�
    private void Start()
    {
        // �̹��� ��ġ�� �������� ����
        int[] locations = { 0, 0, 1, 1};

        locations = Randomiser(locations);

        Vector3 startPosition = startObject.transform.position;

        // ���� ���忡 �̹��� ��ġ
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                MainImageScript gameImage;
                if (i == 0 && j == 0)
                {
                    // ���� �̹����� ���� ó��
                    gameImage = startObject;
                }
                else
                {
                    // ������ �̹����� �����Ͽ� ���
                    gameImage = Instantiate(startObject) as MainImageScript;
                }

                int index = j * columns + i;
                Debug.Log(index);
                int id = locations[index];
                gameImage.ChangeSprite(id, images[id]);

                // �̹����� ��ġ ����
                float positionX = (Xspace * i) + startPosition.x;
                float positionY = (Yspace * j) + startPosition.y;

                gameImage.transform.position = new Vector3(positionX, positionY, startPosition.z);
                gameImage.transform.SetParent(parent, false);


            }
        }
    }

    // ������ �̹����� �����ϰ�, �´��� Ȯ���ϴ� �Լ�
    private MainImageScript firstOpen;
    private MainImageScript secondOpen;
    private MainImageScript firstOpenCard;
    private MainImageScript secondCard;

    // �� ��° �̹����� �� �� �ִ��� ���θ� ��ȯ�ϴ� �Ӽ�
    public bool canOpen
    {
        get { return secondOpen == null; }
    }

    // �̹����� ������ �� ȣ��Ǵ� �Լ�
    public void imageOpened(MainImageScript startObject)
    {
        if (firstOpen == null)
        {
            // ù ��° �̹��� ����
            firstOpen = startObject;
            firstOpenCard = firstOpen;

        }
        else
        {
            // �� ��° �̹��� ���� �� ��ġ ���� Ȯ��
            secondOpen = startObject;
            secondCard= secondOpen;
            
            StartCoroutine(CheckGuessed());
        }
    }
    public Image restartBtn;
    public Image MainBG;
    public float score = 0;
    public Text scoreText;
    // ��ġ ���θ� Ȯ���ϰ� ó���ϴ� �ڷ�ƾ
    private IEnumerator CheckGuessed()
    {
        if (firstOpen.spriteId == secondOpen.spriteId) // �� �̹����� ��������Ʈ ID ��
        {
            // ��ġ�ϸ� ���� ����
            score++;
            Vector3 originScale = new Vector3(1f, 1f, 1f);
            Vector3 targetSclae = new Vector3(1.1f,1.1f,1.1f);

            firstOpen.GGo();
            secondOpen.GGo();





        }
        else
        {
            // ��ġ���� ������ 0.5�� �Ŀ� �̹����� ����
            yield return new WaitForSeconds(0.5f);

            firstOpen.Close();
            secondOpen.Close();
        }


        // ������ �̹��� ���� �ʱ�ȭ
        firstOpen = null;
        secondOpen = null;
    }




    // ���� ����� �Լ�

    public void Restart()
    {
        Debug.Log("ddd");

        SceneManager.LoadScene("HNMiniGameScene");
    }


}
