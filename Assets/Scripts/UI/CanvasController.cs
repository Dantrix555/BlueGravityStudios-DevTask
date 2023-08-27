using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : IInstaller
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region IInstaller Implementation Methods

    public override void Install(ServiceLocator serviceLocator)
    {
        serviceLocator.RegisterService(this);
    }

    #endregion
}
