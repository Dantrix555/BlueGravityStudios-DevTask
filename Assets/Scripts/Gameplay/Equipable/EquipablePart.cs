using UnityEngine;

public class EquipablePart : MonoBehaviour
{
    #region Fields and properties

    [SerializeField]
    private Animator equipableAnimator;

    [SerializeField]
    private EquipableType equipableExpectedType;

    #endregion

    #region Public Mehtods

    public void UpdateEquipable(SOEquipableData equipableData)
    {
        if(this.equipableExpectedType != equipableData.EquipableType)
        {
            Debug.LogWarning(string.Concat("Trying to equip an equipable type of ", equipableData.EquipableType.ToString(), " into ", gameObject.name,
                " which is an equipable type ", this.equipableExpectedType.ToString(), " returning ..."));
            return;
        }

        if(equipableData.EquipableAnimatorController != null)
        {
            gameObject.SetActive(true);
            transform.localPosition = equipableData.EquipablePosition;
            equipableAnimator.runtimeAnimatorController = equipableData.EquipableAnimatorController;
        }
        else
            gameObject.SetActive(false);

    }

    public void SetAnimationState(Vector2 movementDirection)
    {
        if (!gameObject.activeInHierarchy)
            return;

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
