using UnityEngine;

public abstract class PlayerPowerup : MonoBehaviour, IPlayerPowerup
{
    public bool IsEnabled;
    public Player Player => GetComponentInParent<Player>();
}