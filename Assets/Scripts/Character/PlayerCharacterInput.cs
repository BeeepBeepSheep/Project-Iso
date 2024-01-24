using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerCharacterInput : MonoBehaviour
{
    [SerializeField] MouseInput mouseInput;

    CharacterMovementInput characterMovementInput;
    AttackInput attackInput;
    InteractInput interactInput;

    bool isOverUIElement;

    private void Awake()
    {
        characterMovementInput = GetComponent<CharacterMovementInput>();
        attackInput = GetComponent<AttackInput>();
        interactInput = GetComponent<InteractInput>();
    }

    private void Update()
    {
        isOverUIElement = EventSystem.current.IsPointerOverGameObject();
    }

    public void LMB_InputHandle(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {

            if (isOverUIElement == true) { return; }

            if (callbackContext.performed)
            {
                if (attackInput.AttackCheck())
                {
                    attackInput.Attack();
                    return;
                }
            }

            if (interactInput.InteractCheck())
            {
                interactInput.Interact();
                return;
            }

            interactInput.ResetState();
            characterMovementInput.MoveInput();
        }
    }
}
