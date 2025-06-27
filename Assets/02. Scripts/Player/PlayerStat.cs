using System;
using UnityEngine;

[Serializable]
public class PlayerStat
{
    public float MaxHealth  = 100;
    public float Health     = 100;
    public float MaxStamina = 100;
    public float Stamina    = 100;

    public float AttackSpeed    = 1.2f;    // 초당 1.2번 공격할 수 있다.
    public float RotationSpeed  = 200f;
    public float MoveSpeed      = 10f;
    public float SprintSpeed    = 20f;
    public float JumpPower      = 3f;
}
