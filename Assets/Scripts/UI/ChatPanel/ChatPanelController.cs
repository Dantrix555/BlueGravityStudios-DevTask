using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatPanelController : MonoBehaviour, IPanel
{
    #region Fields and Properties

    [SerializeField]
    private TMP_Text chatText;
    [SerializeField]
    private Button nextButton;

    private SOCharacterDialog actualDialog;
    private int actualDialogIndex;

    #endregion

    #region IPanel Implemented Methods

    public void SetupPanel()
    {
        chatText.text = string.Empty;
        nextButton.onClick.AddListener(OnNextButton);
        gameObject.SetActive(false);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    #endregion

    #region Public Methods

    public void ShowPanel(SOCharacterDialog newDialog)
    {
        actualDialog = newDialog;
        gameObject.SetActive(true);
        //Set here the first dialog text
    }

    #endregion

    #region Inner Methods

    private void OnNextButton()
    {
        //Check if more text should be displayed or if only is necesary to hide object
    }

    #endregion
}
