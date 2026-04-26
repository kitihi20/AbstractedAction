using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    List<DamageHistory> histories;

    struct DamageHistory
    {
        float damage;
        float cooltime;
    }

    void Start()
    {
        histories = new List<DamageHistory>(32);
    }

    public int Damage(int damage, int coolTime)
    {
        return 0;
    }
}
