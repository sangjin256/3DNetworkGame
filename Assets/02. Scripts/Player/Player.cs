using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStat Stat;

    public PlayerRotation PlayerRotation;
    public PlayerAttack PlayerAttack;
    public PlayerMovement PlayerMovement;

    private void Awake()
    {
        PlayerRotation = GetComponent<PlayerRotation>();
        PlayerAttack = GetComponent<PlayerAttack>();
        PlayerMovement = GetComponent<PlayerMovement>();
    }
}
