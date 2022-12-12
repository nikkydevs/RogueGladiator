using UnityEngine;
using System.Collections;
using System;
using TMPro;

public class Player: MovingObject
{
    private float moveSpeed;
    private int health;
    private int damage;
    public float knockbackForce = 100f;
    private TextMeshProUGUI healthUIText;
    private GameObject gameManager;
    private GameManager gManagerScript;
    private Vector3 localScale;
    private Animator animator;
    public Transform attackPointDown;
    public Transform attackPointUp;
    public Transform attackPointLeft;
    public Transform attackPointRight;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    private Transform attackPoint;

    protected override void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gManagerScript = gameManager.GetComponent<GameManager>();
        health = gManagerScript.playerHealth;
        moveSpeed = gManagerScript.playerMoveSpeed;
        damage = gManagerScript.playerAtkDamage;
        healthUIText = GameObject.Find("GameManager/UICanvas/HPtext").GetComponent<TextMeshProUGUI>();
        animator = this.GetComponent<Animator>();
        updateHealthText();
        base.Start();
        localScale = transform.localScale;
    }

    protected override void Update()
    {
        base.Update();
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", velocity.x);
        animator.SetFloat("Vertical", velocity.y);
        animator.SetFloat("Speed", velocity.sqrMagnitude);  //might change

        if (velocity.x != 0 || velocity.y != 0)
        {
            animator.SetFloat("LastX", velocity.x);
            animator.SetFloat("LastY", velocity.y);
        }

        velocity.Normalize();
        velocity *= moveSpeed;

        if (velocity.x > 0) 
        {
            attackPoint = attackPointRight;
        }
        else if (velocity.x < 0)
        {
            attackPoint = attackPointLeft;
        }
        else if (velocity.y > 0)
        {
            attackPoint = attackPointUp;
        }
        else if (velocity.y < 0)
        {
            attackPoint = attackPointDown;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            Vector2 dir = (enemy.transform.position - transform.position).normalized;
            Vector2 knockback = dir * knockbackForce;
            enemy.gameObject.GetComponent<Enemy1>().TakeDamage(damage, knockback);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(int amount)
    {
      health -= amount;
      if (health <= 0)
      {
        health = 0;
        //player death
        gManagerScript.Die();
      }
      //update game manager player health
      gManagerScript.playerHealth = health;
      updateHealthText();
      Debug.Log("my health is down to " + health);
    }

    void updateHealthText()
    {
      string healthStr = health.ToString();
      healthUIText.text = healthStr;
    }
}