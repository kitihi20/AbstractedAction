using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager Instance { get; private set; }

    [SerializeField] int maxCommonEnemyCount = 128;
    [SerializeField] int maxBossEnemyCount = 4;

    [SerializeField] Enemy[] firstEnemies;

    N2M4_EnemyList commonEnemys;
    //N2M4_EnemyList bossEnemys;// 今のところは分ける必要が無い


    float dtime;

    void Awake()
    {
        Instance = this;

        commonEnemys = new N2M4_EnemyList(maxCommonEnemyCount);
        //bossEnemys = new N2M4_EnemyList(maxBossEnemyCount);
    }

    void Start()
    {
        for(int i = 0; i < firstEnemies.Length; ++i)
        {
            if(!firstEnemies[i]){ continue; }
            firstEnemies[i].E_A_Start();
            commonEnemys.Add(firstEnemies[i]);
        }
    }

    void Update()
    {
        dtime = Time.deltaTime;

        for (int i = commonEnemys.Index; i >= 0; i--)
        {
            if (commonEnemys[i].IsDead())
            {
                Enemy e = commonEnemys.Remove(i);
                //e.E_A_Death();// <- 現在Healthから実行
                continue;
            }

            commonEnemys[i].E_A_Update(dtime);
        }
        /*for(int i = bossEnemys.Index; i >= 0; i--)
        {

        }*/
    }

    public Enemy InstantiateEnemy(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if(commonEnemys.GetRemainingSpaces() <= 0)
        {
            return null;
        }

        GameObject obj = Instantiate(prefab,position,rotation,transform);
        Enemy e = obj.GetComponent<Enemy>();
        e.E_A_Start();

        commonEnemys.Add(e);
        
        return e;
    }

    public Enemy GetNearestAngleEnemy(Vector3 pos, Vector3 vec, float maxdistance, LayerMask levellayer, bool excludeBackward = true)
    {
        Enemy nearestenemy = null;
        float nearesttmp = excludeBackward ? 0 : -2;//-1: 360  0: 180  1: 0
        for(int i = commonEnemys.Index; i >= 0; i--)
        {
            if (commonEnemys[i].IsDead()) { continue; }
            //if (!commonEnemys[i].IsVisible()) { continue; }

            Vector3 enemypos = commonEnemys[i].GetPosition();
            Vector3 enemydiff = enemypos - pos; 
            float enemydist = enemydiff.magnitude;
            if (enemydist > maxdistance) { continue; }

            Vector3 enemyvec = enemydiff / enemydist;
            float dot = Vector3.Dot(vec, enemyvec);
            if (dot < nearesttmp) { continue; }
            


            bool rayres = Physics.Linecast(pos, enemypos, levellayer);//
            if (rayres) { continue; }
            
            nearesttmp = dot;
            nearestenemy = commonEnemys[i];
        }

        return nearestenemy;
    }
}
