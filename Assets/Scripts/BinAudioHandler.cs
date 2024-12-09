using UnityEngine;

public class BinAudioHandler : MonoBehaviour
{
    //Public variables
    public AudioClip correctAudioClip;   // Drag and drop the correct audio clip in the Inspector
    public AudioClip incorrectAudioClip; // Drag and drop the incorrect audio clip in the Inspector

    //private variables
    private AudioSource audioSource;     // Reference to the AudioSource component used for playing audio

    private void Awake()
    {
        // Get the AudioSource component on the same GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on " + gameObject.name);
        }
    }

    //Plays the audio clip assigned for correct placement 
    //Logs an error if no correctAudioClip is assigned
    public void PlayCorrectAudio()
    {
        Debug.Log("Attempting to play correct audio.");
        if (correctAudioClip != null)
        {
            audioSource.PlayOneShot(correctAudioClip);
        }
        else
        {
            Debug.LogError("Correct audio clip is not assigned!");
        }
    }

    //Plays the audio clip assigned for incorrect placement if it exists
    //Logs an error if no incorrectAudioClip is assigned
    public void PlayIncorrectAudio()
    {
        Debug.Log("Attempting to play incorrect audio.");
        if (incorrectAudioClip != null)
        {
            audioSource.PlayOneShot(incorrectAudioClip);
        }
        else
        {
            Debug.LogError("Incorrect audio clip is not assigned!");
        }
    }
}
