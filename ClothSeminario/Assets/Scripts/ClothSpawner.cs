using UnityEngine;

[System.Serializable]
public class ClothesLineData
{
    public Transform leftSpawn;
    public Transform rightSpawn;
    public CapsuleCollider[] colliders;
}

public class ClothSpawner : MonoBehaviour
{
    [SerializeField] private string _poolKey = "ClothLine";
    [SerializeField] private ClothesLineData[] _lines;
    [SerializeField] private float _spawnInterval = 2f;

    private float _timer;

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer > 0f)
            return;

        SpawnCloth();
        _timer = _spawnInterval;
    }

    private void SpawnCloth()
    {
        if (_lines == null || _lines.Length == 0)
            return;

        int lineIndex = Random.Range(0, _lines.Length);
        ClothesLineData line = _lines[lineIndex];

        if (line.leftSpawn == null || line.rightSpawn == null)
            return;

        bool spawnFromLeft = Random.value > 0.5f;
        Transform spawnPoint = spawnFromLeft ? line.leftSpawn : line.rightSpawn;

        GameObject obj = PoolManager.Instance.GetObject(
            _poolKey,
            spawnPoint.position,
            Quaternion.identity
        );

        if (obj == null)
            return;

        ClothMove move = obj.GetComponent<ClothMove>();
        if (move != null)
        {
            move.SetDirection(spawnFromLeft);
        }

        ClothColliderSetter setter = obj.GetComponent<ClothColliderSetter>();
        if (setter != null && line.colliders != null && line.colliders.Length > 0)
        {
            setter.SetCapsuleColliders(line.colliders);
        }
    }
}