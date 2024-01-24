using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public CanvasGroup joystick;
    public void MoveToBack()
    {
        // gem = gem/2;
        CanvasGroupOff(joystick);
        Time.timeScale = 1f;
        GameObject.Find("Player").GetComponent<Player>().isDead = false;
        DataManager.instance.DataSave();
        SceneManager.LoadScene("LoadingBackstage");
    }

    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;

    }
}
