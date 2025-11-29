using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<MonoBehaviour> systems; 
    
    public void AddSystem<T>(in T system) where T : MonoBehaviour
    {
        if (HasSystem<T>())
        {
            Debug.LogWarning("System already exists!");
            return;
        }
        
        systems.Add(system);
    }
    
    public bool HasSystem<T>() where T : MonoBehaviour
    {
        foreach (MonoBehaviour system in systems)
        {
            T s = system as T;
            if (s != null)
                return true;
        }
        
        return false;
    }
    
    public bool GetSystem<T>(out T result) where T : MonoBehaviour
    {
        foreach (MonoBehaviour system in systems)
        {
            T s = system as T;
            if (s != null)
            {
                result = s;
                return true;
            }
        }

        result = null;
        return false;
    }
}
