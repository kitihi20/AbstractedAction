using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] string enemyName = "enemyName";
    [SerializeField] Health health;


    protected abstract void E_Start();
    protected abstract void E_Update(float dtime);
    protected abstract void E_Death();


    public abstract Vector3 GetPosition();
    public abstract Vector3 GetCenterPosition();
    public abstract Transform GetTransform();

    //
    public void E_A_Start()
    {
        //
        health.AddDelegate_Death(E_A_Death);
        E_Start();
    }
    public void E_A_Update(float dtime)
    {
        //
        E_Update(dtime);
    }
    public void E_A_Death()
    {
        //
        E_Death();
    }


    public string GetName()
    {
        return enemyName;
    }

    //
    public bool IsDead()
    {
        return health.isDead;
    }
    public int GetMaxHealth()
    {
        return health.GetMaxHealth();
    }
    public int GetNowHealth()
    {
        return health.GetNowHealth();
    }

}
