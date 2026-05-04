using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerGameUI : MonoBehaviour
{
    [Header("PlayerHealth")]
    [SerializeField] Image playerHealth_gauge;

    [Header("EnemyHealth")]
    [SerializeField] Image enemyHealth_gauge;
    [SerializeField] TextMeshProUGUI enemyHealth_Name;

    Health playerHealth;
    Enemy targetEnemy;

    void Start()
    {
        
    }

    void Update()
    {
        Update_PlayerHealth();
        Update_EnemyHealth();
    }

    void Update_PlayerHealth()
    {
        if(!playerHealth){ return; }
        playerHealth_gauge.fillAmount = (float)playerHealth.GetNowHealth() / playerHealth.GetMaxHealth();
    }

    void Update_EnemyHealth()
    {
        if(!targetEnemy){ return; }
        enemyHealth_gauge.fillAmount = (float)targetEnemy.GetNowHealth() / targetEnemy.GetMaxHealth();
    }

    public void SetPlayerHealth(Health h)
    {
        playerHealth = h;
        playerHealth_gauge.fillAmount = (float)playerHealth.GetNowHealth() / playerHealth.GetMaxHealth();
    }

    public void SetEnemy(Enemy e)
    {
        targetEnemy = e;
        enemyHealth_gauge.fillAmount = (float)e.GetNowHealth() / e.GetMaxHealth();
        enemyHealth_Name.text = e.GetName();
    }
}
