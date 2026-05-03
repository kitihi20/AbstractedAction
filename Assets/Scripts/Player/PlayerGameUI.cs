using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerGameUI : MonoBehaviour
{
    [Header("EnemyHealth")]
    [SerializeField] Image enemyHealth_gauge;
    [SerializeField] TextMeshProUGUI enemyHealth_Name;

    Enemy targetEnemy;

    void Start()
    {
        
    }

    void Update()
    {
        Update_EnemyHealth();
    }

    void Update_EnemyHealth()
    {
        if(!targetEnemy){ return; }
        enemyHealth_gauge.fillAmount = (float)targetEnemy.GetNowHealth() / targetEnemy.GetMaxHealth();
    }

    public void SetEnemy(Enemy e)
    {
        targetEnemy = e;
        enemyHealth_gauge.fillAmount = (float)e.GetNowHealth() / e.GetMaxHealth();
        enemyHealth_Name.text = e.GetName();
    }
}
