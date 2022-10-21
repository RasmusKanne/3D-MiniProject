using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    private int cheeseScore = 0;
    private int mouseHealth = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CheesePickUp")) {
            cheeseScore += 1;
            Destroy(collision.gameObject);
            Debug.Log(cheeseScore);
        }
    }
}
