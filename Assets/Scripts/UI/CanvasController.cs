using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasController : IInstaller, ICanvasController
{
    #region Fields and properties

    [Header("Main Canvas Panels")]
    [SerializeField]
    private ChatPanelController chatPanel;
    [SerializeField]
    private BuyOptionsPanel buyPanel;
    [SerializeField]
    private InventoryPanel inventoryPanel;

    private EventSystem eventSystem;
    private GameStateController gameStateController;
    private bool canvasControllerIsInstalled = false;

    #endregion

    #region IInstaller Override Methods

    public override void Install(ServiceLocator serviceLocator)
    {
        if (canvasControllerIsInstalled)
            return;

        gameStateController = ServiceLocator.Instance.GetService<GameStateController>();

        canvasControllerIsInstalled = true;
        serviceLocator.RegisterService(this);
        SetupPanels();
    }

    #endregion

    #region Public Methods

    public void ShowChatPanel(SO_NPC_Data npcCharacterData)
    {
        chatPanel.ShowPanel(npcCharacterData);
        buyPanel.ClosePanel();
        inventoryPanel.ClosePanel();
        gameStateController.actualGameState = GameState.Chatting;
    }

    public void ShowBuyingPanel(SOInventory sellerInventory, bool isHairCut)
    {
        gameStateController.actualGameState = GameState.Buying;
        buyPanel.ShowPanel(sellerInventory, isHairCut);
        chatPanel.ClosePanel();
        inventoryPanel.ClosePanel();
    }

    public void ShowInventory(SOInventory characterInventory)
    {
        inventoryPanel.ShowPanel(characterInventory);
        chatPanel.ClosePanel();
        buyPanel.ClosePanel();
    }

    public void HideInventory()
    {
        chatPanel.ClosePanel();
        chatPanel.ClosePanel();
        inventoryPanel.ClosePanel();
    }

    #endregion

    #region Inner Methods

    private void SetupPanels()
    {
        chatPanel.SetupPanel(this);
        buyPanel.SetupPanel(this);
        inventoryPanel.SetupPanel(this);
    }

    #endregion

    #region ICanvasController implementation methods

    public void SetupEventSystem(GameObject selectionGameObject)
    {
        eventSystem = EventSystem.current;
        EventSystem.current.sendNavigationEvents = true;
        EventSystem.current.SetSelectedGameObject(selectionGameObject);
    }

    #endregion
}
