using System;
using UnityEngine.Assertions;
using System.Collections.Generic;

public class ServiceLocator
{
    #region Fields and properties

    public static ServiceLocator Instance => _instance ?? (_instance = new ServiceLocator());
    private static ServiceLocator _instance;

    private readonly Dictionary<Type, object> _servicesDictionary;

    #endregion

    #region Constructor

    private ServiceLocator()
    {
        _servicesDictionary = new Dictionary<Type, object>();
    }

    #endregion

    #region Public Methods

    public void RegisterService<T>(T newService)
    {
        Type type = typeof(T);
        Assert.IsFalse(_servicesDictionary.ContainsKey(type), $"Service {type} is already registered");

        _servicesDictionary.Add(type, newService);
    }

    public void UnregisterService<T>()
    {
        Type type = typeof(T);
        Assert.IsTrue(_servicesDictionary.ContainsKey(type), $"Service {type} is not registered");

        _servicesDictionary.Remove(type);
    }

    public T GetService<T>()
    {
        Type type = typeof(T);
        if (!_servicesDictionary.TryGetValue(type, out object service))
        {
            throw new Exception($"Service {type} not found");
        }

        return (T)service;
    }

    public bool Contains<T>()
    {
        Type type = typeof(T);
        return _servicesDictionary.ContainsKey(type);
    }

    #endregion
}
