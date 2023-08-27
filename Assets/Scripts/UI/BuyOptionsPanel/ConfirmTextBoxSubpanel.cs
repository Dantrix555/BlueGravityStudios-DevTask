using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmTextBoxSubpanel : MonoBehaviour
{
    #region Fields and properties

    [SerializeField]
    private TMP_Text confirmationText;
    [SerializeField]
    private Button yesButton;
    [SerializeField]
    private Button noButton;

    #endregion

    #region Public Methods

    public void SetupConfirmationSubpanel(Action onDeclinedPurchase, Action onAcceptedPurchase)
    {
        noButton.onClick.AddListener(() => { onDeclinedPurchase?.Invoke(); });
        yesButton.onClick.AddListener(() => { onAcceptedPurchase?.Invoke(); });
    }

    public void UpdateEquipableData(string equipableItemName, int equipablePrice, bool playerIsBuying)
    {
        string actionName = playerIsBuying ? "buy" : "sell";
        confirmationText.text = string.Concat("Are you sure you want to ", actionName, " a ", equipableItemName, " its price is ", equipablePrice);

        gameObject.SetActive(true);
    }

    #endregion
}
