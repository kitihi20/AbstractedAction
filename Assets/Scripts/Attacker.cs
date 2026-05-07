using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] float coolTime = 0;
    [SerializeField] bool enableToOtherAttackID = false;
    [SerializeField] TargetType targetType;
    [SerializeField] AttackType attackType;
    [SerializeField] ParticleSystem hitParticle;

    public enum TargetType
    {
        all,
        player,
        enemy,
        none,//攻撃の予測段階での回避に利用
    }

    public enum AttackType
    {
        direct,
        blast//爆発系に利用する想定、遮蔽で無効化、現在未実装
    }

    int id;

    void Awake()
    {
        id = Random.Range(int.MinValue, int.MaxValue);
    }

    void OnEnable()
    {
        if(enableToOtherAttackID)
        {
            id = Random.Range(int.MinValue, int.MaxValue);
        }
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
