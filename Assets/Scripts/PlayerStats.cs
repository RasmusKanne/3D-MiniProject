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

        // Sets UI to sync with variables in this script and initializes currenthealth to be equal to maxhealth
        currentHealth = maxHealth;
        uiController.SetMaxHealth(maxHealth);
        uiController.SetCheese(cheeseScore, winCondition);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            uiController.Pause();
        }
    }

    // Within the OnCollisionEnter function all the players interactions are with the world is run based on what type of tag the collided object has
    private void OnCollisionEnter(Collision collision)
    {
        // Checks if the object is a cheese, then it increases the cheesescore int and destroys the object
        // Afterwards it sync the new score with the UI, and hides the introduction text after taking the two cheeses at the beginning
        if (collision.gameObject.CompareTag("CheesePickUp")) {
            cheeseScore += 1;
            Destroy(collision.gameObject);
            uiController.SetCheese(cheeseScore, winCondition);
            if (cheeseScore == 2)
            {
                uiController.HideIntro();
            }
        }
        
        // Checks for the amount of cheeses and if it is enough the player wins when going into the mousehole
        if (collision.gameObject.CompareTag("MouseHole") && cheeseScore >= winCondition) {
            uiController.Victory();
        }
        else if (collision.gameObject.CompareTag("MouseHole") && cheeseScore != winCondition) {
            Debug.Log($"You need at least {winCondition} cheeses to Win");
        }

        // When the enemy objects collides with the player:
        if (collision.gameObject.CompareTag("Enemy") && Invulnerable == false) {
            currentHealth -= 1; // the player loses a life

            uiController.SetHealth(currentHealth); // Healthbar is updated

            // The player is knocked back from the enemy
            Vector3 difference = collision.transform.position - transform.position;
            difference = difference.normalized * knockback;
            rb.AddForce(difference, ForceMode.Impulse);

            StartCoroutine(InvincibilityFrames()); // Avoids the player from colliding multiple times with the same enemy

            if (currentHealth <= 0)
            {
                uiController.Defeat(); // The player loses when life gets to 0
            }
        }
    }

    // Uses the IEnumerator to make the program wait 3 seconds before making the player vulnerable again
    IEnumerator InvincibilityFrames()
    {
        Invulnerable = true;
        yield return new WaitForSeconds(3f);
        Invulnerable = false;
    }

}
