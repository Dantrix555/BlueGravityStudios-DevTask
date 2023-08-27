using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IInstaller : MonoBehaviour
{
    public abstract void Install(ServiceLocator serviceLocator);
}
