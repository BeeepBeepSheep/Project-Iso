using UnityEngine;

public class ChanceObjectSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ObjectSpawnInfo
    {
        public GameObject objectToSpawn; // The prefab to spawn
        [Range(0f, 100f)] public float spawnChance = 50f; // The chance percentage to spawn the object
    }

    public ObjectSpawnInfo[] objectsToSpawn; // Array of objects with spawn information

    void Start()
    {
        // Calculate the total spawn chance of all objects
        float totalSpawnChance = 0f;
        foreach (var spawnInfo in objectsToSpawn)
        {
            totalSpawnChance += spawnInfo.spawnChance;
        }

        // Choose a random value within the total spawn chance
        float randomValue = Random.Range(0f, totalSpawnChance);

        // Iterate through each object and determine which one to spawn
        float cumulativeChance = 0f;
        foreach (var spawnInfo in objectsToSpawn)
        {
            cumulativeChance += spawnInfo.spawnChance;

            // Check if the random value falls within the spawn chance for this object
            if (randomValue <= cumulativeChance)
            {
                SpawnObject(spawnInfo.objectToSpawn);
                break; // Exit the loop once an object is spawned
            }
        }
    }

    void SpawnObject(GameObject objectPrefab)
    {
        // Spawn the specified object at the position of this GameObject
        Instantiate(objectPrefab, transform.position, Quaternion.identity);
    }
}
