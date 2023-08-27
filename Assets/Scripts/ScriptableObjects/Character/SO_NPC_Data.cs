using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCStatsData", menuName = "BGSDevTask/Create new NPC stats")]
public class SO_NPC_Data : SOCharacterData
{
    #region Fields and properties

    [SerializeField]
    private SOCharacterDialog defaultDialog;
    [SerializeField]
    private Vector2 facingVector;

    public SOCharacterDialog DefaultDialog => defaultDialog;
    public Vector2 FacingVector => facingVector;

    #endregion
}
