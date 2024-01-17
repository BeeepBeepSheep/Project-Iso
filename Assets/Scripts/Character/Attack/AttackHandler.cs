using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    Character character;
    [SerializeField] float attackRange = 1f; // Range within which attacks can be performed
    [SerializeField] float defaultTimeToAttack = 2f;
    float attackTimer;

    Animator animator; // Reference to the Animator component for animation control
    CharacterMovement characterMovement; // Reference to the CharacterMovement script for character movement

    Character target; // Reference to the target object to attack

    private void Awake()
    {
        // Assigns the Animator component attached to a child GameObject (if any) to the 'animator' variable
        animator = GetComponentInChildren<Animator>();

        // Assigns the CharacterMovement component attached to this GameObject to the 'characterMovement' variable
        characterMovement = GetComponent<CharacterMovement>();

        character = GetComponent<Character>(); 
    }

    // Method called externally to initiate an attack on a specified target
    internal void Attack(Character target)
    {
        this.target = target; // Sets the target to attack
        ProcessAttack(); // Initiates the attack process
    }

    private void Update()
    {
        AttackTimerTick();

        // Checks if a target is present to attack
        if (target != null)
        {
            ProcessAttack(); // Initiates the attack process
        }
    }

    private void AttackTimerTick()
    {
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    // Method to process the attack logic
    private void ProcessAttack()
    {
        // Calculates the distance between this object and the target object
        float distance = Vector3.Distance(transform.position, target.transform.position);

        // Checks if the target is within attack range
        if (distance < attackRange)
        {
            if (attackTimer > 0f) { return; }

            attackTimer = GetAttackTime();

            characterMovement.Stop(); // Stops the character's movement
            animator.SetTrigger("Attack"); // Triggers the attack animation

            target.TakeDamage(character.TakeStats(Statistic.Damage).integer_value);

            target = null; // Clears the target after the attack
        }
        else
        {
            // Moves toward the target if it's out of attack range
            characterMovement.SetDestination(target.transform.position);
        }
    }

    float GetAttackTime()
    {
        float attackTime = defaultTimeToAttack;

        attackTime /= character.TakeStats(Statistic.AttackSpeed).float_value;

        return attackTime;
    }
}
