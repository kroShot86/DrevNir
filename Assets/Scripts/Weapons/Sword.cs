using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private int damage = 5;

    public event EventHandler OnSwordSwing;

    private PolygonCollider2D polygonCollider2D;

    private void Awake()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        if (polygonCollider2D == null)
        {
            polygonCollider2D = gameObject.AddComponent<PolygonCollider2D>();
            polygonCollider2D.isTrigger = true; 
        }
    }

    private void Start()
    {
        AttackColiderTurnOff();
    }

    public void Attack()
    {
        AttackColiderTurnOffOn();
        OnSwordSwing?.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.TryGetComponent(out EnemyEnity enemyEntity))
        {
            enemyEntity.TakeDamage(damage);
        }
    }

    public void AttackColiderTurnOff()
    {
        if (polygonCollider2D != null)
        {
            polygonCollider2D.enabled = false;
        }
    }

    public void AttackColiderTurnOn()
    {
        if (polygonCollider2D != null)
        {
            polygonCollider2D.enabled = true;
        }
    }

    public void AttackColiderTurnOffOn()
    {
        AttackColiderTurnOff();
        AttackColiderTurnOn();
    }
}