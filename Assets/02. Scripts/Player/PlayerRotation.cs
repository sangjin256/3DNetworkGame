using Unity.Cinemachine;
using UnityEngine;

public class PlayerRotation : PlayerAbility
{
    [SerializeField] private Transform _cameraRoot;

    private float _yRotation = 0f;
    
    // 마우스 입력을 누적할 변수
    private float _mx;
    private float _my;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (_owner.PhotonView.IsMine)
        {
            CinemachineCamera camera = GameObject.FindGameObjectWithTag("FollowCamera").GetComponent<CinemachineCamera>();
            camera.Follow = _cameraRoot;
        }
    }
    private void Update()
    {
        if (_owner.PhotonView.IsMine == false) return;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _mx += mouseX * _owner.Stat.RotationSpeed * Time.deltaTime;
        _my += mouseY * _owner.Stat.RotationSpeed * Time.deltaTime;

        _my = Mathf.Clamp(_my, -90f, 90f);

        // y축 회전은 캐릭터만 한다.
        transform.eulerAngles = new Vector3(0f, _mx, 0f);

        // x축 회전은 캐릭터는 하지 않는다. (즉, 카메라 루트만 x축 회전하면 된다.)
        _cameraRoot.localEulerAngles = new Vector3(-_my, 0f, 0f);
    }
}
