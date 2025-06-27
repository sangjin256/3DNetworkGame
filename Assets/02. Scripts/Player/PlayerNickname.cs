using TMPro;
using UnityEngine;

public class PlayerNickname : PlayerAbility
{
    public TextMeshProUGUI NicknameTextUI;

    private void Start()
    {
        NicknameTextUI.text = $"{_owner.PhotonView.Owner.NickName}_{_owner.PhotonView.Owner.ActorNumber}";

        if (_owner.PhotonView.IsMine)
        {
            NicknameTextUI.color = Color.green;
        }
        else
        {
            NicknameTextUI.color = Color.red;
        }
    }
}
