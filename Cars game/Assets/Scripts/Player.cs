using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootCooldownTime = 0.3f;
    private bool canFire;
    public float startHP = 100f;
    public Image healthBar;
    private float health;
    [HideInInspector] public bool isDead = false;

    void Start ()
    {
        isDead = false;
        canFire = true;
        health = startHP;
	}
	
	void Update ()
    {
        if (isDead) return;

        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            AudioManager.instance.PlaySound("Shot");
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
	}

    public void DamagePlayer(float damage)
    {
        health -= damage;

        healthBar.fillAmount = (health / startHP);

        if (health <= 0)
        {
            HidePlayer();
            isDead = true;
            GameMaster.instance.GameOver();
        }
    }

    void HidePlayer()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform currentChild = transform.GetChild(i);
            if (currentChild.GetComponent<Camera>() != null) continue;
            currentChild.gameObject.SetActive(false);
        }
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
