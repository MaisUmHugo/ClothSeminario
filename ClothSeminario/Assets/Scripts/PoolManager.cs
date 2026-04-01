using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [SerializeField] private List<PoolConfig> _poolConfigs = new();

    private Dictionary<string, Queue<GameObject>> _pools = new();
    private Dictionary<string, PoolConfig> _configLookup = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        InitializePools();
    }

    private void InitializePools()
    {
        foreach (PoolConfig config in _poolConfigs)
        {
            if (string.IsNullOrWhiteSpace(config.poolKey) || config.prefab == null)
                continue;

            Queue<GameObject> queue = new Queue<GameObject>();
            _pools[config.poolKey] = queue;
            _configLookup[config.poolKey] = config;

            for (int i = 0; i < config.initialSize; i++)
            {
                GameObject obj = CreateNewObject(config);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
        }
    }

    private GameObject CreateNewObject(PoolConfig config)
    {
        GameObject obj = Instantiate(config.prefab, transform);

        PoolObject poolObject = obj.GetComponent<PoolObject>();
        if (poolObject == null)
        {
            poolObject = obj.AddComponent<PoolObject>();
        }

        poolObject.SetPoolKey(config.poolKey);

        return obj;
    }

    public GameObject GetObject(string poolKey, Vector3 position, Quaternion rotation)
    {
        if (!_pools.ContainsKey(poolKey))
        {
            Debug.LogWarning($"Pool '{poolKey}' não encontrada.");
            return null;
        }

        Queue<GameObject> queue = _pools[poolKey];
        GameObject obj = null;

        while (queue.Count > 0 && obj == null)
        {
            obj = queue.Dequeue();
        }

        if (obj == null)
        {
            PoolConfig config = _configLookup[poolKey];

            if (!config.canExpand)
            {
                Debug.LogWarning($"Pool '{poolKey}' sem objetos disponíveis e expansão desativada.");
                return null;
            }

            obj = CreateNewObject(config);
        }

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);

        IPoolable poolable = obj.GetComponent<IPoolable>();
        poolable?.OnSpawnFromPool();

        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        if (obj == null)
            return;

        PoolObject poolObject = obj.GetComponent<PoolObject>();

        if (poolObject == null)
        {
            Debug.LogWarning("Objeto retornado para pool sem PoolObject.");
            obj.SetActive(false);
            return;
        }

        string poolKey = poolObject.PoolKey;

        if (!_pools.ContainsKey(poolKey))
        {
            Debug.LogWarning($"Pool '{poolKey}' não existe.");
            obj.SetActive(false);
            return;
        }

        IPoolable poolable = obj.GetComponent<IPoolable>();
        poolable?.OnReturnToPool();

        obj.SetActive(false);
        obj.transform.SetParent(transform);

        _pools[poolKey].Enqueue(obj);
    }
}