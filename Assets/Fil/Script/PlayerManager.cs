using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public string essentialScene = "Essential"; // Name of the essential scene
    public GameObject playerPrefab; // Assign the player prefab in the inspector

    private GameObject playerInstance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Spawn the player if not already spawned
        SpawnPlayer(Vector3.zero, Quaternion.identity);
    }

    public void SpawnPlayer(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        if (playerInstance == null)
        {
            // Check if the player exists in the "Essential" scene
            GameObject essentialPlayer = GameObject.FindWithTag("Player");

            if (essentialPlayer != null)
            {
                // Move the player to the new position
                essentialPlayer.transform.position = spawnPosition;
                essentialPlayer.transform.rotation = spawnRotation;
                playerInstance = essentialPlayer;
            }
            else if (playerPrefab != null)
            {
                // Instantiate the player prefab if not found in the "Essential" scene
                playerInstance = Instantiate(playerPrefab, spawnPosition, spawnRotation);
            }
            else
            {
                Debug.LogError("Player prefab is not assigned in the inspector.");
            }

            // Optionally, you can perform additional setup for the player here if needed
        }
    }

    public GameObject GetPlayerInstance()
    {
        return playerInstance;
    }
}
