using UnityEngine;

public class ClothSpawner : MonoBehaviour
{
    [SerializeField] private string _poolKey = "ClothLine";
    [SerializeField] private Transform[] _leftSpawns;
    [SerializeField] private Transform[] _rightSpawns;
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
        bool spawnFromLeft = Random.value > 0.5f;

        Transform[] chosenSide = spawnFromLeft ? _leftSpawns : _rightSpawns;

        if (chosenSide == null || chosenSide.Length == 0)
            return;

        Transform spawnPoint = chosenSide[Random.Range(0, chosenSide.Length)];

        GameObject obj = PoolManager.Instance.GetObject(
            _poolKey,
            spawnPoint.position,
            spawnPoint.rotation
        );

        if (obj == null)
            return;

        ClothMove move = obj.GetComponent<ClothMove>();
        if (move != null)
        {
            move.SetDirection(spawnFromLeft);
        }
    }
}