using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLightController : MonoBehaviour
{
    public Light spotlight;       // spotlight ����
    public Light directionalLight; // Directional Light ����
    public SaveSwitch saveSwitch; // SaveSwitch ��ũ��Ʈ Ÿ������ ����


    void Start()
    {
        // ���� ���� �� spotlight�� ��Ȱ��ȭ�ϰ�, Directional Light�� Ȱ��ȭ
        spotlight.enabled = false;
        directionalLight.enabled = true;
    }

    void Update()
    {
        // �����̽��ٸ� ������ spotlight�� Directional Light�� ��ȯ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleLights();
        }
        /*
        // SaveSwitch ��ũ��Ʈ�� SwitchFunc()�� ȣ��Ǹ� ToggleLights() �Լ� ����
        if (saveSwitch != null)
        {
            if (saveSwitch.enabled)
            {
                saveSwitch.enabled = false; // ȣ�� ���� �ʱ�ȭ
                ToggleLights();
            }
        }
        */
    }
    

    void ToggleLights()
    {
        // spotlight�� Ȱ��ȭ�Ǿ������� ��Ȱ��ȭ�ϰ�, Directional Light�� Ȱ��ȭ
        if (spotlight.enabled)
        {
            spotlight.enabled = false;
            directionalLight.enabled = true;
        }
        else
        {
            // spotlight�� ��Ȱ��ȭ�Ǿ������� Ȱ��ȭ�ϰ�, Directional Light�� ��Ȱ��ȭ
            spotlight.enabled = true;
            directionalLight.enabled = false;
        }
    }
}
