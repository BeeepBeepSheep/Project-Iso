using UnityEngine;

public class PortalActivation : MonoBehaviour
{
    public int requiredDeadEnemies = 3; // Set the required number of dead enemies in the Inspector
    private EnemyDeathCounter deathCounter;

    private void Start()
    {
        deathCounter = FindObjectOfType<EnemyDeathCounter>(); // Assuming there's only one EnemyDeathCounter in the scene

        // Deactivate the portal at the start
        DeactivatePortal();
    }

    // Call this method when an enemy is killed to check the activation condition
    public void EnemyKilled()
    {
        if (deathCounter != null && deathCounter.DeadEnemyCount >= requiredDeadEnemies)
        {
            // Activate your portal or perform any other desired action
            ActivatePortal();
        }
    }

    private void ActivatePortal()
    {
        // For example, assuming there's a GameObject named "MyPortal" to activate
        GameObject myPortal = GameObject.Find("MyPortal");
        if (myPortal != null)
        {
            myPortal.SetActive(true);
        }
    }

    private void DeactivatePortal()
    {
        // For example, assuming there's a GameObject named "MyPortal" to deactivate
        GameObject myPortal = GameObject.Find("MyPortal");
        if (myPortal != null)
        {
            myPortal.SetActive(false);
        }
    }
}
