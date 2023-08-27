using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasController : IInstaller
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
    private bool canvasControllerIsInstalled = false;

    #endregion

    #region IInstaller Override Methods

    public override void Install(ServiceLocator serviceLocator)
    {
        if (canvasControllerIsInstalled)
            return;

        canvasControllerIsInstalled = true;
        serviceLocator.RegisterService(this);
        SetupPanels();
    }

    #endregion

    #region Public Methods

    public void ShowChatPanel(SOCharacterDialog dialogToShow)
    {
        chatPanel.ShowPanel(dialogToShow);
        buyPanel.ClosePanel();
        inventoryPanel.ClosePanel();
    }

    public void ShowBuyingPanel(SOInventory sellerInventory, bool isHairCut)
    {
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
        chatPanel.SetupPanel();
        buyPanel.SetupPanel();
        inventoryPanel.SetupPanel();
    }

    private void SetupEventSystem(GameObject selectionGameObject)
    {
        eventSystem = EventSystem.current;
        EventSystem.current.sendNavigationEvents = true;
        EventSystem.current.SetSelectedGameObject(selectionGameObject);
    }

    #endregion
}
