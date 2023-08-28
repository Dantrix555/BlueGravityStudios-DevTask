using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Playing, Inventory, Buying, Chatting }

public class GameStateController : IInstaller
{
    #region Fields and Properties

    [HideInInspector]
    public GameState actualGameState;

    #endregion

    #region IInstall Implementation Methods

    public override void Install(ServiceLocator serviceLocator)
    {
        serviceLocator.RegisterService(this);
        actualGameState = GameState.Playing;
    }

    #endregion

}
