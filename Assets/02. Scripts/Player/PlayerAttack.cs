using UnityEngine;

public class PlayerAttack : PlayerAbility
{
    private float _elapsedtime;

    private void Start()
    {
        _elapsedtime = 0f;
    }

    private void Update()
    {
        if (_owner.PhotonView.IsMine == false) return;

        _elapsedtime += Time.deltaTime;
        if (_elapsedtime >= 1f / _owner.Stat.AttackSpeed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_owner.GetAbility<PlayerStatus>().CanUseStamina)
                {
                    RandomAttack();
                    _owner.GetAbility<PlayerStatus>().UseStamina(StaminaType.BasicAttack);
                    _elapsedtime = 0f;
                }
            }
        }
    }

    private void RandomAttack()
    {
        int index = Random.Range(0, (int)AttackType.Count);
        string attack = GetAttackTypeString((AttackType)index);

        if (string.IsNullOrEmpty(attack)) return;

        _owner.Animator.SetTrigger(attack);
    }


    private string GetAttackTypeString(AttackType type)
    {
        switch (type)
        {
            case AttackType.Attack1:
                return "Attack1";
            case AttackType.Attack2:
                return "Attack2";
            case AttackType.Attack3:
                return "Attack3";
        }
        return string.Empty;
    }
}