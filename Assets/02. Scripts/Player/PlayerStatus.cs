using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : PlayerAbility, IDamageable
{
    public bool CanUseStamina { get; private set; }
    private Dictionary<StaminaType, float> _staminaDic;

    private void Start()
    {
        _staminaDic = new();
        _staminaDic[StaminaType.Sprint] = 5f;      // 초당 10
        _staminaDic[StaminaType.BasicAttack] = 10f; // 번당 20
        _staminaDic[StaminaType.Jump] = 5f;        // 번당 10

        CanUseStamina = true;
    }

    public void UseStamina(StaminaType type)
    {
        if (CanUseStamina)
        {
            _owner.Stat.Stamina -= _staminaDic[type];

            if (_owner.Stat.Stamina <= 0) CanUseStamina = false;
        }
    }

    public void UseStaminaOnFrame(StaminaType type)
    {
        if (CanUseStamina)
        {
            _owner.Stat.Stamina -= _staminaDic[type] * Time.deltaTime;

            if (_owner.Stat.Stamina <= 0) CanUseStamina = false;
        }
    }

    public void TakeDamage(Damage damage)
    {
        _owner.Stat.Health -= damage.Value;
    }
}
