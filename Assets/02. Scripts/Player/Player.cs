using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using static UnityEngine.UI.GridLayoutGroup;

public class Player : MonoBehaviour, IDamageable
{
    public PlayerStat Stat;
    public PlayerEvent Events;
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    public PhotonView PhotonView { get; private set; }

    private Dictionary<Type, PlayerAbility> _componentDic;

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
        PhotonView = GetComponent<PhotonView>();

        _componentDic = new Dictionary<Type, PlayerAbility>();
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        Stat.Health = Mathf.Max(0, Stat.Health - damage);
        Events.OnHealthChanged?.Invoke();
    }

    public T GetAbility<T>() where T : PlayerAbility
    {
        if(_componentDic.TryGetValue(typeof(T), out PlayerAbility value))
        {
            return value as T;
        }

        value = GetComponent<T>();

        if(value != null)
        {
            _componentDic[value.GetType()] = value;
            return value as T;
        }

        throw new Exception($"어빌리티 {typeof(T)}를 {gameObject.name}에서 찾을 수 없습니다.");
    }
}
