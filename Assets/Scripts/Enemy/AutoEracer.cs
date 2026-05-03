using UnityEngine;

public class AutoEracer : MonoBehaviour
{
    [SerializeField] float eraceTime = 1;
    [SerializeField] GameObject eracedPrefab;

    float nowtime;
    
    void Start()
    {
        nowtime = 0;
    }

    void Update()
    {
        nowtime += Time.deltaTime;
        if(nowtime >= eraceTime)
        {
            enabled = false;
            gameObject.SetActive(false);

            if(eracedPrefab)
            {
                Instantiate(eracedPrefab, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}
