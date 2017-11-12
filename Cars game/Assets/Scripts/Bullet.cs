using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float damage;
    public float speed;
    public GameObject bulletImpactEffect;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy hitEnemy = collision.gameObject.GetComponent<Enemy>();
            hitEnemy.DamageEnemy(damage);
            if (hitEnemy.enemyType.type == Enemy.Type.Muscle)
            {
                AudioManager.instance.PlaySound("ImpactExplosion");
            } else if (hitEnemy.enemyType.type == Enemy.Type.Pickup)
            {
                AudioManager.instance.PlaySound("ImpactExplosion");
            }
        }

        else if (collision.gameObject.tag == "Player" && !collision.gameObject.GetComponent<Player>().isDead)
        {
            collision.gameObject.GetComponent<Player>().DamagePlayer(damage);
        }
        else
        {
            AudioManager.instance.PlaySound("MissImpact");
        }
        GameObject effect = Instantiate(bulletImpactEffect, transform.position, transform.rotation);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }
}