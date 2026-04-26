using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] HealthType healthType;
    [SerializeField] int health = 100;

    public enum HealthType
    {
        player,
        enemy,
    }

    float dtime;

    int health_now;

    Dictionary<int,DamageHistory> histories;

    class DamageHistory
    {
        public DamageHistory(int damage, float coolTime)
        {
            this.damage = damage;
            this.cooltime = coolTime;
        }

        public int damage;
        public float cooltime;
    }

    void Awake()
    {
        histories = new Dictionary<int,DamageHistory>(32);
        health_now = health;
    }

    void Update()
    {
        dtime = Time.deltaTime;

        foreach(int k in histories.Keys)
        {
            histories[k].cooltime -= dtime;
            if(histories[k].cooltime <= 0)
            {
                histories.Remove(k);
                continue;
            }
        }
    }

    public bool ExaminingAttackerTypes(Attacker a)
    {
        switch(healthType)
        {
            case HealthType.player:
                if(a.GetTarget() != Attacker.TargetType.enemy)
                {
                    return true;
                }
            break;
            case HealthType.enemy:
                if(a.GetTarget() != Attacker.TargetType.player)
                {
                    return true;
                }
            break;
        }
        return false;
    }

    public void Damage(int id, int damage, float coolTime)
    {
        if(histories.ContainsKey(id))
        {
            return;
        }
        histories.Add(id, new DamageHistory(damage, coolTime));
        health_now -= damage;

        if(health_now <= 0)
        {
            health_now = 0;
            Death();
        }
        return;
    }

    public void Death()
    {
        
    }


    void OnTriggerEnter(Collider other)
    {
        Attacker a = other.GetComponent<Attacker>();
        if(a)
        {
            if(ExaminingAttackerTypes(a))
            {
                Damage(a.GetID(), a.GetDamage(), a.GetCoolTime());
            }
        }
    }

    void OnParticleCollision(GameObject other)
    {
        Attacker a = other.GetComponent<Attacker>();
        if(a)
        {
            if(ExaminingAttackerTypes(a))
            {
                Damage(a.GetID(), a.GetDamage(), a.GetCoolTime());
            }
        }
    }
}
