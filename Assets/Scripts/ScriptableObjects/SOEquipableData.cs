using UnityEngine;

public enum EquipableType { Hat, Outfit, Hair }

[CreateAssetMenu(fileName = "NewEquipable", menuName = "BGSDevTask/Create new equipable")]
public class SOEquipableData : ScriptableObject
{
    #region Fields and properties

    [SerializeField]
    private EquipableType equipableType;

    [SerializeField]
    private Sprite equipablePreviewImage;
    
    [SerializeField]
    private RuntimeAnimatorController equipableAnimatorController;

    [SerializeField]
    private Vector3 equipablePosition;

    [SerializeField]
    private string equipableName;

    [SerializeField]
    private int equipablePrice;

    public EquipableType EquipableType => equipableType;
    public Sprite EquipablePreviewImage => equipablePreviewImage;
    public RuntimeAnimatorController EquipableAnimatorController => equipableAnimatorController;
    public Vector3 EquipablePosition => equipablePosition;
    public string EquipableName => equipableName;
    public int EquipablePrice => equipablePrice;

    #endregion
}
