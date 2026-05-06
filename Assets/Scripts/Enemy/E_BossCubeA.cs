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

    [SerializeField] Rigidbody[] deathObjs;

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
            int rand = Random.Range(0,2);
            switch(rand)
            {
                case 1:
                    Attack_MLazer();
                    attacktime = 4f;
                break;
                default:
                    Attack_Takenoko_1();
                    attacktime = 1.5f;
                break;
            }
        }
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

    void Attack_Takenoko_1()
    {
        Instantiate(takenoko1Prefab, playerPos, Quaternion.Euler(new Vector3(-90, 0, 0)));
    }

    void Attack_MLazer()
    {
        Vector3 shotvec = (playerPos - GetPosition()).normalized;
        Vector3 shotpos = GetPosition() + new Vector3(0, 2.5f, 0) + shotvec * 2f;
        Instantiate(mLazerPrefab, shotpos, Quaternion.LookRotation(shotvec));
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
