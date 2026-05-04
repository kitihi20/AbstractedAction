using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance { get; private set; }

    [SerializeField] float dodge_movetime = 0.4f;
    [SerializeField] float attack_movetime = 0.2f;

    [SerializeField] LayerMask enemyLayer;

    [SerializeField] Enemy firstEnemy;
    [SerializeField] Transform lookAt2D;

    [SerializeField] PlayerInput input;

    [SerializeField] PlayerCamera cam;
    [SerializeField] PlayerMover mover;
    [SerializeField] PlayerAnimator animator;
    [SerializeField] PlayerGameUI gameUI;
    [SerializeField] Health health;

    RaycastHit hit;
    Enemy enemy;
    Transform enemytra;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SetEnemy(firstEnemy);

        gameUI.SetPlayerHealth(health);

        cam.SetFollower(mover.transform);
    }

    void Update()
    {
        lookAt2D.position = mover.transform.position;
        if(enemytra)
        {
            lookAt2D.LookAt(enemytra);
        }

        Dodge();
        Attack();
    }

    void Dodge()
    {
        if(input.dodge_down)
        {
            float rand = (Random.Range(0, 2) == 0) ? 1 : -1;
            Vector3 dodgePos = mover.transform.position + lookAt2D.right * 5 * rand;

            mover.Move(dodgePos, dodge_movetime);
            animator.Animate_Dodge();
            health.SetInvincibleTime(dodge_movetime);
        }
    }

    void Attack()
    {
        if(input.attack_down)
        {
            if(enemytra)
            {
                Vector3 AttackPos = enemytra.position + lookAt2D.forward * -3;
                float sqrDist = mover.GetTargetSQRDist(AttackPos);
                if(sqrDist > 0.1f)
                {
                    mover.Move(AttackPos, attack_movetime);
                    health.SetInvincibleTime(attack_movetime);

                    animator.Animate_Dodge();
                    animator.Animate_MoveAttack();
                }else
                {
                    //Attack
                    animator.Animate_Attack();
                }
            }
        }
    }

    void SetEnemy(Enemy e)
    {
        enemy = e;
        enemytra = e.GetTransform();

        cam.LookAt(enemytra);
        mover.LookAt(enemytra);

        gameUI.SetEnemy(e);
    }

    public Vector3 GetPosition()
    {
        return lookAt2D.position;
    }
}
