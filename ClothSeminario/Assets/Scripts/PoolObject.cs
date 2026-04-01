using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public string PoolKey { get; private set; }

    public void SetPoolKey(string poolKey)
    {
        PoolKey = poolKey;
    }
}