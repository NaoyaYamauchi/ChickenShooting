using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour {
    private Vector2 playerPosition;

    // Use this for initialization
    void Start () {
       GameObject player = GameObject.FindGameObjectWithTag("PlayerTag");
       Debug.Log(player.transform.position);
        playerPosition = player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = playerPosition;
        

        if (this.gameObject.transform.position.x < -3.1f
            || this.gameObject.transform.position.y < -5.1f
            ||this.gameObject.transform.position.x < -3.1f
            || this.gameObject.transform.position.y > 5.2f)
        {
            Destroy(this.gameObject);
        }
    }
}
