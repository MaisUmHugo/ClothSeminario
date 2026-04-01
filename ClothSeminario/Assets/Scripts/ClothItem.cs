using UnityEngine;

public class ClothItem : MonoBehaviour, IPoolable
{
    private Cloth _cloth;
    private ClothMove _move;
    private ClothTarget _target;

    private void Awake()
    {
        _cloth = GetComponentInChildren<Cloth>();
        _move = GetComponent<ClothMove>();
        _target = GetComponent<ClothTarget>();
    }

    public void OnSpawnFromPool()
    {
        if (_cloth != null)
        {
            _cloth.enabled = false;
            _cloth.enabled = true;
        }

        _target?.ResetTarget();
    }

    public void OnReturnToPool()
    {
        if (_move != null)
        {
            _move.SetDirection(true);
        }
    }
}