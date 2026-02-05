using UnityEngine;
using System.Collections;

public class DamageOnContact : MonoBehaviour
{
    public int damage = 15;
    public float damageInterval = 1f;

    [Header("Knockback")]
    public float knockbackForce = 5f;

    private Coroutine damageCoroutine;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                damageCoroutine = StartCoroutine(DamageOverTime(collision.gameObject));
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    IEnumerator DamageOverTime(GameObject player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();

        while (true)
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            if (playerRb != null)
            {
                ApplyKnockback(playerRb);
            }

            yield return new WaitForSeconds(damageInterval);
        }
    }

    void ApplyKnockback(Rigidbody2D playerRb)
    {
        Vector2 direction = (playerRb.transform.position - transform.position).normalized;
        playerRb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }
}
