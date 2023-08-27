using UnityEngine;

[CreateAssetMenu(fileName = "NewMainCharacterStatsData", menuName = "BGSDevTask/Create new main character stats")]
public class MainCharacterData : SOCharacterData
{
    #region Fields and Properties

    [SerializeField]
    [Range(3, 10)] [Min(3)]
    private float detectionLinearDistance;

    [SerializeField]
    private LayerMask interactableLayer;

    public float DetectionLinearDistance => detectionLinearDistance;
    public LayerMask InteractableLayer => interactableLayer;

    #endregion
}
