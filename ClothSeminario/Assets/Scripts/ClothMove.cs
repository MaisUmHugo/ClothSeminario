using UnityEngine;

public class ClothMove : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _despawnX = 15f;

    [Header("Shot Fly")]
    [SerializeField] private float _shotFlyDuration = 1.2f;

    private int _direction = 1;

    private bool _wasShot;
    private float _shotTimer;
    private Vector3 _shotVelocity;

    public void SetDirection(bool goRight)
    {
        _direction = goRight ? 1 : -1;
    }

    public void LaunchFromShot(Vector3 velocity)
    {
        _wasShot = true;
        _shotTimer = _shotFlyDuration;
        _shotVelocity = velocity;
    }

    public void ResetMoveState()
    {
        _wasShot = false;
        _shotTimer = 0f;
        _shotVelocity = Vector3.zero;
    }

    private void Update()
    {
        if (_wasShot)
        {
            transform.position += _shotVelocity * Time.deltaTime;
            transform.Rotate(180f * Time.deltaTime, 120f * Time.deltaTime, 0f);

            _shotTimer -= Time.deltaTime;

            if (_shotTimer <= 0f)
            {
                PoolManager.Instance.ReturnObject(gameObject);
            }

            return;
        }

        transform.Translate(Vector3.right * _direction * _speed * Time.deltaTime, Space.World);

        if (Mathf.Abs(transform.position.x) >= _despawnX)
        {
            PoolManager.Instance.ReturnObject(gameObject);
        }
    }
}