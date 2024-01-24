using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLightController : MonoBehaviour
{
    public Light spotlight;       // spotlight 참조
    public Light directionalLight; // Directional Light 참조
    public SaveSwitch saveSwitch; // SaveSwitch 스크립트 타입으로 선언


    void Start()
    {
        // 게임 시작 시 spotlight를 비활성화하고, Directional Light를 활성화
        spotlight.enabled = false;
        directionalLight.enabled = true;
    }

    void Update()
    {
        // 스페이스바를 누르면 spotlight와 Directional Light를 전환
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleLights();
        }
        /*
        // SaveSwitch 스크립트의 SwitchFunc()가 호출되면 ToggleLights() 함수 실행
        if (saveSwitch != null)
        {
            if (saveSwitch.enabled)
            {
                saveSwitch.enabled = false; // 호출 상태 초기화
                ToggleLights();
            }
        }
        */
    }
    

    void ToggleLights()
    {
        // spotlight가 활성화되어있으면 비활성화하고, Directional Light를 활성화
        if (spotlight.enabled)
        {
            spotlight.enabled = false;
            directionalLight.enabled = true;
        }
        else
        {
            // spotlight가 비활성화되어있으면 활성화하고, Directional Light를 비활성화
            spotlight.enabled = true;
            directionalLight.enabled = false;
        }
    }
}
