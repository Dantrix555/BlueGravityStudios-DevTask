using System;
using UnityEngine;
using System.Collections.Generic;

public class ItemsViewForPurchase : MonoBehaviour
{
    #region Fields and Properties

    [SerializeField]
    private ItemButton[] availableButtons; //This should be its own type of buttons

    #endregion

    #region Public Methods

    public void SetupItemsView(Action<SOEquipableData> onSelectedObject)
    {
        foreach (ItemButton button in availableButtons)
            button.SetupButton(onSelectedObject);

        gameObject.SetActive(false);
    }

    public void UpdateInventory(SOInventory selectedInventory)
    {
        List<SOEquipableData> equipables = selectedInventory.EquipableInventory;

        for (int i = 0; i < availableButtons.Length; i++)
        {
            if(equipables[i])
            {
                availableButtons[i].SetButtonNewItemData(equipables[i]);
                availableButtons[i].gameObject.SetActive(true);
            }
            else
                availableButtons[i].gameObject.SetActive(false);
        }

        gameObject.SetActive(true);
    }

    #endregion
}
