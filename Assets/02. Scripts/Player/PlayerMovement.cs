using Photon.Pun;
using System.IO;
using UnityEngine;

public class PlayerMovement : PlayerAbility, IPunObservable
{
    private const float GRAVITY = -9.8f;
    private Vector3 _direction;
    [SerializeField] private float _yVelocity;
    private bool _isSprint;
    private float _currentMoveSpeed;

    private Vector3 _receivedPosition = Vector3.zero;
    private Quaternion _receivedRotataion = Quaternion.identity;




    private void Start()
    {
        _photonView.ObservedComponents.Add(this);

        _currentMoveSpeed = _owner.Stat.MoveSpeed;
    }

    // 데이터 동기화를 위한 데이터 전송 및 수신 기능
    // stream : 서버에서 주고받을 데이터가 담겨있는 변수
    // info는 : 송수신 성공/실패 여부에 대한 로그
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 데이터를 전송하는 상황 -> 데이터를 보내주면 되고,
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else if (stream.IsReading)
        {
            // 데이터를 수신하는 상황 -> 받은 데이터를 세팅하면 됩니다.
            // 보내준 순서대로 받을 수 있다>!!!!
            _receivedPosition = (Vector3)stream.ReceiveNext();
            _receivedRotataion = (Quaternion)stream.ReceiveNext();
        }
    }

    private void Update()
    {
        if (_photonView.IsMine == false)
        {
            transform.position = Vector3.Lerp(transform.position, _receivedPosition, Time.deltaTime * 20f);
            transform.rotation = Quaternion.Lerp(transform.rotation, _receivedRotataion, Time.deltaTime * 20f);
            return;
        }

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        _owner.Animator.SetFloat("Vertical", vertical);
        _owner.Animator.SetFloat("Horizontal", horizontal);

        _direction = transform.TransformDirection(new Vector3(horizontal, 0, vertical));
        _direction.y = -0.01f;

        Jump();
        Sprint();

        _direction.y = _yVelocity;
        _owner.Controller.Move(_direction * _currentMoveSpeed * Time.deltaTime);
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _owner.Controller.isGrounded)
        {
            _yVelocity = _owner.Stat.JumpPower;
            _owner.Animator.SetTrigger("JumpStart");
        }

        if (_owner.Controller.isGrounded == false)
        {
            _yVelocity += GRAVITY * Time.deltaTime;
        }
        else
        {
            if (_yVelocity != -0.01f) _owner.Animator.SetTrigger("JumpEnd");
        }
    }

    public void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _isSprint = true;

            if (_owner.GetAbility<PlayerStatus>().CanUseStamina == false)
            {
                _isSprint = false;
            }

            PlayerCameraManager.Instance.ChangeToSprintFOV();
        }

        if (_isSprint)
        {
            if (_owner.GetAbility<PlayerStatus>().CanUseStamina == false)
            {
                _currentMoveSpeed = _owner.Stat.MoveSpeed;
                _isSprint = false;
                PlayerCameraManager.Instance.ChangeToOriginFOV();
            }
            else
            {
                _currentMoveSpeed = _owner.Stat.SprintSpeed;
                _owner.GetAbility<PlayerStatus>().UseStaminaOnFrame(StaminaType.Sprint);
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _currentMoveSpeed = _owner.Stat.MoveSpeed;
            _isSprint = false;
            PlayerCameraManager.Instance.ChangeToOriginFOV();
        }

    }
}
