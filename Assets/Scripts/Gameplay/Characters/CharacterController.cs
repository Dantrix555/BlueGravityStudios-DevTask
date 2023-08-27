using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : BaseCharacter, PlayerInputs.IPlayerMapActions
{
    #region Fields and Properties

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

    #region Player Input Maps inheritance

    public void OnInteract(InputAction.CallbackContext context)
    {
        //TODO: Check if player is interacting with something
        ServiceLocator.Instance.GetService<CanvasController>().ShowBuyingPanel(characterData.testInventory, false);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        actualCharacterDirection = context.ReadValue<Vector2>();
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        //Freeze game state and show inventory, if inventory opened, close inventory
        ServiceLocator.Instance.GetService<CanvasController>().ShowInventory(characterData.CharacterInventory);
    }

    #endregion
}
