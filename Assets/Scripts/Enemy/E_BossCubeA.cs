using System.Collections;
using UnityEngine;

//

/*
ギミック案
避けやすい攻撃 > 隙が大きい攻撃
- タケノコ、単独・連続
- レーザー、単独・連続
- ゲロビ
- 
*/

public class E_BossCubeA : Enemy
{
    [SerializeField] Transform enemyTra;
    [SerializeField] Transform enemyCenterTra;
    [SerializeField] int nextSceneIndex = 2;
    [SerializeField] ParticleSystem deathParticle;

    [SerializeField] GameObject takenoko1Prefab;
    [SerializeField] GameObject mLazerPrefab;
    [SerializeField] GameObject tsuraraPrefab;

    [SerializeField] Rigidbody[] deathObjs;

    int attackType;
    int attackCount;
    float attacktime;

    Vector3 playerPos;

    protected override void E_Start()
    {
        attacktime = 3f;
    }

    protected override void E_Update(float dtime)
    {
        attacktime -= dtime;

        playerPos = PlayerController.instance.GetPosition();

        if(attacktime <= 0)
        {
            if(attackCount <= 0)
            {
                NextAttack();
            }
            else
            {
                Attack();
            }
        }
    }


    void NextAttack()
    {
        attackType = Random.Range(0,3);
        switch(attackType)
        {
            case 1:
                attackCount = 1;
            break;
            case 2:
                attackCount = 4;
            break;
            default:
                attackCount = 1;
            break;
        }

        Attack();
    }

    void Attack()
    {
        attackCount--;

        switch(attackType)
        {
            case 1:
                Attack_MLazer();
            break;
            case 2:
                Attack_Tsurara();
            break;
            default:
                Attack_Takenoko_1();
            break;
        }
    }
    void Attack_Takenoko_1()
    {
        Instantiate(takenoko1Prefab, playerPos, Quaternion.Euler(new Vector3(-90, 0, 0)));
        attacktime = 1.5f;
    }

    void Attack_MLazer()
    {
        Vector3 shotvec = (playerPos - GetPosition()).normalized;
        Vector3 shotpos = GetPosition() + new Vector3(0, 2.5f, 0) + shotvec * 2f;
        Instantiate(mLazerPrefab, shotpos, Quaternion.LookRotation(shotvec));
        attacktime = 4f;
    }

    void Attack_Tsurara()
    {
        Instantiate(tsuraraPrefab, playerPos, Quaternion.Euler(new Vector3(-90, 0, 0)));
        attacktime = 0.5f;
    }


    protected override void E_Death()
    {
        for(int i = 0; i < deathObjs.Length; ++i)
        {
            deathObjs[i].isKinematic = false;
        }
        
        StartCoroutine(DeathCoroutine());
    }

    IEnumerator DeathCoroutine()
    {
        deathParticle.Play();

        TimeController.Instance.SetTimeScale(0.1f);

        yield return new WaitForSecondsRealtime(5);

        TimeController.Instance.SetTimeScale(1f);

        yield return new WaitForSecondsRealtime(4);

        SceneLoader.Instance.LoadScene(nextSceneIndex);

        yield break;
    }


    public override Vector3 GetPosition()
    {
        return enemyTra.position;
    }
    public override Vector3 GetCenterPosition()
    {
        return enemyCenterTra.position;
    }

    public override Transform GetTransform()
    {
        return enemyTra;
    }
}
