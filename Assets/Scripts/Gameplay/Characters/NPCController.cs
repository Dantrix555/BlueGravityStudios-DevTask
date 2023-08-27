using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : BaseCharacter
{
    #region Fields and Properties

    private SO_NPC_Data npcData;

    #endregion

    #region Unity Methods

    private void Start()
    {
        npcData = (SO_NPC_Data)characterData;
    }

    #endregion
}
