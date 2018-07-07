using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batteries : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Aumentar vidas do player se for < 3
        if (collision.gameObject.GetComponent<Player>().AmountOfShots < Player.maxAmountOfShots)
        {
            Destroy(gameObject);
        }
    }

}
