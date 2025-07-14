using System;
using UnityEngine;

public static class PoolCustom
{
    private static PoolHandleCustom _poolHandle;

    public static void InitPoolCustom()
    {
        if (_poolHandle == null)
        {
            _poolHandle = new PoolHandleCustom();
            _poolHandle.Initialize();
        }
    }


    #region API Spawn

    public static void PreSpawnCustom(this PoolData poolData)
    {
        if (_poolHandle == null)
        {
            Debug.Log($"Please init pool before {System.Reflection.MethodBase.GetCurrentMethod()?.Name}");
            return;
        }

        _poolHandle.PreSpawn(poolData);
    }

    public static GameObject SpawnCustom(this GameObject prefab, Transform parent = null,
        bool worldPositionStays = true,
        bool initialize = true, Action<GameObject> initAction = null)
    {
        if (_poolHandle == null)
        {
            Debug.Log($"Please init pool before {System.Reflection.MethodBase.GetCurrentMethod()?.Name}");
            return null;
        }

        return _poolHandle.Spawn(prefab, parent, worldPositionStays, initialize, initAction);
    }

    public static T SpawnCustom<T>(this T type, Transform parent = null, bool worldPositionStays = true,
        bool initialize = true, Action<GameObject> initAction = null) where T : Component
    {
        if (_poolHandle == null)
        {
            Debug.Log($"Please init pool before {System.Reflection.MethodBase.GetCurrentMethod()?.Name}");
            return null;
        }

        return _poolHandle.Spawn(type, parent, worldPositionStays, initialize, initAction).GetComponent<T>();
    }

    public static GameObject SpawnCustom(this GameObject prefab, Vector3 position, Quaternion rotation,
        Transform parent = null,
        bool worldPositionStays = true,
        bool initialize = true, Action<GameObject> initAction = null)
    {
        if (_poolHandle == null)
        {
            Debug.Log($"Please init pool before {System.Reflection.MethodBase.GetCurrentMethod()?.Name}");
            return null;
        }

        return _poolHandle.Spawn(prefab, position, rotation, parent, worldPositionStays, initialize, initAction);
    }

    public static T SpawnCustom<T>(this T type, Vector3 position, Quaternion rotation, Transform parent = null,
        bool worldPositionStays = true, bool initialize = true, Action<GameObject> initAction = null)
        where T : Component
    {
        if (_poolHandle == null)
        {
            Debug.Log($"Please init pool before {System.Reflection.MethodBase.GetCurrentMethod()?.Name}");
            return null;
        }

        return _poolHandle.Spawn(type, position, rotation, parent, worldPositionStays, initialize, initAction)
            .GetComponent<T>();
    }

    #endregion

    #region API DeSpawn

    public static void DeSpawnCustom(this GameObject gameObject, bool destroy = false, bool worldPositionStays = true)
    {
        if (_poolHandle == null)
        {
            Debug.Log($"Please init pool before {System.Reflection.MethodBase.GetCurrentMethod()?.Name}");
            return;
        }

        _poolHandle.DeSpawn(gameObject, destroy, worldPositionStays);
    }

    public static void DeSpawnCustom<T>(this T type, bool destroy = false, bool worldPositionStays = true)
        where T : Component
    {
        if (_poolHandle == null)
        {
            Debug.Log($"Please init pool before {System.Reflection.MethodBase.GetCurrentMethod()?.Name}");
            return;
        }

        _poolHandle.DeSpawn(type, destroy, worldPositionStays);
    }

    public static void DeSpawnAll()
    {
        if (_poolHandle == null)
        {
            Debug.Log($"Please init pool before {System.Reflection.MethodBase.GetCurrentMethod()?.Name}");
            return;
        }

        _poolHandle.DeSpawnAll();
    }

    #endregion

    #region API Destroy

    public static void DestroyAll()
    {
        if (_poolHandle == null)
        {
            Debug.Log($"Please init pool before {System.Reflection.MethodBase.GetCurrentMethod()?.Name}");
            return;
        }

        _poolHandle.DestroyAll();
    }

    public static void DestroyAllWaitPools()
    {
        if (_poolHandle == null)
        {
            Debug.Log($"Please init pool before {System.Reflection.MethodBase.GetCurrentMethod()?.Name}");
            return;
        }

        _poolHandle.DestroyAllWaitPools();
    }

    #endregion
}