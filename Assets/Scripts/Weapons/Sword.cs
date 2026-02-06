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
            enemyEntity.TakeDamage(5);
        }
    }

    public void AttackColiderTurnOff()
    {
        polygonCollider2D.enabled = false;
    }

    public void AttackColiderTurnOn()
    {
        polygonCollider2D.enabled = true;
    }

    public void AttackColiderTurnOffOn()
    {
        AttackColiderTurnOff();
        AttackColiderTurnOn();
    }
}
