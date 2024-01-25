using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    AttackHandler attackHandler;
    EnemyDeathCounter deathCounter; // Reference to the death counter script

    private void Awake()
    {
        attackHandler = GetComponent<AttackHandler>();
        deathCounter = FindObjectOfType<EnemyDeathCounter>(); // Assuming there's only one EnemyDeathCounter in the scene
    }

    [SerializeField] public Character target;
    float timer = 4f;

    private void Start()
    {
        target = GameManager.instance.playerObject.GetComponent<Character>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            timer = 4f;

            // Check if the target is alive before attacking
            if (target != null && !target.isDead)
            {
                attackHandler.Attack(target);
            }
        }
    }

    // Called when the enemy dies
    public void OnEnemyDeath()
    {
        if (deathCounter != null)
        {
            deathCounter.IncrementDeadEnemyCount();

            // Optionally, you can add additional logic here when an enemy dies.
        }
    }
}
