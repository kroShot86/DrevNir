using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnity : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;
    private PolygonCollider2D polygonCollider2D;

    private void Awake()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        DetectDeath();
    }

    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void PolygonCollider2DTurnOff()
    {
        polygonCollider2D.enabled = false;
    }

    public void PolygonCollider2DTurnOn()
    {
        polygonCollider2D.enabled = true;
    }
}
