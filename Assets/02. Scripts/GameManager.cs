using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : BehaviourSingleton<GameManager>
{
    [SerializeField] private List<Transform> _playerSpawnpointList;

    public Vector3 GetPlayerSpawnPoint()
    {
        return _playerSpawnpointList[Random.Range(0, _playerSpawnpointList.Count)].position;
    }
}
