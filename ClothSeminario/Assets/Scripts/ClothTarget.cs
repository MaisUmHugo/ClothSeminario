using UnityEngine;

public class ClothTarget : MonoBehaviour
{
    [SerializeField] private int _points = 10;
    [SerializeField] private int _hitsToDisable = 1;
    [SerializeField] private Cloth _cloth;
    [SerializeField] private ClothMove _clothMove;

    [Header("Shot Launch")]
    [SerializeField] private float _launchForwardForce = 8f;
    [SerializeField] private float _launchUpForce = 2f;
    [SerializeField] private float _launchSideForce = 1.5f;

    private int _currentHits;
    private bool _wasLaunched;

    private void Awake()
    {
        if (_cloth == null)
            _cloth = GetComponentInChildren<Cloth>();

        if (_clothMove == null)
            _clothMove = GetComponent<ClothMove>();
    }

    public void Hit(Vector3 hitPoint, Vector3 shotDirection)
    {
        if (_wasLaunched)
            return;

        _currentHits++;

        ScoreManager.Instance?.AddScore(_points);

        ReactToShot(shotDirection);

        if (_currentHits >= _hitsToDisable)
        {
            LaunchCloth(shotDirection);
        }
    }

    private void ReactToShot(Vector3 shotDirection)
    {
        if (_cloth == null)
            return;

        _cloth.externalAcceleration = shotDirection.normalized * 6f;
    }

    private void LaunchCloth(Vector3 shotDirection)
    {
        _wasLaunched = true;

        if (_clothMove == null)
        {
            PoolManager.Instance.ReturnObject(gameObject);
            return;
        }

        Vector3 side = shotDirection.normalized * _launchSideForce;
        Vector3 up = Vector3.up * _launchUpForce;
        Vector3 forward = Vector3.forward * _launchForwardForce; // fundo = Z positivo

        Vector3 finalVelocity = side + up + forward;

        _clothMove.LaunchFromShot(finalVelocity);
    }

    public void ResetTarget()
    {
        _currentHits = 0;
        _wasLaunched = false;

        if (_cloth != null)
        {
            _cloth.externalAcceleration = Vector3.zero;
            _cloth.randomAcceleration = Vector3.zero;
        }
    }
}