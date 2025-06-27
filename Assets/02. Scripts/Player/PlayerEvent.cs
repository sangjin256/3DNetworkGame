using System;
using UnityEngine;

[Serializable]
public class PlayerEvent
{
    public Action OnHealthChanged;
    public Action OnStaminaChanged;
}
