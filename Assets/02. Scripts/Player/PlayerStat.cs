using Photon.Pun;
using System;
using UnityEngine;

[Serializable]
public class PlayerStat : IPunObservable
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

    public float Damage = 20;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 데이터를 전송하는 상황 -> 데이터를 보내주면 되고,
            stream.SendNext(Health);
        }
        else if (stream.IsReading)
        {
            // 데이터를 수신하는 상황 -> 받은 데이터를 세팅하면 됩니다.
            // 보내준 순서대로 받을 수 있다>!!!!
            Health = (float)stream.ReceiveNext();
        }
    }
}
