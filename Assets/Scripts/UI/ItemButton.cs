using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    #region Fields and properties

    [SerializeField]
    private Button itemButton;
    [SerializeField]
    private Image itemImage;

    private SOEquipableData itemData;
    private Action<SOEquipableData> onClikedButton;

    #endregion

    #region Public Methods

    public void SetupButton(Action<SOEquipableData> onClikedButton)
    {
        this.onClikedButton = onClikedButton;
        itemButton.onClick.AddListener(OnItemSelected);
    }

    public void SetButtonNewItemData(SOEquipableData itemData)
    {
        this.itemData = itemData;
        itemImage.sprite = itemData.EquipablePreviewImageSprite;
    }

    #endregion

    #region Inner Methods

    private void OnItemSelected()
    {
        if(!itemData)
        {
            Debug.LogError("Returning with error becasue button is trying to select an item that hasn't any data");
            return;
        }

        onClikedButton?.Invoke(itemData);
    }

    #endregion
}
