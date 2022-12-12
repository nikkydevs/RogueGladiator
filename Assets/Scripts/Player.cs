using UnityEngine;
using System.Collections;
using System;
using TMPro;

// We need to make some assumptions about what direction the
// character spawns in. Currenty assuming always spawns "down."

public class Player: MovingObject
{
    private float moveSpeed;
    private int health;
    private TextMeshProUGUI healthUIText;
    private GameObject gameManager;
    private GameManager gManagerScript;
    private Vector3 localScale;
    private Animator animator;


    protected override void Start()
    {
      gameManager = GameObject.Find("GameManager");
      gManagerScript = gameManager.GetComponent<GameManager>();
      health = gManagerScript.playerHealth;
      healthUIText = GameObject.Find("GameManager/UICanvas/HPtext").GetComponent<TextMeshProUGUI>();
      animator = this.GetComponent<Animator>();
      moveSpeed = 5f;
      updateHealthText();
      base.Start();
      localScale = transform.localScale;
    }

    protected override void Update()
    {
        base.Update();

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
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
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
    }

    void updateHealthText()
    {
      string healthStr = health.ToString();
      healthUIText.text = healthStr;
    }
}
