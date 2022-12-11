using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapBehaviorScript : MonoBehaviour
{
    public int damage = 5;
    Player component;

    void Start()
    {
      GameObject player = GameObject.Find("/GameManager/Playerground/Player");
      component = player.GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
      Debug.Log(string.Format("haha idiot you stepped in the KNIFE PILE and took {0} damage!", damage));
      component.TakeDamage(damage);
    }
}