using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossKillDoor : MonoBehaviour
{
    public AnimationClip bossDoorOpenAnimation;
    // ...
    private void Start()
    {
        if (DataManager.instance.datas.galioFirstClear == true)
        {
            gameObject.SetActive(false);
        }
    }
    public void BossKillDoorOpen()
    {
        // Check if the doorAnimation is assigned
        if (bossDoorOpenAnimation != null)
        {
            // Play the 'bosskilldooropen' animation
            bossPlayDoorAnimation(bossDoorOpenAnimation);
            SoundManager.instance.PlayAudio("Door2", "SE");

        }
        else
        {
            Debug.LogError("Door Animation not assigned to BossKillDoor script.");
        }
    }
    void bossPlayDoorAnimation(AnimationClip animationClip)
    {
        // Set the specified animation clip to the Animation component
        GetComponent<Animation>().clip = animationClip;

        // Play the animation once
        GetComponent<Animation>().wrapMode = WrapMode.Once;
        GetComponent<Animation>().Play();
        // Add self-disable method call
        StartCoroutine(DisableAfterAnimation(animationClip.length));
    }

    IEnumerator DisableAfterAnimation(float duration)
    {
        // Wait for the animation to finish
        yield return new WaitForSeconds(duration);

        // Disable the script or GameObject
        DisableScript();
    }

    void DisableScript()
    {

            DataManager.instance.datas.galioFirstClear = true;
            Debug.Log("Galio has been defeated!");
            gameObject.SetActive(false);
            DataManager.instance.DataSave();
        
    }
}