using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerHealthBar : MonoBehaviour
{
    //public GameObject healthBarUI;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GameObject.Find("EnemyHealthbar").GetComponent<HealthBar>();
        healthBar.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            healthBar.gameObject.SetActive(true);
            //Debug.Log("in");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            healthBar.gameObject.SetActive(false);
            //Debug.Log("out");
        }
            
    }

}
