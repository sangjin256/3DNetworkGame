using JetBrains.Annotations;
using Microlight.MicroBar;
using Photon.Pun;
using UnityEngine;

public class UI_PlayerStatus : BehaviourSingleton<UI_PlayerStatus>
{
    [SerializeField] private MicroBar HealthBar;
    [SerializeField] private MicroBar StaminaBar;

    private Player _player;

    public void Init(Player player)
    {
        _player = player;
        HealthBar.Initialize(player.Stat.MaxHealth);
        StaminaBar.Initialize(player.Stat.MaxStamina);
        _player.Events.OnHealthChanged += UpdateHealthBar;
        _player.Events.OnStaminaChanged += UpdateStaminaBar;
    }

    public void UpdateHealthBar()
    {
        HealthBar.UpdateBar(_player.Stat.Health, false, UpdateAnim.Damage);
    }

    public void UpdateStaminaBar()
    {
        StaminaBar.UpdateBar(_player.Stat.Stamina, false, UpdateAnim.Damage);
    }
}
