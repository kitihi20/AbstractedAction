using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayer;

    [SerializeField] Transform firstEnemy;
    [SerializeField] Transform lookAt2D;

    [SerializeField] PlayerInput input;

    [SerializeField] PlayerCamera cam;
    [SerializeField] PlayerMover mover;

    RaycastHit hit;
    Transform enemytra;

    void Start()
    {
        SetEnemy(firstEnemy);

        cam.SetFollower(mover.transform);
    }

    void Update()
    {
        lookAt2D.position = mover.transform.position;
        if(enemytra)
        {
            lookAt2D.LookAt(enemytra);
        }

        if(input.dodge_down)
        {
            Dodge();
        }
        if(input.attack_down)
        {
            Attack();
        }
    }

    void Dodge()
    {
        float rand = (Random.Range(0, 2) == 0) ? 1 : -1;
        Vector3 dodgePos = mover.transform.position + lookAt2D.right * 5 * rand;
        mover.Move(dodgePos);
    }

    void Attack()
    {
        if(enemytra)
        {
            Vector3 AttackPos = enemytra.position + lookAt2D.forward * -3;
            mover.Move(AttackPos);
        }
    }

    void SetEnemy(Transform tra)
    {
        enemytra = tra;
        cam.LookAt(enemytra);
        mover.LookAt(enemytra);
    }
}
