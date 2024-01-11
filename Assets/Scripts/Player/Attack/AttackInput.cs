using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInput : MonoBehaviour
{
    InteractInput interactInput; // Reference to the InteractInput script for detecting hovered objects
    AttackHandler attackHandler; // Reference to the AttackHandler script for performing attacks

    private void Awake()
    {
        // Retrieves the InteractInput component attached to this GameObject and assigns it to the 'interactInput' variable
        interactInput = GetComponent<InteractInput>();

        // Retrieves the AttackHandler component attached to this GameObject and assigns it to the 'attackHandler' variable
        attackHandler = GetComponent<AttackHandler>();
    }

    public void Attack()
    {
        attackHandler.Attack(interactInput.hoveringOverCharacter);
    }

    public bool AttackCheck()
    {
        return interactInput.hoveringOverCharacter != null;
    }
}
