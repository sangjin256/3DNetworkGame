using UnityEngine;

public class MinimapCamera : BehaviourSingleton<MinimapCamera>
{
    private Transform _target;
    public float YOffset = 15f;

    public void Init(Transform target)
    {
        _target = target;
    }

    private void LateUpdate()
    {
        if (_target == null) return;
        Vector3 newPosition = _target.position;
        newPosition.y += YOffset;
        transform.position = newPosition;

        Vector3 newEulerAngle = _target.eulerAngles;
        newEulerAngle.x = 90;
        newEulerAngle.z = 0;
        transform.eulerAngles = newEulerAngle;
    }
}
