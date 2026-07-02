using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player1HealthScripts : MonoBehaviour
{
    public GameManager gameManager;
    public float health;
    private float lerpTime;
    public float maxHealth = 100f;

    private float chipSpeed = 2f;

    public Image backgroundHealthBar;
    public Image foregroundHealthBar;
    public float fadeSpeed = 2f;


void Start()
{
    health = maxHealth;
}

    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        UpdateHealthUI();
    }
    public void UpdateHealthUI()
    {
        float fillAmountB = backgroundHealthBar.fillAmount;
        float fillAmountF = foregroundHealthBar.fillAmount;
        float normalizedHealth = health / maxHealth;

        if (fillAmountB > normalizedHealth)
        {
            foregroundHealthBar.fillAmount = normalizedHealth;
            backgroundHealthBar.color = Color.red;
            lerpTime += Time.deltaTime;
            float percentCompleted = lerpTime / chipSpeed;
            percentCompleted = percentCompleted * percentCompleted;
            backgroundHealthBar.fillAmount = Mathf.Lerp(fillAmountB, normalizedHealth, percentCompleted);
        }

        if (fillAmountF < normalizedHealth)
        {
            backgroundHealthBar.fillAmount = normalizedHealth;
            backgroundHealthBar.color = Color.green;
            lerpTime += Time.deltaTime;
            float percentCompleted = lerpTime / chipSpeed;
            percentCompleted = percentCompleted * percentCompleted;
            foregroundHealthBar.fillAmount = Mathf.Lerp(fillAmountF, normalizedHealth, percentCompleted);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTime = 0f;
    }

}