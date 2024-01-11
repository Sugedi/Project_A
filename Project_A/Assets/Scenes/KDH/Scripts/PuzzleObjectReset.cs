using UnityEngine;

public class PuzzleObjectReset : MonoBehaviour
{
    // Initial position of the puzzleCube
    private Vector3 puzzleInitialPosition;

    void Start()
    {
        // Store the initial position when the scene starts
        puzzleInitialPosition = transform.position;
    }

    public void PuzzleResetFunction()
    {
        // Reset the puzzleCube's position to the initial position
        transform.position = puzzleInitialPosition;
    }
}
