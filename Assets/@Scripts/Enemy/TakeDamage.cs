using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum collisionType { Head, Body, Limbs}
public class TakeDamage : MonoBehaviour
{
    [SerializeField] public collisionType damageType;
    private HealthSystem healthController;

    private void Awake()
    {
        healthController = GetComponentInParent<HealthSystem>();
    }

    public void CallDamage(float damage)
    {
        switch (damageType)
        {
            case collisionType.Head:
                healthController.HitDamage(damage * 2);
                break;
            case collisionType.Body:
                healthController.HitDamage(damage);
                break;
            case collisionType.Limbs:
                healthController.HitDamage(damage / 2);
                break;
        }
    }
}
