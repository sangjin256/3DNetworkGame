using UnityEngine;

public class Weapon : MonoBehaviour
{
    private PlayerAttack _playerAttack;

    private void Start()
    {
        _playerAttack = GetComponentInParent<PlayerAttack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == _playerAttack.transform)
        {
            return;
        }

        if(other.GetComponent<IDamageable>() != null)
        {
            _playerAttack.Hit(other);
        }
    }
}
