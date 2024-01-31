using UnityEngine;
using System.Collections;

public class LeverController : MonoBehaviour
{
    public GameObject off;
    public GameObject on;
    public float transitionDelay = 0.5f;
    private bool leverOn = false;
    private bool canToggle = true;
    public bool isPuzzleAnswer = false; // New variable for puzzle answer
    private Animator leverAnimator;

    void Start()
    {
        if (off == null || on == null)
        {
            Debug.LogError("Ensure 'off' and 'on' components are assigned in the Inspector.");
            return;
        }

        leverAnimator = transform.Find("lever").GetComponent<Animator>();

        SetLeverState(false);
    }

    public void Lever()
    {
        // Replace "Player" with your actual player tag
        StartCoroutine(ToggleLever(!leverOn));
    }

    IEnumerator ToggleLever(bool newState)
    {
        if (canToggle)
        {
            canToggle = false;

            SoundManager.instance.PlayAudio("Lever2", "SE");

            int leverState = newState ? 1 : 2;

            // Set the lever state for the animation
            leverAnimator.SetInteger("isleverstate", leverState);

            // Wait for the animation to complete
            yield return new WaitForSeconds(transitionDelay);

            // Set the lever state back to 0 to stop the animation
            leverAnimator.SetInteger("isleverstate", 0);

            SetLeverState(newState);

            // Toggle the puzzle answer
            isPuzzleAnswer = !isPuzzleAnswer;
            Debug.Log("Is Puzzle Answer: " + isPuzzleAnswer);

            canToggle = true;
        }
    }

    void SetLeverState(bool newState)
    {
        leverOn = newState;
        off.SetActive(!leverOn);
        on.SetActive(leverOn);
    }
}
