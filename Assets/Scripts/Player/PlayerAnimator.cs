using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;

    float attackAcceptableTime = 0.2f;

    int ComboNum;
    float ComboTime;

    float time;
    float moveattack_time;

    int hash_Dodge;
    int hash_Attack;
    int hash_MoveAttack;

    void Awake()
    {
        ComboNum = 0;
        ComboTime = 0;

        hash_Dodge = Animator.StringToHash("Dodge");
        hash_Attack = Animator.StringToHash("Attack");
        hash_MoveAttack = Animator.StringToHash("MoveAttack");
    }

    void Update()
    {
        time = Time.timeSinceLevelLoad;

        animator.SetBool(hash_MoveAttack, moveattack_time+attackAcceptableTime >= time);
        /*if(attack_time+attackAcceptableTime <= time)
        {
            
        }*/
    }

    public void Animate_Dodge()
    {
        animator.SetTrigger(hash_Dodge);
    }

    public void Animate_MoveAttack()
    {
        moveattack_time = Time.timeSinceLevelLoad;
    }

    public void Animate_Attack()
    {
        animator.SetTrigger(hash_Attack);
    }
}
