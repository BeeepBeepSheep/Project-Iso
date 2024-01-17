using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterDefeatHandler : MonoBehaviour
{
    NavMeshAgent agent;
    AIEnemy aIEnemy;
    Collider objectCollider;

    AttackInput attackInput;
    InteractInput interactInput;
    PlayerCharacterInput playerCharacterInput;
    CharacterMovementInput movementInput;
    Character character;


    [SerializeField] bool player = false;
    [SerializeField] GameObject defeatedPanel;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        aIEnemy = GetComponent<AIEnemy>();
        objectCollider = GetComponent<Collider>();

        attackInput = GetComponent<AttackInput>();
        interactInput = GetComponent<InteractInput>();
        playerCharacterInput = GetComponent<PlayerCharacterInput>();
        movementInput = GetComponent<CharacterMovementInput>();
        character = GetComponent<Character>();
    }

    public void Defeated()
    {
        SetState(false);
    }

    internal void Respawn()
    {
        SetState(true);
    }

    void SetState(bool state)
    {
        agent.isStopped = !state;
        agent.enabled = state;

        objectCollider.enabled = state;

        //AI Defeated part

        if (aIEnemy != null)
        {
            aIEnemy.enabled = state;
        }

        //Player Defeated part

        if (attackInput != null)
        {
            attackInput.enabled = state;
        }

        if (interactInput != null)
        {
            interactInput.enabled = state;
        }

        if (movementInput != null)
        {
            movementInput.enabled = state;
        }

        if (playerCharacterInput != null)
        {
            playerCharacterInput.enabled = state;
        }

        if (movementInput != null)
        {
            movementInput.enabled = state;
        }

        if (defeatedPanel != null)
        {
            defeatedPanel.SetActive(!state);
        }

        if(state == true) //Called only on respawn
        {
            if (character != null)
            {
                character.Restore();
            }
        }
    }
}
