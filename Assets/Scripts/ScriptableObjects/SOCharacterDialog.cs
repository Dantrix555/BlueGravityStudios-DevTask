using System;
using UnityEngine;

public enum CharacterTalking { Player, ShopKeeper }

public enum ActionAfterDialog { None, ShowBuyingOptions }

[Serializable]
public struct DialogStruct
{
    [TextArea]
    public string dialogText;

    public CharacterTalking characterTalking;
}

[CreateAssetMenu(fileName = "NewCharacterDialogData", menuName = "BGSDevTask/Create new character dialog")]
public class SOCharacterDialog : ScriptableObject
{
    #region Fields and Properties

    [SerializeField]
    private DialogStruct[] dialogs;

    [SerializeField]
    private ActionAfterDialog actionAfterDialog;

    public DialogStruct[] Dialogs => dialogs;
    public ActionAfterDialog ActionAfterDialog => actionAfterDialog;

    #endregion
}
