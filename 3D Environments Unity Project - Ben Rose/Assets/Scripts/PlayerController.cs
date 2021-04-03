﻿using UnityEngine;
using TMPro;

// Controls the player 5head | Base: https://bit.ly/391dTIA

/**
 * Controls: 
 * WASD: Move, Shift: Run
 * P: Interact with object (as it is in the spec, though 'E' is also bound because its a lot more convenient) 
 * Mouse0: Action with object in hand
**/

public class PlayerController : MonoBehaviour
{
#pragma warning disable 649 // SerializeField bug (https://bit.ly/3cMPR6C)
    [SerializeField] float walkSpeed = 2, runSpeed = 6, jumpHeight = 1, turnSmoothTime = 0.2f, speedSmoothTime = 0.1f, gravity = -12;
    [SerializeField] [Range(0, 1)] float airControl = 0.1f; // Closer to 1 -> more control when jumping

    [SerializeField] float strength = 10;
    [SerializeField] [Range(0, 1)] float strengthVariance = 0.9f; // Closer to 1 -> less strength can vary

    [SerializeField] float interactionRange = 10;
    [SerializeField] Camera playerCamera;

    [SerializeField] Transform pickableObjectPoint;
    [SerializeField] TMP_Text interactionText;
#pragma warning restore 649

    Animator animator;
    Transform cameraTransform;
    CharacterController controller;

    float turnSmoothVelocity, speedSmoothVelocity, currentSpeed, velocityY;

    InteractableObject currentInteractableObject, currentlyHoldingObject;

    const KeyCode RUN_KEY = KeyCode.LeftShift, JUMP_KEY = KeyCode.Space, INTERACTION_KEY = KeyCode.P, ALT_INTERACTION_KEY = KeyCode.E, ACTION_KEY = KeyCode.Mouse0;

    void Start()
    {
        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
        controller = GetComponent<CharacterController>();

        interactionText.CrossFadeAlpha(0, 0, false);
    }

    void Update()
    {
        // Movement
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDirection = input.normalized;
        bool isRunning = Input.GetKey(RUN_KEY);

        Move(inputDirection, isRunning);

        if (Input.GetKeyDown(JUMP_KEY)) Jump();

        // Interaction
        Interact();

        if (Input.GetKeyDown(INTERACTION_KEY) || Input.GetKeyDown(ALT_INTERACTION_KEY) && currentInteractableObject) // Interacting
        {
            currentInteractableObject.Interact(this);
            currentInteractableObject.DisplayMessage(false, this);
            currentInteractableObject = null;
        }
        else if (Input.GetKeyDown(INTERACTION_KEY) || Input.GetKeyDown(ALT_INTERACTION_KEY) && currentlyHoldingObject) // Uninteracting
        {
            currentlyHoldingObject.UnInteract(this);
            currentlyHoldingObject = null;
        }

        if (Input.GetKeyDown(ACTION_KEY) && currentlyHoldingObject) currentlyHoldingObject.Action(this); // Action

        // Animation
        float animationSpeedPercent = isRunning ? currentSpeed / runSpeed : currentSpeed / walkSpeed * 0.5f;
        animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
    }

    void Move(Vector2 inputDirection, bool isRunning)
    {
        if (inputDirection != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
        }

        float targetSpeed = (isRunning ? runSpeed : walkSpeed) * inputDirection.magnitude; // If there is no input, then the magnitude will be zero and therefore the speed 0
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

        velocityY += Time.deltaTime * gravity;
        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        if (controller.isGrounded) velocityY = 0;
    }

    void Jump()
    {
        if (!controller.isGrounded) return;

        float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
        velocityY = jumpVelocity;
    }

    float GetModifiedSmoothTime(float smoothTime) // Allows us to modify the degree of control the player has over the character when jumping
    {
        if (controller.isGrounded) return smoothTime;

        if (airControl == 0) return float.MaxValue;

        return smoothTime / airControl;
    }

    void Interact()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, interactionRange))
        {
            Collider hitCollider = hit.collider;

            if (hitCollider.CompareTag("InteractableObject") && !currentInteractableObject)
            {
                InteractableObject interactableObject = hitCollider.GetComponent<InteractableObject>();
                if (!interactableObject.GetInteractableState()) return;

                currentInteractableObject = interactableObject;
                currentInteractableObject.DisplayMessage(true, this);
            }
            else if (!hitCollider.CompareTag("InteractableObject") && currentInteractableObject)
            {
                currentInteractableObject.DisplayMessage(false, this);
                currentInteractableObject = null;
            }
        }
    }

    public void SetCurrentlyHoldingObject(InteractableObject _currentlyHoldingObject)
    {
        // The if checks make sure that we can't select an object we are currently holding. NameToLayer is a bit clearer than 0 / 2

        if (!_currentlyHoldingObject) currentlyHoldingObject.gameObject.layer = LayerMask.NameToLayer("Default");

        currentlyHoldingObject = _currentlyHoldingObject;

        if (currentlyHoldingObject) currentlyHoldingObject.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    public InteractableObject GetCurrentlyHoldingObject()
    {
        return currentlyHoldingObject;
    }

    public float GetStrength()
    {
        return Random.Range(strength * strengthVariance, strength / strengthVariance);
    }

    public Transform GetPickupPoint()
    {
        return pickableObjectPoint;
    }

    public TMP_Text GetInteractionText()
    {
        return interactionText;
    }

    public string GetKey(string type)
    {
        KeyCode key;

        switch (type)
        {
            case "Interaction":
                key = INTERACTION_KEY;
                break;
            case "Action":
                key = ACTION_KEY;
                break;
            default:
                throw new System.ArgumentException("'" + type + "' is an invalid key");
        }

        return key.ToString();
    }
}