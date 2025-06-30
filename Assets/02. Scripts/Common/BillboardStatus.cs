using Microlight.MicroBar;
using UnityEngine;

public class BillboardStatus : MonoBehaviour
{
    private Player _player;
    [SerializeField] private MicroBar _healthBar;

    private void Start()
    {
        _player = GetComponentInParent<Player>();
        if (_player.PhotonView.IsMine)
        {
            _healthBar.gameObject.SetActive(false);
            return;
        }

        _healthBar.Initialize(_player.Stat.MaxHealth);
        _healthBar.UpdateBar(_player.Stat.Health);
        _player.Events.OnHealthChanged += UpdateBar;
    }

    private void UpdateBar()
    {
        _healthBar.UpdateBar(_player.Stat.Health);
    }
}
