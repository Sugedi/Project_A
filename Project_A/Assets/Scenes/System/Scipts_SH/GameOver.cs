using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void MoveToBack()
    {
        // gem = gem/2;
        Time.timeScale = 1f;
        GameObject.Find("Player").GetComponent<Player>().isDead = false;
        DataManager.instance.DataSave();
        SceneManager.LoadScene("BackStage_0114");
    }
}
