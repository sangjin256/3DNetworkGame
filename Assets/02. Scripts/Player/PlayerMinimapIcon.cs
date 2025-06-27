using UnityEngine;

public class PlayerMinimapIcon : PlayerAbility
{
    [SerializeField] private GameObject PlayerIcon;
    [SerializeField] private GameObject EnemyIcon;

    private void Start()
    {
        if (_owner.PhotonView.IsMine) PlayerIcon.SetActive(true);
        else EnemyIcon.SetActive(true);
    }
}
