using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;

    int ComboNum;
    float ComboTime;


    int hash_Dodge;
    int hash_Attack1;

    void Awake()
    {
        ComboNum = 0;
        ComboTime = 0;

        hash_Dodge = Animator.StringToHash("Dodge");
        hash_Attack1 = Animator.StringToHash("Attack1");
    }

    void Update()
    {
        
    }

    public void Animate_Dodge()
    {
        animator.SetTrigger(hash_Dodge);
    }

    public void Animate_Attack()
    {
        animator.SetTrigger(hash_Attack1);
    }
}
