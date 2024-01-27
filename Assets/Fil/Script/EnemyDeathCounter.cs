using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyDeathCounter : MonoBehaviour
{
    private int deadEnemyCount = 0;

    public TextMeshProUGUI deathCountText;

    private void Start()
    {
        UpdateDeathCountUI();
    }

    public void IncrementDeadEnemyCount()
    {
        deadEnemyCount++;
        UpdateDeathCountUI();
    }

    private void UpdateDeathCountUI()
    {
        if (deathCountText != null)
        {
            deathCountText.text = "Enemies Killed: " + deadEnemyCount;
        }
    }
}
