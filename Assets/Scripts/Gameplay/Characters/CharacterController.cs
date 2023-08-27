using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : BaseCharacter, PlayerInputs.IPlayerMapActions, IInteractor
{
    #region Fields and Properties

    private Collider2D[] collidersInFrontDetectedList = new Collider2D[1];
    private int collidersInFrontDetected = 0;

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
        MoveCharacter();
        DetectInteractablesInFront(actualCharacterDirection);
    }

    #endregion

    #region Player Input Maps inheritance

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (nearestInteractable != null)
            nearestInteractable.TriggerInteraction();
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
