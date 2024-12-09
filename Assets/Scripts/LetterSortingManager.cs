using UnityEngine;
using System.Collections;

public class LetterSortingManager : MonoBehaviour
{
    //Public variables
    public static LetterSortingManager Instance { get; private set; } // Static instance for global access, allowing other scripts to call LetterSortingManager
    public GameObject[] letterPrefabs; // Array of different letter prefabs (e.g., LetterA, LetterB, LetterC). New letter is randomly chosen from this array when spawned.
    public Transform spawnLetter;      // The empty GameObject representing the spawn position
    //Private variables
    private GameObject currentLetter; // Reference to the currently active letter in the scene

    private void Awake()
    {
        // Set up the singleton instance. If an instance already exists, destroy this one
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SpawnNewLetter(); // Spawn the first letter when the game starts.
    }

    //Spawns a new random letter from the letterPrefabs array. If a letter already exists, it is destroyed first
    public void SpawnNewLetter()
    {
        // If a current letter exists, remove it before spawning another one
        if (currentLetter != null)
        {
            Destroy(currentLetter);
        }

        // Spawn a random letter
        int randomIndex = Random.Range(0, letterPrefabs.Length);
        currentLetter = Instantiate(letterPrefabs[randomIndex], spawnLetter.position, Quaternion.identity);
    }

    //Respawns an existing letter back to the spawn position. This occurs when a letter is placed incorrectly in a sorting bin
    //The letter is unparented, repositioned, reoriented, and its physics and collider are reset.
    public void RespawnLetter(GameObject letter)
    {
        Debug.Log("Respawning letter: " + letter.name);

        // Unparent the letter from anything it might currently be attached to
        letter.transform.parent = null;

        // Reset the position and rotation to the spawn point
        letter.transform.position = spawnLetter.position;
        letter.transform.rotation = spawnLetter.rotation;

        // Make sure physics is reset (no kinematic, no velocity)
        Rigidbody rb = letter.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;    // Ensure it can move freely
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Re-enable the collider if needed
        Collider col = letter.GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = true;
        }

        Debug.Log("Letter " + letter.name + " respawned successfully at " + spawnLetter.position);
    }
}
