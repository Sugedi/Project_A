using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToEnding : MonoBehaviour
{
    public void GoEnd()
    {
        SceneManager.LoadScene("EndingVideo");
    }

}
