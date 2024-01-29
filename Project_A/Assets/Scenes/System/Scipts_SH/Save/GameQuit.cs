using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuit : MonoBehaviour
{
    public enum GameQuitBtn
    {
        Quit,
        Back
    }

    public GameQuitBtn gameQuitBtn;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            GameObject.Find("GameQuit").transform.Find("GameQuitCanvas").gameObject.SetActive(true);

        }
    }

    public void OnClick()
    {
        switch (gameQuitBtn)
        {
            case GameQuitBtn.Quit:

                Application.Quit();

                break;
            case GameQuitBtn.Back:

                Time.timeScale = 1f;
                GameObject.Find("GameQuit").transform.Find("GameQuitCanvas").gameObject.SetActive(false);

                break;

        }
    }

}
