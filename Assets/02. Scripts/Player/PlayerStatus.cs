using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : PlayerAbility
{
    public bool CanUseStamina { get; private set; }
    [SerializeField] private float _recoveryDelay = 0.5f;
    [SerializeField] private float _recoverySpeed = 15f;
    private Dictionary<StaminaType, float> _staminaDic;

    private Coroutine _staminaRecoveryCoroutine;

    private void Start()
    {
        _staminaDic = new();
        _staminaDic[StaminaType.Sprint] = 5f;      // 초당 10
        _staminaDic[StaminaType.BasicAttack] = 10f; // 번당 20
        _staminaDic[StaminaType.Jump] = 5f;        // 번당 10

        CanUseStamina = true;

        if (_owner.PhotonView.IsMine)
        {
            UIManager.Instance.Init(_owner);
        }
    }

    private void Update()
    {
        if (_owner.Controller.enabled == false) return;

        if (transform.position.y < -10f) _owner.TakeDamage(10000000f);
    }

    private void StartStaminaRecovery()
    {
        if(_staminaRecoveryCoroutine != null)
        {
            StopCoroutine(_staminaRecoveryCoroutine);
        }
        _staminaRecoveryCoroutine = StartCoroutine(RecoverStamina());
    }

    private IEnumerator RecoverStamina()
    {
        yield return new WaitForSeconds(_recoveryDelay);

        while(_owner.Stat.Stamina < _owner.Stat.MaxStamina)
        {
            CanUseStamina = true;
            _owner.Stat.Stamina += _recoverySpeed * Time.deltaTime;
            if (_owner.Stat.Stamina > _owner.Stat.MaxStamina) _owner.Stat.Stamina = _owner.Stat.MaxStamina;
            _owner.Events.OnStaminaChanged?.Invoke();
            yield return null;
        }

        _staminaRecoveryCoroutine = null;
    }

    public void UseStamina(StaminaType type)
    {
        if (CanUseStamina)
        {
            _owner.Stat.Stamina -= _staminaDic[type];
            _owner.Events.OnStaminaChanged?.Invoke();

            if (_owner.Stat.Stamina <= 0)
            {
                _owner.Stat.Stamina = 0f;
                CanUseStamina = false;
            }

            StartStaminaRecovery();
        }
    }

    public void UseStaminaOnFrame(StaminaType type)
    {
        if (CanUseStamina)
        {
            _owner.Stat.Stamina -= _staminaDic[type] * Time.deltaTime;
            _owner.Events.OnStaminaChanged?.Invoke();

            if (_owner.Stat.Stamina <= 0)
            {
                _owner.Stat.Stamina = 0f;
                CanUseStamina = false;
            }

            StartStaminaRecovery();
        }
    }
}
