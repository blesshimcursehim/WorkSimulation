using UnityEngine;

public class SortingBin : MonoBehaviour

{
    public string correctLetterTag;  // Tag of the correct letter for this bin, switched to name of prefab instead later on though 
    public BinAudioHandler binAudioHandler; // Reference to the audio handler component

    //Called when another collider enters the trigger volume of this bin
    //Checks if the entering object matches the correct letter type
    private void OnTriggerEnter(Collider other)
    {
        // Get the base name of the letter by removing "(Clone)" and trimming whitespace
        string letterName = other.gameObject.name.Replace("(Clone)", "").Trim();
        Debug.Log("Trigger entered by: " + letterName + ", expected: " + correctLetterTag);
        // Check if the letter name matches the expected correctLetterTag
        if (letterName == correctLetterTag)
        {
            Debug.Log("Correct Placement in bin: " + gameObject.name);
            HandleCorrectPlacement(other.gameObject);
        }
        else
        {
            Debug.Log("Incorrect Placement in bin: " + gameObject.name);
            HandleIncorrectPlacement(other.gameObject);
        }
    }

    //Called when a correctly placed letter is detected
    // plays the correct sound, destroys the current letter, and spawns a new one
    private void HandleCorrectPlacement(GameObject letter)
    {
        // If we are holding this letter, force release it
        GrabObject.Instance.ForceReleaseIfHolding(letter);
        // Play correct placement audio
        if (binAudioHandler != null)
        {
            binAudioHandler.PlayCorrectAudio();
        }

        // Handle correct placement: destroy the letter and spawn a new one
        Destroy(letter);

        // Tell the LetterSortingManager to spawn a new letter
        LetterSortingManager.Instance.SpawnNewLetter();
    }

    //Called when an incorrectly placed letter is detected.
    //Plays the incorrect sound, then respawns the letter back to the spawn point
    private void HandleIncorrectPlacement(GameObject letter)
    {
        // Play incorrect placement audio
        if (binAudioHandler != null)
        {
            binAudioHandler.PlayIncorrectAudio();
        }

        // Handle incorrect placement: send the letter back to its spawn point
        LetterSortingManager.Instance.RespawnLetter(letter);
    }
}
