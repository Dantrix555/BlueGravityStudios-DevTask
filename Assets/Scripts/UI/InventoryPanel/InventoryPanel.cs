using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour, IPanel
{
    #region Fields and properties

    [Header("Inventory buttons")]
    [SerializeField]
    private Button outfitsButton;
    [SerializeField]
    private Button hatsButton;
    [SerializeField]
    private List<ItemInventoryButton> availableButtons;

    [Space(5)]
    [Header("Description components")]
    [SerializeField]
    private Image selectedItemImage;
    [SerializeField]
    private TMP_Text descriptionText;

    [Space(5)]
    [Header("Other components")]
    [SerializeField]
    private SOInventory playerInventory;


    private CharactersInstaller charactersInstaller;
    private SOEquipableData cachedEquipedHat;
    private SOEquipableData cachedEquipedOutfit;

    #endregion

    #region IPanel implementation methods

    public void SetupPanel()
    {
        outfitsButton.onClick.AddListener(() => { OnSelectedItemType(EquipableType.Outfit); });
        hatsButton.onClick.AddListener(() => { OnSelectedItemType(EquipableType.Hat); });

        charactersInstaller = ServiceLocator.Instance.GetService<CharactersInstaller>();

        ResetItemDescription();

        availableButtons.ForEach(button => button.SetupButton(OnItemSet));
    }

    public void ClosePanel()
    {
        ResetItemDescription();
        gameObject.SetActive(false);
    }

    #endregion

    #region Public Methods

    public void ShowPanel(SOInventory playerInventory)
    {
        this.playerInventory = playerInventory;
        cachedEquipedHat = charactersInstaller.GetEquipableData(EquipableType.Hat);
        cachedEquipedOutfit = charactersInstaller.GetEquipableData(EquipableType.Outfit);
    }

    #endregion

    #region Inner Methods

    private void ResetItemDescription()
    {
        selectedItemImage.gameObject.SetActive(false);
        descriptionText.text = string.Empty;
    }

    private void OnSelectedItemType(EquipableType equipableItemType)
    {
        List<SOEquipableData> equipables = playerInventory.EquipableInventory;
        int actualButtonIndex = 0;

        availableButtons.ForEach(button => button.gameObject.SetActive(false));

        foreach (SOEquipableData equipableItem in equipables)
        {
            if(equipableItem.EquipableType == equipableItemType)
            {
                ItemInventoryButton button = availableButtons[actualButtonIndex];
                
                SOEquipableData equipableItemToCheck = equipableItemType == EquipableType.Hat ? cachedEquipedHat : cachedEquipedOutfit;
                button.SetFrameIfSelected(equipableItemToCheck);
                button.ShowButton(equipableItem);

                actualButtonIndex++;
            }
        }
    }

    private void OnItemSet(SOEquipableData itemData)
    {
        cachedEquipedHat = itemData.EquipableType == EquipableType.Hat ? itemData : cachedEquipedHat;
        cachedEquipedOutfit = itemData.EquipableType == EquipableType.Outfit ? itemData : cachedEquipedOutfit;
        charactersInstaller.UpdatePlayerData(itemData);

        selectedItemImage.gameObject.SetActive(true);
        selectedItemImage.sprite = itemData.EquipablePreviewImageSprite;
        descriptionText.text = itemData.EquipableDescription;
    }

    #endregion
}
