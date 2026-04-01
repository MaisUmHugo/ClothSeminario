using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _maxDistance = 100f;
    [SerializeField] private LayerMask _hitMask;
    [SerializeField] private AudioClip _damageSoundClip;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (_camera == null)
            _camera = Camera.main;

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance, _hitMask))
        {
            ClothTarget target = hit.collider.GetComponentInParent<ClothTarget>();

            if (target != null)
            {
                SOundEffectsManager.instance.PlaySoundEffect(_damageSoundClip, transform, 1f);
                target.Hit(hit.point, ray.direction);

            }
        }
    }
}