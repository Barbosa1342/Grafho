using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthSystem : MonoBehaviour
{
    [SerializeField] float maxHealth;
    public int MaxHealth => (int)maxHealth;

    float currentHealth;
    public float CurrentHealth => currentHealth;

    bool isDamageble;
    public bool IsDamageble => isDamageble;
    // I dont know if this word exist
    // basically, it means if the object can take damage
    // as it is not attacking or have already taked damage

    [SerializeField] float cooldownTime = 0.5f;
    float timer;

    private void OnEnable()
    {
        setDamageble(false);
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (timer <= 0f)
        {
            setDamageble(true);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
    public void ChangeHealth(float amount)
    {
        if (isDamageble)
        {
            float oldHealth = currentHealth;
            currentHealth += amount;

            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            setDamageble(false);

            if (gameObject.tag == "Player")
            {
                sceneManager.sceneManagerObj.updateHeart();
            }
        }
        

        if (currentHealth <= 0)
        {
            if (gameObject.tag == "Enemy")
            {
                pointSystem.changePoint(1);
                enemyAnimManager anim = gameObject.GetComponent<enemyAnimManager>();
                anim.setDying(true);
            }
            else if (gameObject.tag == "Player")
            {
                animManag.anim.setDying(true);
            }
        }
    }

    public void setDamageble(bool isDam)
    {
        isDamageble = isDam;
        if (!isDam)
        {
            timer = cooldownTime;
        }
    }

    public void desactiveInstance()
    {
        gameObject.SetActive(false);

        if (gameObject.tag == "Player")
        {
            sceneManager.sceneManagerObj.GameOverScreen();
        }
    }
}
