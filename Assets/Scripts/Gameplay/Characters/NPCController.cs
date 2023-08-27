public class NPCController : BaseCharacter, IInteractable
{
    #region Fields and Properties

    private SO_NPC_Data npcData;

    #endregion

    #region Unity Methods

    private void Start()
    {
        npcData = (SO_NPC_Data)characterData;

        animator.SetFloat("IdleX", npcData.FacingVector.x);
        animator.SetFloat("IdleY", npcData.FacingVector.y);
    }

    #endregion

    #region IInteractable Implementation Methods

    public void TriggerInteraction()
    {
        ServiceLocator.Instance.GetService<CanvasController>().ShowChatPanel(npcData);
    }

    #endregion
}
