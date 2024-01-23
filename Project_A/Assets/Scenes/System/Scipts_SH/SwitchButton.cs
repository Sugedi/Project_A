using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SwitchUI
{
    Next1,
    Next2,
    Next3,
    Backstage,
    Continue,
    TreasureBoxNext
}

public class SwitchButton : MonoBehaviour
{
    public SwitchUI switchUI;
    public CanvasGroup no1; 
    public CanvasGroup no2; 
    public CanvasGroup no3; 
    public CanvasGroup no4; 
    public CanvasGroup saveConvas; 
    public CanvasGroup treasureCanvas; 

    public CanvasGroup joystick; 
    public CanvasGroup joystick2; 

    public void OnClick()
    {
        switch (switchUI)
        {
            case SwitchUI.Next1:

                CanvasGroupOff(no1);
                CanvasGroupOn(no2);

                break;
            case SwitchUI.Next2:

                CanvasGroupOff(no2);
                CanvasGroupOn(no3);

                break;
            case SwitchUI.Next3:

                CanvasGroupOff(no3);
                CanvasGroupOn(no4);

                break;
            case SwitchUI.Backstage:

                CanvasGroupOff(saveConvas);
                Time.timeScale = 1f;
                DataManager.instance.DataSave();
                SceneManager.LoadScene("BackStage_0114");
                CanvasGroupOn(joystick);
                CanvasGroupOn(joystick2);

                break;
            case SwitchUI.Continue:

                CanvasGroupOff(saveConvas);
                CanvasGroupOn(no1);
                CanvasGroupOff(no4);
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                CanvasGroupOn(joystick);
                CanvasGroupOn(joystick2);

                break;
            case SwitchUI.TreasureBoxNext:

                CanvasGroupOff(treasureCanvas);
                Time.timeScale = 1f;
                CanvasGroupOn(joystick);
                CanvasGroupOn(joystick2);

                break;


        }
    }

    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;

    }
}
