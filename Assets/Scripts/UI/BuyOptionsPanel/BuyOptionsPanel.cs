using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyOptionsPanel : MonoBehaviour, IPanel
{
    #region Fields and properties

    [Header("Subpanels")]
    [SerializeField]
    private ItemsViewForPurchase itemsView;
    [SerializeField]
    private ConfirmTextBoxSubpanel confirmationSubpanel;

    [Space(5)]
    [Header("Options Buttons")]
    [SerializeField]
    private Button buyButton;
    [SerializeField]
    private Button sellButton;
    [SerializeField]
    private Button exitButton;

    [Space(5)]
    [Header("Other components")]
    [SerializeField]
    private SOInventory playerInventory;

    private SOInventory sellerInventory;
    SOEquipableData cachedEquipableData;
    private bool playerIsBuying;

    private ICanvasController cachedCanvasController;
    public ICanvasController CanvasController => cachedCanvasController;

    #endregion

    #region IPanel Implemented Methods

    public void SetupPanel(ICanvasController canvasReference)
    {
        itemsView.SetupItemsView(OnSelectedItem);
        confirmationSubpanel.SetupConfirmationSubpanel(OnDeclineConfirmation, OnAceptedConfirmation);

        buyButton.onClick.AddListener(() => { OnBuySelected(true); });
        sellButton.onClick.AddListener(() => { OnBuySelected(false); });
        exitButton.onClick.AddListener(OnExitSelected);
        gameObject.SetActive(false);

        cachedCanvasController = canvasReference;
    }

    public void ClosePanel()
    {
        OnExitSelected();
    }

    #endregion

    #region Public Methods

    public void ShowPanel(SOInventory sellerInventory, bool isHairCut)
    {
        sellButton.gameObject.SetActive(!isHairCut);
        this.sellerInventory = sellerInventory;
        gameObject.SetActive(true);

        itemsView.gameObject.SetActive(false);
        confirmationSubpanel.gameObject.SetActive(false);

        cachedCanvasController.SetupEventSystem(buyButton.gameObject);
    }

    #endregion

    #region Inner Methods

    private void OnBuySelected(bool playerIsBuying)
    {
        this.playerIsBuying = playerIsBuying;
        SOInventory inventoryToBeSet = playerIsBuying ? sellerInventory : playerInventory;
        itemsView.UpdateInventory(inventoryToBeSet, playerIsBuying);
    }

    private void OnExitSelected()
    {
        itemsView.gameObject.SetActive(false);
        confirmationSubpanel.gameObject.SetActive(false);
        gameObject.SetActive(false);
        ServiceLocator.Instance.GetService<GameStateController>().actualGameState = GameState.Playing;
    }

    private void OnSelectedItem(SOEquipableData equipableData)
    {
        confirmationSubpanel.UpdateEquipableData(equipableData.EquipableName, equipableData.EquipablePrice, playerIsBuying);
        cachedEquipableData = equipableData;
    }

    private void OnDeclineConfirmation()
    {
        confirmationSubpanel.gameObject.SetActive(false);
    }

    private void OnAceptedConfirmation()
    {
        if (playerIsBuying && cachedEquipableData.EquipableType != EquipableType.Hair)
        {
            playerInventory.AddEquipableToInventory(cachedEquipableData);
            sellerInventory.RemoveEquipableFromInventory(cachedEquipableData);
        }
        else if(playerIsBuying && cachedEquipableData.EquipableType == EquipableType.Hair)
        {
            ServiceLocator.Instance.GetService<CharactersInstaller>().UpdatePlayerData(cachedEquipableData);
        }
        else if (!playerIsBuying)
        {
            sellerInventory.AddEquipableToInventory(cachedEquipableData);
            playerInventory.RemoveEquipableFromInventory(cachedEquipableData);
            ServiceLocator.Instance.GetService<CharactersInstaller>().CheckIfSoldPartShouldBeRemoved(cachedEquipableData);
        }

        itemsView.gameObject.SetActive(false);
        confirmationSubpanel.gameObject.SetActive(false);
    }

    #endregion
}
