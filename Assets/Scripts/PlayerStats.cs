using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    private int cheeseScore = 0;
    private int maxHealth = 3;
    private int currentHealth;
    private int winCondition = 10;
    bool Invulnerable = false;

    Rigidbody rb;
    float knockback = 25;

    public UIController uiController;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        currentHealth = maxHealth;
        uiController.SetMaxHealth(maxHealth);
        uiController.SetCheese(cheeseScore, winCondition);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CheesePickUp")) {
            cheeseScore += 1;
            Destroy(collision.gameObject);
            uiController.SetCheese(cheeseScore, winCondition);
        }
        
        if (collision.gameObject.CompareTag("MouseHole") && cheeseScore >= winCondition) {
            uiController.Victory();
        }
        else if (collision.gameObject.CompareTag("MouseHole") && cheeseScore != winCondition) {
            Debug.Log($"You need at least {winCondition} cheeses to Win");
        }

        if (collision.gameObject.CompareTag("Enemy") && Invulnerable == false) {
            currentHealth -= 1;

            uiController.SetHealth(currentHealth);

            Vector3 difference = collision.transform.position - transform.position;
            difference = difference.normalized * knockback;
            rb.AddForce(difference, ForceMode.Impulse);

            StartCoroutine(InvincibilityFrames());

            if (currentHealth <= 0)
            {
                uiController.Defeat();
            }
        }
    }

    IEnumerator InvincibilityFrames()
    {
        Invulnerable = true;
        yield return new WaitForSeconds(3f);
        Invulnerable = false;
    }

}
