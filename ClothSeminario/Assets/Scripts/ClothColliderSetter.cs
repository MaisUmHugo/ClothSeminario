using UnityEngine;

public class ClothColliderSetter : MonoBehaviour
{
    [SerializeField] private Cloth _cloth;

    private void Awake()
    {
        if (_cloth == null)
            _cloth = GetComponentInChildren<Cloth>();
    }

    public void SetCapsuleColliders(CapsuleCollider[] colliders)
    {
        if (_cloth == null)
            return;

        _cloth.capsuleColliders = colliders;
    }
}