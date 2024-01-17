using UnityEngine;

public class TrapDoorSpawner : MonoBehaviour
{
    public GameObject doorPrefab;
    public Transform[] spawningPoints;

    void Start()
    {
        // Ensure there are spawning points
        if (spawningPoints.Length == 0)
        {
            Debug.LogError("No spawning points assigned. Please assign points in the Inspector.");
            return;
        }

        // Randomly choose a spawning point for the door
        int doorSpawnIndex = Random.Range(0, spawningPoints.Length);
        SpawnObject(doorPrefab, spawningPoints[doorSpawnIndex]);
    }

    void SpawnObject(GameObject prefab, Transform spawnPoint)
    {
        // Instantiate the specified prefab at the given spawn point
        Instantiate(prefab, spawnPoint.position, Quaternion.identity);
    }
}
