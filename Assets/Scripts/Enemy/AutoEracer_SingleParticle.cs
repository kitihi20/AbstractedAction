using UnityEngine;

public class AutoEracer_SingleParticle : MonoBehaviour
{
    [SerializeField] float chargeTime = 1;
    [SerializeField] float attackTime = 1;
    [SerializeField] float endingTime = 1;

    [SerializeField] GameObject dodgeObj;
    [SerializeField] GameObject attackerObj;

    int state;

    float nowtime;
    
    void Start()
    {
        nowtime = 0;
        state = 0;
        
        attackerObj.SetActive(false);
    }

    void Update()
    {
        nowtime += Time.deltaTime;

        switch (state)
        {
            case 0:
                if(nowtime >= chargeTime)
                {
                    nowtime = 0;
                    state = 1;
                    attackerObj.SetActive(true);
                }
            break;
            case 1:
                if(nowtime >= attackTime)
                {
                    nowtime = 0;
                    state = 2;
                    dodgeObj.SetActive(false);
                    attackerObj.SetActive(false);
                }
            break;
            case 2:
                if(nowtime >= endingTime)
                {
                    nowtime = 0;
                    state = 99;
                }
            break;
            default:
                enabled = false;
                gameObject.SetActive(false);
                Destroy(gameObject);
            break;
        }
    }
}