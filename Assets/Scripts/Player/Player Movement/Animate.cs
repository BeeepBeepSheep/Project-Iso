using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animate : MonoBehaviour
{
    Animator animator; // Animator component reference for controlling animations

    NavMeshAgent agent; // NavMeshAgent component reference for navigation

    Character character;

    private void Awake()
    {
        // Assigns the Animator component attached to a child GameObject (if any) to the 'animator' variable
        animator = GetComponentInChildren<Animator>();

        // Assigns the NavMeshAgent component attached to this GameObject to the 'agent' variable
        agent = GetComponent<NavMeshAgent>();

        character = GetComponent<Character>();
    }

    private void Update()
    {
        // Calculates the magnitude (speed) of the agent's velocity
        float motion = agent.velocity.magnitude;

        // Sets the 'motion' parameter in the Animator Controller to control animation speed based on agent velocity
        animator.SetFloat("motion", motion);

        animator.SetBool("defeated", character.isDead);
    }
}
