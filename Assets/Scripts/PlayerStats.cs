using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    private int cheeseScore = 0;
    private int mouseHealth = 3;
    private int winCondition = 10;
    bool Invulnerable = false;

    Rigidbody rb;
    float knockback = 25;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CheesePickUp")) {
            cheeseScore += 1;
            Destroy(collision.gameObject);
            Debug.Log($"Cheeses = {cheeseScore}");
        }
        
        if (collision.gameObject.CompareTag("MouseHole") && cheeseScore == winCondition) {
            Debug.Log("You Won");
        }
        else if (collision.gameObject.CompareTag("MouseHole") && cheeseScore != winCondition) {
            Debug.Log($"You need at least {winCondition} cheeses to Win");
        }

        if (collision.gameObject.CompareTag("Enemy") && Invulnerable == false) {
            mouseHealth -= 1;
            Debug.Log($"Health = {mouseHealth}");

            Vector3 difference = collision.transform.position - transform.position;
            difference = difference.normalized * knockback;
            rb.AddForce(difference, ForceMode.Impulse);

            StartCoroutine(InvincibilityFrames());
            Debug.Log("Can now take damage again");
        }
    }

    IEnumerator InvincibilityFrames()
    {
        Invulnerable = true;
        yield return new WaitForSeconds(3f);
        Invulnerable = false;
    }

}
