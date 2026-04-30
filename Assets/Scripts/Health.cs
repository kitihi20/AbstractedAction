using System;
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

    //public delegate void DamageDelegate(int damage);
    public delegate void DeathDelegate();

    public bool isDead { private set; get; }

    int health_now;
    float healthCheckedTime;
    //DamageDelegate damageDelegate;
    DeathDelegate deathDelegate;

    Dictionary<int,DamageHistory> histories;

    class DamageHistory
    {
        public DamageHistory(int damage, float coolTime)
        {
            this.hitTime = Time.timeSinceLevelLoad;
            this.damage = damage;
            this.cooltime = coolTime;
        }


        public float hitTime { get; private set;}
        public int damage { get; private set;}
        public float cooltime { get; private set;}
    }

    void Awake()
    {
        histories = new Dictionary<int,DamageHistory>(32);
        isDead = false;
        health_now = health;
        healthCheckedTime = -1;
    }


    void CheckDamageHistories()
    {
        if(healthCheckedTime >= Time.timeSinceLevelLoad) { return; }
        healthCheckedTime = Time.timeSinceLevelLoad;

        foreach(int k in histories.Keys)
        {
            if(histories[k].hitTime + histories[k].cooltime <= healthCheckedTime)
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

    public void Damage(int id, int damage, float cooltime)
    {
        if(isDead) { return; }

        CheckDamageHistories();

        if(histories.ContainsKey(id)) { return; }

        health_now -= damage;

        if(health_now <= 0)
        {
            health_now = 0;
            Death();
            return;
        }

        //damageDelegate?.Invoke(damage);

        if(cooltime > 0)
        {
            histories.Add(id, new DamageHistory(damage, cooltime));
        }

        return;
    }

    public void Death()
    {
        isDead = true;
        enabled = false;
        deathDelegate?.Invoke();
    }

    /*public void AddDelegate_Damage(DamageDelegate d)
    {
        damageDelegate += d;
    }
    public void RemoveDelegate_Damage(DamageDelegate d)
    {
        damageDelegate -= d;
    }*/

    public void AddDelegate_Death(DeathDelegate d)
    {
        deathDelegate += d;
    }
    public void RemoveDelegate_Death(DeathDelegate d)
    {
        deathDelegate -= d;
    }


    public int GetMaxHealth()
    {
        return health;
    }
    public int GetNowHealth()
    {
        return health_now;
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
