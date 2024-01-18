using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyInteraction : MonoBehaviour
{
    public Canvas StageUI;
    public Image Clickspace;

    private Image displayedImage;

    private void Start()
    {
        // 초기에 이미지를 비활성화
        Clickspace.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowImage();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideImage();
        }
    }

    private void ShowImage()
    {


        // 이미지를 Canvas 하위에 생성하고 활성화
        displayedImage = Instantiate(Clickspace, StageUI.transform);
        displayedImage.gameObject.SetActive(true);

    }

    private void HideImage()
    {

        // 이미지를 비활성화하고 제거
        if (displayedImage != null)
        {
            displayedImage.gameObject.SetActive(false);
            Destroy(displayedImage.gameObject);
            displayedImage = null;
        }
    }
}