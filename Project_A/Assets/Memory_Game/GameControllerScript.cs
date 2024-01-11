using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;




public class GameControllerScript : MonoBehaviour
{

    // 게임 보드의 열과 행의 수
    public const int columns = 2;
    public const int rows = 2;

    // 이미지를 배치할 부모 객체
    public Transform parent;

    // 이미지 사이의 간격 설정
    public const float Xspace = 260f;
    public const float Yspace = -300f;

    // 게임 시작 이미지 및 사용될 스프라이트 배열
    [SerializeField] private MainImageScript startObject;
    [SerializeField] private Sprite[] images;

    // 위치를 무작위로 섞는 함수
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

    // 게임이 시작될 때 호출되는 함수
    private void Start()
    {
        // 이미지 위치를 무작위로 섞음
        int[] locations = { 0, 0, 1, 1};

        locations = Randomiser(locations);

        Vector3 startPosition = startObject.transform.position;

        // 게임 보드에 이미지 배치
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                MainImageScript gameImage;
                if (i == 0 && j == 0)
                {
                    // 시작 이미지는 따로 처리
                    gameImage = startObject;
                }
                else
                {
                    // 나머지 이미지는 복제하여 사용
                    gameImage = Instantiate(startObject) as MainImageScript;
                }

                int index = j * columns + i;
                Debug.Log(index);
                int id = locations[index];
                gameImage.ChangeSprite(id, images[id]);

                // 이미지의 위치 설정
                float positionX = (Xspace * i) + startPosition.x;
                float positionY = (Yspace * j) + startPosition.y;

                gameImage.transform.position = new Vector3(positionX, positionY, startPosition.z);
                gameImage.transform.SetParent(parent, false);


            }
        }
    }

    // 열려진 이미지를 추적하고, 맞는지 확인하는 함수
    private MainImageScript firstOpen;
    private MainImageScript secondOpen;
    private MainImageScript firstOpenCard;
    private MainImageScript secondCard;

    // 두 번째 이미지를 열 수 있는지 여부를 반환하는 속성
    public bool canOpen
    {
        get { return secondOpen == null; }
    }

    // 이미지가 열렸을 때 호출되는 함수
    public void imageOpened(MainImageScript startObject)
    {
        if (firstOpen == null)
        {
            // 첫 번째 이미지 열림
            firstOpen = startObject;
            firstOpenCard = firstOpen;

        }
        else
        {
            // 두 번째 이미지 열림 후 일치 여부 확인
            secondOpen = startObject;
            secondCard= secondOpen;
            
            StartCoroutine(CheckGuessed());
        }
    }
    public Image restartBtn;
    public Image MainBG;
    public float score = 0;
    public Text scoreText;
    // 일치 여부를 확인하고 처리하는 코루틴
    private IEnumerator CheckGuessed()
    {
        if (firstOpen.spriteId == secondOpen.spriteId) // 두 이미지의 스프라이트 ID 비교
        {
            // 일치하면 점수 증가
            score++;
            Vector3 originScale = new Vector3(1f, 1f, 1f);
            Vector3 targetSclae = new Vector3(1.1f,1.1f,1.1f);

            firstOpen.GGo();
            secondOpen.GGo();





        }
        else
        {
            // 일치하지 않으면 0.5초 후에 이미지를 닫음
            yield return new WaitForSeconds(0.5f);

            firstOpen.Close();
            secondOpen.Close();
        }


        // 열려진 이미지 변수 초기화
        firstOpen = null;
        secondOpen = null;
    }




    // 게임 재시작 함수

    public void Restart()
    {
        Debug.Log("ddd");

        SceneManager.LoadScene("HNMiniGameScene");
    }


}
