using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    NavMeshAgent agent; // NavMeshAgent component reference for navigation
    Character character;
    [SerializeField] float default_MoveSpeed = 3.5f;
    float current_MoveSpeed;
    StatsValue moveSpeed;

    private void Awake()
    {
        // Assigns the NavMeshAgent component attached to this GameObject to the 'agent' variable
        agent = GetComponent<NavMeshAgent>();

        character = GetComponent<Character>();
    }

    private void Start()
    {
        moveSpeed = character.TakeStats(Statistic.MoveSpeed);
        UpdateMoveSpeed();
    }

    private void UpdateMoveSpeed()
    {
        agent.speed = default_MoveSpeed * moveSpeed.float_value;
    }

    private void Update()
    {
        if(current_MoveSpeed != moveSpeed.float_value)
        {
            current_MoveSpeed = moveSpeed.float_value;
            UpdateMoveSpeed();
        }
    }

    // Public method allowing external scripts to set the destination for the NavMeshAgent
    public void SetDestination(Vector3 destinationPosition)
    {
        agent.isStopped = false;
        // Sets the destination for the NavMeshAgent to the provided 'destinationPosition'
        agent.SetDestination(destinationPosition);
    }

    internal void Stop()
    {
        agent.isStopped = true;
    }
}