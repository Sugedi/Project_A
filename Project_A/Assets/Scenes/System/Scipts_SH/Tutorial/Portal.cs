using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public CanvasGroup Move_0;
    public CanvasGroup joy;

    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;

    }
    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;

    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject.Find("MoveGoal").transform.Find("Portal").gameObject.SetActive(false);
            CanvasGroupOn(Move_0);
            CanvasGroupOff(joy);
            Destroy(this);
        }

            
    }

}
