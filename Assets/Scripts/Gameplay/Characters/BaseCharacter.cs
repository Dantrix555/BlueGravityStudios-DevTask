using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    #region Fields and properties

    [Header("Main Components")]
    [SerializeField]
    private Rigidbody2D rigidboby;
    [SerializeField]
    private Animator animator;

    [Space(5)]
    [Header("Equipable parts")]
    [SerializeField]
    private EquipablePart equipableHat;
    [SerializeField]
    private EquipablePart equipableHair;
    [SerializeField]
    private EquipablePart equipableOutfit;

    protected SOCharacterData characterData;
    protected Vector2 actualCharacterDirection;

    #endregion

    #region Public Methods

    public void UpdateCharacterData(SOCharacterData characterData)
    {
        this.characterData = characterData;
        equipableHat.UpdateEquipable(characterData.EquipableHatData);
        equipableHair.UpdateEquipable(characterData.EquipableHairData);
        equipableOutfit.UpdateEquipable(characterData.EquipableOutfitData);
    }

    #endregion

    #region Protected Methods

    protected void MoveCharacter()
    {
        animator.SetBool("IsMoving", actualCharacterDirection.x != 0 || actualCharacterDirection.y != 0);

        rigidboby.velocity = actualCharacterDirection * characterData.Speed;

        animator.SetFloat("MoveX", actualCharacterDirection.x);
        animator.SetFloat("MoveY", actualCharacterDirection.y);

        if (actualCharacterDirection.x != 0 || actualCharacterDirection.y != 0)
        {
            animator.SetFloat("IdleX", actualCharacterDirection.x);
            animator.SetFloat("IdleY", actualCharacterDirection.y);
        }

        equipableHat.SetAnimationState(actualCharacterDirection);
        equipableHair.SetAnimationState(actualCharacterDirection);
        equipableOutfit.SetAnimationState(actualCharacterDirection);
    }

    #endregion
}
