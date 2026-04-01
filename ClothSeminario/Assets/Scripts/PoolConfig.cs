using UnityEngine;

[System.Serializable]
public class PoolConfig
{
    public string poolKey;
    public GameObject prefab;
    public int initialSize;
    public bool canExpand = true;
}