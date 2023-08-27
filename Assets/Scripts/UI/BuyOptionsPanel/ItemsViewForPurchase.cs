using System;
using UnityEngine;
using System.Collections.Generic;

public class ItemsViewForPurchase : MonoBehaviour
{
    #region Fields and Properties

    [SerializeField]
    private ItemButton[] availableButtons;

    #endregion

    #region Public Methods

    public void SetupItemsView(Action<SOEquipableData> onSelectedObject)
    {
        foreach (ItemButton button in availableButtons)
            button.SetupButton(onSelectedObject);

        gameObject.SetActive(false);
    }

    public void UpdateInventory(SOInventory selectedInventory, bool playerIsBuying)
    {
        List<SOEquipableData> equipables = selectedInventory.EquipableInventory;
        int actualButtonIndex = 0;

        foreach (ItemButton button in availableButtons)
            button.gameObject.SetActive(false);
        
        foreach (SOEquipableData equipableItem in equipables)
        {
            if (equipableItem.EquipablePreviewImageSprite == null || 
                (!playerIsBuying && equipableItem.EquipableType == EquipableType.Hair))
                continue;

            ItemButton button = availableButtons[actualButtonIndex];

            button.SetButtonNewItemData(equipableItem);
            button.gameObject.SetActive(true);

            actualButtonIndex++;
        }

        gameObject.SetActive(true);
    }

    #endregion
}
