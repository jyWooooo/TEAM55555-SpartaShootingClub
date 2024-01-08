using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    private bool isDie;
    public float currentHealth { get; private set; }


    public event Action OnDeath;
    private void Start()
    {
        InitHealthSystem();
    }

    public void InitHealthSystem()
    {
        currentHealth = maxHealth;
        isDie = false;
    }

    public virtual void HitDamage(float damage)
    {
        if(damage <=0 || isDie)
        {
            return;
        }

        Debug.Log("맞기 전 데미지" + currentHealth);
        currentHealth -= damage;
        Debug.Log("맞은 후 데미지" + currentHealth);
        currentHealth = currentHealth < 0 ? 0 : currentHealth;

        if (currentHealth <= 0f)
        {
            CallDeath();
        }
    }

    protected virtual void CallDeath()
    {
        Debug.Log("죽었음");
        SetDeath();
        GetComponent<EnemyController>().Die();
    }

    protected void SetDeath()
    {
        isDie = true;
        OnDeath?.Invoke();
    }
}