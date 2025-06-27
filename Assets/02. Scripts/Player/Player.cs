using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public PlayerStat Stat;
    public PlayerEvent Events;
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }

    private Dictionary<Type, PlayerAbility> _componentDic;

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();

        _componentDic = new Dictionary<Type, PlayerAbility>();
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
