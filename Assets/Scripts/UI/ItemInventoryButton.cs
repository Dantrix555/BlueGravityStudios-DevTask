using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventoryButton : MonoBehaviour
{
    #region Fields and properties

    [SerializeField]
    private Button itemButton;
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private GameObject selectedFrame;

    private SOEquipableData cachedEquipableData;
    private Action<SOEquipableData> onSelectedAction;

    #endregion

    #region Public Methods

    public void SetupButton(Action<SOEquipableData> onSelectedAction)
    {
        this.onSelectedAction = onSelectedAction;
        itemButton.onClick.AddListener(OnItemSelected);
    }

    public void ShowButton(SOEquipableData equipableData)
    {
        cachedEquipableData = equipableData;
        itemImage.sprite = equipableData.EquipablePreviewImageSprite;
        gameObject.SetActive(true);
    }

    public void SetFrameIfSelected(SOEquipableData equipableDataToCheck)
    {
        selectedFrame.SetActive(cachedEquipableData == equipableDataToCheck);
    }

    #endregion

    #region Inner Methods

    private void OnItemSelected()
    {
        onSelectedAction?.Invoke(cachedEquipableData);
        selectedFrame.SetActive(true);
    }

    #endregion
}
