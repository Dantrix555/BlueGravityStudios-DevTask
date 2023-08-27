using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayInstaller : MonoBehaviour
{
    #region Fields and properties

    [SerializeField]
    private IInstaller[] installers;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        foreach (IInstaller installer in installers)
        {
            installer.Install(ServiceLocator.Instance);
        }
    }
}
