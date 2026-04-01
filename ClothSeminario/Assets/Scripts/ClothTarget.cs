using UnityEngine;

public class ClothTarget : MonoBehaviour
{
    [SerializeField] private int _points = 10;
    [SerializeField] private int _hitsToDisable = 3;
    [SerializeField] private Cloth _cloth;
    [SerializeField] private ClothMove _clothMove;

    private int _currentHits;

    private void Awake()
    {
        if (_cloth == null)
            _cloth = GetComponentInChildren<Cloth>();

        if (_clothMove == null)
            _clothMove = GetComponent<ClothMove>();
    }

    public void Hit(Vector3 hitPoint, Vector3 shotDirection)
    {
        _currentHits++;

        ScoreManager.Instance?.AddScore(_points);

        ReactToShot(shotDirection);

        if (_currentHits >= _hitsToDisable)
        {
            PoolManager.Instance.ReturnObject(gameObject);
        }
    }

    private void ReactToShot(Vector3 shotDirection)
    {
        if (_cloth == null)
            return;

        Vector3 impulse = shotDirection.normalized * 8f;
        _cloth.externalAcceleration = shotDirection.normalized * 6f;
    }

    public void ResetTarget()
    {
        _currentHits = 0;

        if (_cloth != null)
        {
            _cloth.externalAcceleration = Vector3.zero;
            _cloth.randomAcceleration = Vector3.zero;
        }
    }
}