using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffects : MonoBehaviour
{
    public ParticleSystem deathParticles; // Assign the particle system prefab in the inspector
    public Transform targetPosition; // Assign the target position in the inspector

    public float spawnDelay = 1.0f; // Delay before spawning particles
    public float moveSpeed = 5.0f; // Speed of particles moving towards the target

    private void OnEnable()
    {
        // Subscribe to the enemy death event
        AIEnemy.OnEnemyDeathEvent += SpawnDeathParticles;
    }

    private void OnDisable()
    {
        // Unsubscribe from the enemy death event to prevent memory leaks
        AIEnemy.OnEnemyDeathEvent -= SpawnDeathParticles;
    }

    private void SpawnDeathParticles(Vector3 enemyPosition)
    {
        // Start the coroutine with specified delay before spawning particles
        StartCoroutine(SpawnAndMoveParticles(enemyPosition));
    }

    private IEnumerator SpawnAndMoveParticles(Vector3 enemyPosition)
    {
        // Wait for the specified delay before spawning particles
        yield return new WaitForSeconds(spawnDelay);

        // Instantiate the particle system at the enemy's position
        ParticleSystem particles = Instantiate(deathParticles, enemyPosition, Quaternion.identity);

        // Start the coroutine to move and disappear the particles
        StartCoroutine(MoveAndDisappearParticles(particles));
    }

    private IEnumerator MoveAndDisappearParticles(ParticleSystem particles)
    {
        // Move the particles to the target position
        while (particles != null && particles.isPlaying)
        {
            particles.transform.position = Vector3.MoveTowards(particles.transform.position, targetPosition.position, Time.deltaTime * moveSpeed);
            yield return null;
        }

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Destroy the particles
        if (particles != null)
        {
            Destroy(particles.gameObject);
        }
    }
}
