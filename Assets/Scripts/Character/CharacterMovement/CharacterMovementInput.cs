using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementInput : MonoBehaviour
{
    [SerializeField] MouseInput mouseInput; // Reference to MouseInput script for mouse input handling
    CharacterMovement characterMovement; // Reference to the CharacterMovement script for character movement control

    private void Awake()
    {
        // Assigns the CharacterMovement component attached to this GameObject to the 'characterMovement' variable
        characterMovement = GetComponent<CharacterMovement>();
    }

    public void MoveInput()
    {
        characterMovement.SetDestination(mouseInput.mouseInputPosition);

    }
}
