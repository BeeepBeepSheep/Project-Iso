using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform spawnTransform; // Assign the spawn transform in the inspector

    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        PlayerManager.Instance.SpawnPlayer(spawnTransform.position, spawnTransform.rotation);
    }
}
