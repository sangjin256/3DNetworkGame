using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public abstract class PlayerAbility : MonoBehaviour
{
    protected Player _owner { get; private set; }

    protected virtual void Awake()
    {
        _owner = GetComponent<Player>();
    }
}
