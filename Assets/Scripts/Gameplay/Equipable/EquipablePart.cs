using UnityEngine;

public class EquipablePart : MonoBehaviour
{
    #region Fields and properties

    [SerializeField]
    private Animator equipableAnimator;

    [SerializeField]
    private SOEquipableData equipableData;

    #endregion

    #region Public Mehtods

    public void UpdateEquipable(SOEquipableData equipableData)
    {
        if(this.equipableData.EquipableType != equipableData.EquipableType)
        {
            Debug.LogWarning(string.Concat("Trying to equip an equipable type of ", equipableData.EquipableType.ToString(), " into ", gameObject.name,
                " which is an equipable type ", this.equipableData.EquipableType.ToString(), " returning ..."));
            return;
        }

        this.equipableData = equipableData;
        
        equipableAnimator.runtimeAnimatorController = equipableData.EquipableAnimatorController;
    }

    public void SetAnimationState(Vector2 movementDirection)
    {
        equipableAnimator.SetBool("IsMoving", movementDirection.x != 0 || movementDirection.y != 0);

        equipableAnimator.SetFloat("MoveX", movementDirection.x);
        equipableAnimator.SetFloat("MoveY", movementDirection.y);

        if (movementDirection.x != 0 || movementDirection.y != 0)
        {
            equipableAnimator.SetFloat("IdleX", movementDirection.x);
            equipableAnimator.SetFloat("IdleY", movementDirection.y);
        }
    }

    #endregion
}
