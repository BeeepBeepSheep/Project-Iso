using CharacterCommand;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerCharacterInput : MonoBehaviour
{
    [SerializeField] MouseInput mouseInput;
    CommandHandler commandHandler;

    CharacterMovementInput characterMovementInput;
    AttackInput attackInput;
    InteractInput interactInput;

    bool isOverUIElement;
    bool isLMBPressed;

    private void Awake()
    {
        commandHandler = GetComponent<CommandHandler>();

        characterMovementInput = GetComponent<CharacterMovementInput>();
        attackInput = GetComponent<AttackInput>();
        interactInput = GetComponent<InteractInput>();
    }

    private void Update()
    {
        isOverUIElement = EventSystem.current.IsPointerOverGameObject();
        LMB_Hold_CommandProcess();
    }

    private void LMB_Hold_CommandProcess()
    {
        if (commandHandler.GetCurrentCommandType() == CommandType.Interact) { return; }
        if (isLMBPressed)
        {
            if (attackInput.AttackTargetCheck())
            {
                if (attackInput.AttackCooldownCheck() )
                {
                    AttackCommand(interactInput.hoveringOverObject.gameObject);
                }
                return;
            }

            MoveCommand(mouseInput.rayToWorldIntersectionPoint);
        }
    }

    public void LMB_InputHandle(InputAction.CallbackContext callbackContext)
    {

        if (isOverUIElement == true) { return; }

        LMB_Hold(callbackContext);

        if (callbackContext.performed || callbackContext.canceled) { return; }

        LMB_Press_ProcessCommand();
    }

    private void LMB_Press_ProcessCommand()
    {
        if (attackInput.AttackTargetCheck() && attackInput.AttackCooldownCheck())
        {
            AttackCommand(interactInput.hoveringOverObject.gameObject);
            return;
        }

        if (interactInput.InteractCheck())
        {
            InteractCommand(interactInput.hoveringOverObject.gameObject);
            return;
        }

        MoveCommand(mouseInput.rayToWorldIntersectionPoint);
    }

    private void LMB_Hold(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            isLMBPressed = true;
        }

        if (callbackContext.canceled)
        {
            isLMBPressed = false;
        }
    }

    private void MoveCommand(Vector3 point)
    {
        commandHandler.SetCommand(new Command(CommandType.Move, point));
    }

    private void InteractCommand(GameObject target)
    {
        commandHandler.SetCommand(new Command(CommandType.Interact, target));
    }

    private void AttackCommand(GameObject target)
    {
        commandHandler.SetCommand(new Command(CommandType.Attack, target));
    }
}
