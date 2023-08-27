using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour, PlayerInputs.IPlayerMapActions
{
    #region Fields and Properties

    [Header("Main Components")]
    [SerializeField]
    private Rigidbody2D rigidboby;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float speed = 5f;

    [Space(5)]
    [Header("Equipable parts")]
    [SerializeField]
    private EquipablePart equipableHat;
    [SerializeField]
    private EquipablePart equipableHair;
    [SerializeField]
    private EquipablePart equipableOutfit;

    private Vector2 actualCharacterDirection;
    private PlayerInputs playerInputs;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        playerInputs = new PlayerInputs();
        playerInputs.PlayerMap.SetCallbacks(this);
    }

    private void OnEnable()
    {
        playerInputs.Enable();
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }

    private void FixedUpdate()
    {
        //Check if player isn't on inventory or purchasing equipables here

        MoveCharacter();
    }

    #endregion

    #region Inner Methods

    private void MoveCharacter()
    {
        animator.SetBool("IsMoving", actualCharacterDirection.x != 0 || actualCharacterDirection.y != 0);

        rigidboby.velocity = actualCharacterDirection * speed;

        animator.SetFloat("MoveX", actualCharacterDirection.x);
        animator.SetFloat("MoveY", actualCharacterDirection.y);

        if (actualCharacterDirection.x != 0 || actualCharacterDirection.y != 0)
        {
            animator.SetFloat("IdleX", actualCharacterDirection.x);
            animator.SetFloat("IdleY", actualCharacterDirection.y);
        }

        equipableHat.SetAnimationState(actualCharacterDirection);
        equipableHair.SetAnimationState(actualCharacterDirection);
        equipableOutfit.SetAnimationState(actualCharacterDirection);
    }

    #endregion

    #region Player Input Maps inheritance

    public void OnInteract(InputAction.CallbackContext context)
    {
        //TODO: Check if player is interacting with something
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        actualCharacterDirection = context.ReadValue<Vector2>();
    }

    #endregion
}
