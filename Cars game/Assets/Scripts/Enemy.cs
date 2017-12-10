using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public enum Type { Muscle, Pickup }

    public float range;
    public float turnDamping;
    public float shootCooldownTime;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform partRotate;
    public float startHP = 100f;
    public Image healthBar;
    public GameObject explosionEffectPrefab;

    private bool canFire;
    private float health;
    private Player player;
    [HideInInspector] public EnemyType enemyType;

    void Start()
    {
        player = FindObjectOfType<Player>();
        canFire = true;
        health = startHP;
    }

    void Update()
    {
        if (player != null && !player.isDead)
        {
            float distToEnemy = Vector3.Distance(transform.position, player.transform.position);

            if (distToEnemy <= range)
            {
                Ray ray = new Ray(transform.position, (player.transform.position - transform.position));
                RaycastHit hit;
                Debug.DrawRay(transform.position, (player.transform.position - transform.position));
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    Debug.Log("Raycast success");
                    Debug.Log(
                        hit.transform.gameObject.name);
                    Debug.Log(
                        player.gameObject.name);
                    if (hit.transform.root.gameObject == player.gameObject)
                    {
                        Debug.Log("hit");
                        LockOntoPlayer();
                        ShootPlayer();
                    }
                }
            }
            else
            {
                // Patrol.
            }
        }
    }

    public void DamageEnemy(float damage)
    {
        health -= damage;

        healthBar.fillAmount = (health / startHP);

        if (health <= 0)
        {
            GameMaster.instance.enemiesKilled++;
            GameObject explosionEffect = Instantiate(explosionEffectPrefab, transform.position, explosionEffectPrefab.transform.rotation);
            Destroy(explosionEffect, 4f);
            Destroy(gameObject);
            GameMaster.instance.enemiesAlive--;
        }
    }

    void ShootPlayer()
    {
        if (!canFire) return;

        canFire = false;
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Destroy(bulletGO, 10f);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());//ensure the bullet cannot collide with the player
        foreach (Bullet b in FindObjectsOfType<Bullet>())// for each bullet in the scene currently
        {
            if (b != bullet) // as long as the bullet we're currently checking isn't the one we just spawned in...
            {
                Physics.IgnoreCollision(bullet.GetComponent<Collider>(), b.GetComponent<Collider>());
            }
        }

        StartCoroutine(WaitForShootCooldown());
    }

    void LockOntoPlayer()
    {
        Vector3 lookDir = player.transform.position - partRotate.position;
        lookDir.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookDir);
        partRotate.rotation = Quaternion.Slerp(partRotate.rotation, rotation, turnDamping * Time.deltaTime);
    }

    IEnumerator WaitForShootCooldown()
    {
        float t = shootCooldownTime;

        while (t > 0)
        {
            t -= Time.deltaTime;
            yield return 0;
        }

        canFire = true;
    }
}