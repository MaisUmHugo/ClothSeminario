using UnityEngine;

public class MoveLine : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private bool _startGoingRight = true;
    [SerializeField] private float _leftLimit = -8f;
    [SerializeField] private float _rightLimit = 8f;

    private int direction;

    private void Start()
    {
        direction = _startGoingRight ? 1 : -1;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * direction * _speed * Time.deltaTime, Space.World);

        if (transform.position.x >= _rightLimit)
            direction = -1;
        else if (transform.position.x <= _leftLimit)
            direction = 1;
    }
}