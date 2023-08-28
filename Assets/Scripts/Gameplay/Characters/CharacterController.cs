using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : BaseCharacter, PlayerInputs.IPlayerMapActions, IInteractor
{
    #region Fields and Properties

    private Collider2D[] collidersInFrontDetectedList = new Collider2D[1];
    private int collidersInFrontDetected = 0;

    private GameStateController gameStateController;
    private MainCharacterData mainCharacterData;
    private PlayerInputs playerInputs;
    private IInteractable nearestInteractable;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        playerInputs = new PlayerInputs();
        playerInputs.PlayerMap.SetCallbacks(this);
    }

    private void Start()
    {
        mainCharacterData = (MainCharacterData)characterData;
        gameStateController = ServiceLocator.Instance.GetService<GameStateController>();
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
        if (gameStateController.actualGameState != GameState.Playing)
            return;

        MoveCharacter();
        DetectInteractablesInFront(actualCharacterDirection);
    }

    #endregion

    #region Player Input Maps inheritance

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (nearestInteractable != null && gameStateController.actualGameState == GameState.Playing)
            nearestInteractable.TriggerInteraction();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        actualCharacterDirection = context.ReadValue<Vector2>();
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if(gameStateController.actualGameState == GameState.Playing)
        {
            ServiceLocator.Instance.GetService<CanvasController>().ShowInventory(characterData.CharacterInventory);
            gameStateController.actualGameState = GameState.Inventory;
        }
        else if(gameStateController.actualGameState == GameState.Inventory)
        {
            ServiceLocator.Instance.GetService<CanvasController>().HideInventory();
            gameStateController.actualGameState = GameState.Playing;
        }
    }
    #endregion

    #region IInteractor Implementation Methods

    public void DetectInteractablesInFront(Vector3 movingDirection)
    {
        Vector3 boxColliderSize = new Vector3(mainCharacterData.DetectionLinearDistance, mainCharacterData.DetectionLinearDistance);
        collidersInFrontDetected = Physics2D.OverlapBoxNonAlloc(transform.position + movingDirection.normalized, boxColliderSize, 0, collidersInFrontDetectedList, mainCharacterData.InteractableLayer);

        if (collidersInFrontDetected <= 0)
        {
            nearestInteractable = null;
            return;
        }

        if (collidersInFrontDetectedList[0].TryGetComponent(out IInteractable newInteractable))
            nearestInteractable = newInteractable;
    }

    #endregion
}
