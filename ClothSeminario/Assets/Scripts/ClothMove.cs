using UnityEngine;

public class ClothMove : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _despawnX = 15f;

    private int _direction = 1;

    public void SetDirection(bool goRight)
    {
        _direction = goRight ? 1 : -1;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * _direction * _speed * Time.deltaTime, Space.World);

        if (Mathf.Abs(transform.position.x) >= _despawnX)
        {
            PoolManager.Instance.ReturnObject(gameObject);
        }
    }
}