using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectoreForNPC : MonoBehaviour
{
    public NPCStateManager stateManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Found player");
            stateManager.TargetPlayer();
        }
    }
}
