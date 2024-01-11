using System.Collections.Generic;
using UnityEngine;

public class ResetCube : MonoBehaviour
{
    public List<PuzzleObjectReset> puzzleCubes = new List<PuzzleObjectReset>();

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player
        if (other.CompareTag("Player"))
        {
            // Call the puzzle reset function on all puzzleCubes in the list
            foreach (var puzzleCube in puzzleCubes)
            {
                puzzleCube.PuzzleResetFunction();
            }
        }
    }
}
