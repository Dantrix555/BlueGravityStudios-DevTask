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

    private GameStateController gameStateController;

    private SO_NPC_Data npcCharacterData;
    private SOCharacterDialog actualDialog;
    private int actualDialogIndex;

    private ICanvasController cachedCanvasController;
    public ICanvasController CanvasController => cachedCanvasController;

    #endregion

    #region IPanel Implemented Methods

    public void SetupPanel(ICanvasController canvasReference)
    {
        chatText.text = string.Empty;
        nextButton.onClick.AddListener(OnNextButton);
        gameObject.SetActive(false);

        gameStateController = ServiceLocator.Instance.GetService<GameStateController>();

        cachedCanvasController = canvasReference;
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

        cachedCanvasController.SetupEventSystem(nextButton.gameObject);
    }

    #endregion

    #region Inner Methods

    private void OnNextButton()
    {
        if(actualDialogIndex < actualDialog.Dialogs.Length)
        {
            chatText.text = string.Concat(actualDialog.Dialogs[actualDialogIndex].characterTalking, ": ", actualDialog.Dialogs[actualDialogIndex].dialogText);
            actualDialogIndex++;
        }
        else
        {
            if (actualDialog.ActionAfterDialog == ActionAfterDialog.ShowBuyingOptions)
            {
                ServiceLocator.Instance.GetService<CanvasController>().ShowBuyingPanel(npcCharacterData.CharacterInventory, npcCharacterData.CharacterInventory.IsFullyHaircutInventory);
                gameStateController.actualGameState = GameState.Buying;
            }
            else
                gameStateController.actualGameState = GameState.Playing;

            ClosePanel();
        }
    }

    #endregion
}
