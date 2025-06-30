using Photon.Pun;
using UnityEngine;

public class PlayerAttack : PlayerAbility
{
    private float _elapsedtime;

    public Collider WeaponCollider;

    private void Start()
    {
        _elapsedtime = 0f;
        DeActiveCollider();
    }

    // - '위치/회전'처럼 상시로 확인이 필요한 데이터 동기화: IPunObservable(OnPhotonSerializeView)
    // - '트리거/공격/피격' 처럼 간헐적으로 특정한 이벤트가 발생했을때의 변화된 데이터 동기화: RPC
    //   RPC : Remote Procedure Call
    //          ㄴ 물리적으로 떨어져 있는 다른 디바이스의 함수를 호출하는 기능
    //          ㄴ RPC 함수를 호출하면 네트워크를 통해 다른 사용자의 스크립트에서 해당 함수가 호출된다.
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
                    // RPC 메서드 호출 방식
                    _owner.PhotonView.RPC(nameof(RandomAttack), RpcTarget.All);
                    _owner.GetAbility<PlayerStatus>().UseStamina(StaminaType.BasicAttack);
                    _elapsedtime = 0f;
                }
            }
        }
    }

    public void ActiveCollider()
    {
        WeaponCollider.enabled = true;
    }

    public void DeActiveCollider()
    {
        WeaponCollider.enabled = false;
    }

    public void Hit(Collider other)
    {
        // 내 캐릭터가 아니면 남을 때리면 안됨
        if (_owner.PhotonView.IsMine == false)
        {
            return;
        }

        DeActiveCollider();

        // RPC로 호출해야지 다른 사람의 게임 오브젝트들도 이 함수가 실행된다. 
        //damagedObject.TakeDamage(damage);

        PhotonView otherPhotonView = other.GetComponent<PhotonView>();
        otherPhotonView.RPC(nameof(Player.TakeDamage), RpcTarget.All, _owner.Stat.Damage);
    }

    // RPC로 호출할 함수는 반드시 [PunRPC] 어트리뷰트를 함수 앞에 명시해줘야 한다.
    [PunRPC]
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