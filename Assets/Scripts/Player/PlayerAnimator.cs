using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;

    int ComboNum;
    float ComboTime;

    int hash_Move;
    int hash_Dodge;
    int hash_Attack1;

    void Awake()
    {
        ComboNum = 0;
        ComboTime = 0;

        hash_Move = Animator.StringToHash("Move");
        hash_Dodge = Animator.StringToHash("Dodge");
        hash_Attack1 = Animator.StringToHash("Attack1");
    }

    void Update()
    {
        
    }

    public void Animate_Move()
    {
        
    }

    public void Animate_Dodge()
    {
        
    }

    public void Animate_Attack()
    {
        
    }
}
