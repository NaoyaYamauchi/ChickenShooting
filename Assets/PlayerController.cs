using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    private object PrevPosition;
    private Vector2 Position;
    private Touch firstDetectedTouch;
    private float movableRangeX = 2.8f;
    private float movableRangeY = 3.7f;
    private int point = 0;
    //ゲーム終了判定
    public bool isEnd = false;
    //ボムを押した時の判定
    public bool bombButton = false;

    //情報表示
    public GameObject finalscoreText;
    private GameObject scoreText;
    // Use this for initialization
    void Start () {

        this.scoreText = GameObject.Find("Point");

    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.touchCount > 0)
        {

            Vector2 deltaPos = Input.GetTouch(0).deltaPosition;
            Vector3 newPos = transform.position + new Vector3(deltaPos.x, deltaPos.y, 0) * 0.003f;

            newPos.x = Mathf.Clamp(newPos.x, -movableRangeX, movableRangeX);
            newPos.y = Mathf.Clamp(newPos.y, -movableRangeY, movableRangeY);

            transform.position = newPos;
            
        }
        //ゲーム終了時にはタップでもどす。
        if (this.isEnd)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //GameSceneを読み込む
                SceneManager.LoadScene("GameScene");
            }
        }
    }

    //当たったら終了なのでゲームオーバー
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyTag")
        {
            isEnd = true;
            this.finalscoreText.GetComponent<Text>().text = "GAME OVER";

        }
    }
    //ボムを押した時の処理
    public void OnClick()
    {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("EnemyTag");
        Score();

        GetComponent<ParticleSystem>().Play();
        this.finalscoreText.GetComponent<Text>().text = point.ToString();
        isEnd = true;

        for(int i = 0; i < enemy.Length ; i++)
        {
            Destroy(enemy[i]);
        }

    }
    private void Score()
    {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("EnemyTag");
        GameObject player = GameObject.FindGameObjectWithTag("PlayerTag");
        Vector2 coordinatePlayer = player.transform.position;
        for (int i = 0; i < enemy.Length; i++)
        {
            Vector2 coordinateEnemy = enemy[i].transform.position;
            if(Vector2.Distance(coordinatePlayer, coordinateEnemy) <= 0.3)
            {
                point += 5000;
                Debug.Log(Vector2.Distance(coordinatePlayer, coordinateEnemy));
            }
            else if(Vector2.Distance(coordinatePlayer, coordinateEnemy) <= 0.5)
            {
                point += 3000;
                Debug.Log(Vector2.Distance(coordinatePlayer, coordinateEnemy));
            }
            else if (Vector2.Distance(coordinatePlayer, coordinateEnemy) <= 1.5)
            {
                point += 1000;
                Debug.Log(Vector2.Distance(coordinatePlayer, coordinateEnemy));
            }
            else
            {
                point += 100;
                Debug.Log(Vector2.Distance(coordinatePlayer, coordinateEnemy));
            }
        }
    }
}
