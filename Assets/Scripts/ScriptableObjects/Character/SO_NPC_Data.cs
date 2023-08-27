using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCStatsData", menuName = "BGSDevTask/Create new NPC stats")]
public class SO_NPC_Data : SOCharacterData
{
    #region Fields and properties

    [SerializeField]
    private SOCharacterDialog defaultDialog;

    public SOCharacterDialog DefaultDialog => defaultDialog;

    #endregion
}
