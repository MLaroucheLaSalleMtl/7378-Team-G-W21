using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBehaviour : MonoBehaviour
{
    [SerializeField] private float fireballDamage = 30f;
    [SerializeField] private float fireballSpeed = 7f;
    private GameObject otherPlayer;
    //[SerializeField] private GameObject fireball;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.root.CompareTag("Player1") || other.gameObject.transform.root.CompareTag("Player2"))
        {
            otherPlayer = other.transform.root.gameObject;
            otherPlayer.GetComponent<FighterStatus>().ReceiveDamage(fireballDamage);

            Destroy(gameObject);
        }
    }

    public void FireballMoveP1()
    {
        transform.position -= new Vector3(1 * Time.deltaTime * fireballSpeed, 0, 0);
    }

    public void FireballMoveP2()
    {
        transform.position += new Vector3(1 * Time.deltaTime * fireballSpeed, 0, 0);
    }

    private void Update()
    {
        if (gameObject.CompareTag("Player1"))   
        {
            FireballMoveP1();
        }
        else if (gameObject.CompareTag("Player2"))
        {
            FireballMoveP2();
        }
    }
}
