using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    //プレイヤーの現在地
    private Vector2 playerPosition;

    private GameObject player;

    private Vector2 speed = new Vector2(0.01f, 0.01f);

    private float radian;

    private Vector2 Position;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerTag");
        radian = Mathf.Atan2(player.transform.position.y - transform.position.y,
           player.transform.position.x - transform.position.x);
        
        Debug.Log(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Position = transform.position;

        Position.x += speed.x * Mathf.Cos(radian);
        Position.y += speed.y * Mathf.Sin(radian);

        transform.position = Position;

        if (this.gameObject.transform.position.x < -3.1f
            || this.gameObject.transform.position.y < -5.1f
            || this.gameObject.transform.position.x < -3.1f
            || this.gameObject.transform.position.y > 5.2f)
        {
            Destroy(this.gameObject);
        }
    }
}
