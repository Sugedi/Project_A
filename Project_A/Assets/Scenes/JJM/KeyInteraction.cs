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
        // �ʱ⿡ �̹����� ��Ȱ��ȭ
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
        if (other.CompareTag("Player") || Input.GetKeyDown(KeyCode.Space))
        {
            HideImage();
        }
    }

    private void ShowImage()
    {
        Debug.Log("ShowImage() ȣ���");

        // �̹����� Canvas ������ �����ϰ� Ȱ��ȭ
        displayedImage = Instantiate(Clickspace, StageUI.transform);
        displayedImage.gameObject.SetActive(true);

    }

    private void HideImage()
    {
        Debug.Log("HideImage() ȣ���");
        // �̹����� ��Ȱ��ȭ�ϰ� ����
        if (displayedImage != null)
        {
            displayedImage.gameObject.SetActive(false);
            Destroy(displayedImage.gameObject);
            displayedImage = null;
        }
    }
}