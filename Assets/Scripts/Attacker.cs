using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] float coolTime = 0;
    [SerializeField] TargetType targetType;
    [SerializeField] AttackType attackType;
    [SerializeField] ParticleSystem hitParticle;

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

    int id;

    void Awake()
    {
        id = Random.Range(0, 999999999);
    }

    public void Hit(Vector3 pos)
    {
        if(hitParticle)
        {
            hitParticle.transform.position = pos;
            hitParticle.Play();
        }
    }

    public Vector3 GetPosition() { return transform.position; }
    public int GetID() { return id; }


    public int GetDamage() { return damage; }
    public float GetCoolTime() { return coolTime; }
    public TargetType GetTarget() { return targetType; }
    public AttackType GetAttackType() { return attackType; }
}
