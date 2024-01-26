using UnityEngine;

public class EnemyDeathCounter : MonoBehaviour
{
    private int deadEnemyCount = 0;
    public int DeadEnemyCount => deadEnemyCount;

    public void IncrementDeadEnemyCount()
    {
        deadEnemyCount++;
    }
}
