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
    //ゲーム終了判定
    private bool isEnd = false;
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

        //現時点で表示されている弾の数をコンソールに出すため
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("EnemyTag");
        Debug.Log(enemy.Length);
        if (Input.touchCount > 0)
        {

            Vector2 deltaPos = Input.GetTouch(0).deltaPosition;
            Vector3 newPos = transform.position + new Vector3(deltaPos.x, deltaPos.y, 0) * 0.003f;

            newPos.x = Mathf.Clamp(newPos.x, -movableRangeX, movableRangeX);
            newPos.y = Mathf.Clamp(newPos.y, -movableRangeY, movableRangeY);

            transform.position = newPos;
            
        }
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
        Debug.Log("CLICK");
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("EnemyTag");
        Debug.Log(gameObject.name);
        this.finalscoreText.GetComponent<Text>().text = enemy.Length.ToString();
        isEnd = true;
    }

}
