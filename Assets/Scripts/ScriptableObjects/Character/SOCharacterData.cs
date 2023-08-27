using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterStatsData", menuName = "BGSDevTask/Create new character stats")]
public class SOCharacterData : ScriptableObject
{
    #region Fields and properties

    [Space(5)]
    [Header("Equipable parts")]
    [SerializeField]
    private SOEquipableData equipableHatData;
    [SerializeField]
    private SOEquipableData equipableHairData;
    [SerializeField]
    private SOEquipableData equipableOutfitData;

    [Space(5)]
    [Header("Other stats")]
    [SerializeField]
    private SOInventory characterInventory;

    public SOInventory testInventory;

    [SerializeField]
    [Range(0, 10)] [Min(0)]
    private float speed = 5f;

    public SOEquipableData EquipableHatData { get => equipableHatData; set => equipableHatData = value; }
    public SOEquipableData EquipableHairData { get => equipableHairData; set => equipableHairData = value; }
    public SOEquipableData EquipableOutfitData { get => equipableOutfitData; set => equipableOutfitData = value; }
    public SOInventory CharacterInventory => characterInventory;
    public float Speed => speed;

    #endregion
}
