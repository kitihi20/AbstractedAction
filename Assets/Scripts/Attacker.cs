using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] float coolTime = 0;
    [SerializeField] TargetType targetType;
    [SerializeField] AttackType attackType;

    public enum TargetType
    {
        all,
        player,
        enemy,
    }

    public enum AttackType
    {
        direct,
        blast
    }

    public Vector3 GetPosition() { return transform.position; }
    
    public int GetDamage() { return damage; }
    public float GetCoolTime() { return coolTime; }
    public TargetType GetTarget() { return targetType; }
    public AttackType GetAttackType() { return attackType; }
}
