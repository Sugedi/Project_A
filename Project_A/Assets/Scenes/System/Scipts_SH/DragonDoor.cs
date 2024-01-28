using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDoor : MonoBehaviour
{

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject.Find("DragonDoor").transform.Find("DragonDoor_").gameObject.SetActive(true);
            Destroy(this);
            SoundManager.instance.PlayAudio("DragonDoorOpen", "SE");

        }


    }

}
