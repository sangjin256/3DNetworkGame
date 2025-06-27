using UnityEngine;

public class UIManager : BehaviourSingleton<UIManager>
{
    [SerializeField] private UI_PlayerStatus _playerStatus;
    [SerializeField] private MinimapCamera _minimapCamera;

   public void Init(Player player)
    {
        _playerStatus.Init(player);
        _minimapCamera.Init(player.transform);
    }
}
