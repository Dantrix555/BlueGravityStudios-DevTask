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

    private SO_NPC_Data npcCharacterData;
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

    public void ShowPanel(SO_NPC_Data npcCharacterData)
    {
        this.npcCharacterData = npcCharacterData;
        actualDialog = npcCharacterData.DefaultDialog;
        actualDialogIndex = 0;
        OnNextButton();
        gameObject.SetActive(true);
    }

    #endregion

    #region Inner Methods

    private void OnNextButton()
    {
        if(actualDialogIndex < actualDialog.Dialogs.Length - 1)
        {
            chatText.text = string.Concat(actualDialog.Dialogs[actualDialogIndex].characterTalking, ": ", actualDialog.Dialogs[actualDialogIndex].dialogText);
            actualDialogIndex++;
        }
        else
        {
            if (actualDialog.ActionAfterDialog == ActionAfterDialog.ShowBuyingOptions)
                ServiceLocator.Instance.GetService<CanvasController>().ShowBuyingPanel(npcCharacterData.CharacterInventory, npcCharacterData.CharacterInventory.IsFullyHaircutInventory);

            ClosePanel();
        }
    }

    #endregion
}
