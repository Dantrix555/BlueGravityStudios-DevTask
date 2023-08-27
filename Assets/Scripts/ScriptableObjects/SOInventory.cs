using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventory", menuName = "BGSDevTask/Create new inventory")]
public class SOInventory : ScriptableObject
{
    #region Fields and properties

    [SerializeField]
    private List<SOEquipableData> equipableInventory;
    [SerializeField]
    private bool isFullyHaircutInventory;

    public List<SOEquipableData> EquipableInventory => equipableInventory;
    public bool IsFullyHaircutInventory => isFullyHaircutInventory;

    #endregion

    #region Public Methods

    public void RemoveEquipableFromInventory(SOEquipableData equipableToRemove)
    {
        equipableInventory.Remove(equipableToRemove);
    }

    public void AddEquipableToInventory(SOEquipableData equipableToAdd)
    {
        equipableInventory.Add(equipableToAdd);
    }

    #endregion
}
