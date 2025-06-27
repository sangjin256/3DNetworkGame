using DG.Tweening;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerCameraManager : BehaviourSingleton<PlayerCameraManager>
{
    [SerializeField] private float _sprintFOV = 90f;
    [SerializeField] private float _fovTransitionDuration = 0.5f;
    private float _originFOV = 60f;

    CinemachineCamera _camera;
    private Tween _fovTween;

    private void Start()
    {
        _camera = GetComponent<CinemachineCamera>();
        _originFOV = _camera.Lens.FieldOfView;
    }

    public void ChangeToSprintFOV()
    {
        _fovTween?.Kill();
        _fovTween = DOTween.To(() => _camera.Lens.FieldOfView, value => _camera.Lens.FieldOfView = value, _sprintFOV, _fovTransitionDuration);
    }

    public void ChangeToOriginFOV()
    {
        _fovTween?.Kill();
        _fovTween = DOTween.To(() => _camera.Lens.FieldOfView, value => _camera.Lens.FieldOfView = value, _originFOV, _fovTransitionDuration);
    }
}
