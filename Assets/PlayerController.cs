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

    //得点
    private int near_one = 0;
    private int near_two = 0;
    private int near_three = 0;
    private int near_four = 0;

    //開始判定
    public bool isStart = false;
    //ゲーム終了判定
    public bool isEnd = false;
    public bool gameover = false;
    //ボムを押した時の判定
    public bool bombButton = false;
    public bool endCount = false;

    //情報表示
    public GameObject finalscoreText;
    private GameObject scoreText;
    private GameObject startText;
    private GameObject resultText;
    // Use this for initialization
    void Start () {

        this.scoreText = GameObject.Find("Point");
        this.startText = GameObject.Find("Start");
        this.resultText = GameObject.Find("Result");

        Time.timeScale = 0f;
        this.startText = GameObject.Find("Start");
       
            this.startText.GetComponent<Text>().text = "ChickenShoting\nタップで開始";
        
    }
	
	// Update is called once per frame
	void Update () {

        if (!isEnd)
        {
            if (Input.touchCount > 0)
            {
                isStart = true;
                Time.timeScale = 1f;
                this.startText.GetComponent<Text>().text = "　";
                Vector2 deltaPos = Input.GetTouch(0).deltaPosition;
                Vector3 newPos = transform.position + new Vector3(deltaPos.x, deltaPos.y, 0) * 0.003f;

                newPos.x = Mathf.Clamp(newPos.x, -movableRangeX, movableRangeX);
                newPos.y = Mathf.Clamp(newPos.y, -movableRangeY, movableRangeY);

                transform.position = newPos;

            }
        }
        //ゲーム終了時にはタップでもどす。
        if (this.isEnd)
        {
            Invoke("DelayMethod", 1.5f);
        }
    }

    //当たったら終了なのでゲームオーバー
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "EnemyTag"&&!endCount)
        {
            endCount = true;
            gameover = true;
            isEnd = true;
            this.finalscoreText.GetComponent<Text>().text = "GAME OVER";
        }
    
    }
    //ボムを押した時の処理
    public void OnClick()
    {
        if (!gameover&&!endCount)
        {
            endCount = true;
            GameObject[] enemy = GameObject.FindGameObjectsWithTag("EnemyTag");
            Score();

            GetComponent<ParticleSystem>().Play();
            this.finalscoreText.GetComponent<Text>().text = point.ToString();
            this.resultText.GetComponent<Text>().text = "ギリギリ:" + near_one + "×5000\nニアピン:" + near_two + "×3000\n1馬身差:"+ near_three + "×1000\nとりあえず画面内:"+ near_four + "×100";
            isEnd = true;

            for (int i = 0; i < enemy.Length; i++)
            {
                Destroy(enemy[i]);
            }
        }

    }
    private void Score()
    {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("EnemyTag");
        GameObject player = GameObject.FindGameObjectWithTag("PlayerTag");
        Vector2 coordinatePlayer = player.transform.position;

        double[] distance = new double[enemy.Length];

        
        //並び替える
        for (int i = 0; i < enemy.Length; i++)
        {
            Debug.Log(distance);
        }

        for (int i = 0; i < enemy.Length; i++)
        {
            Vector2 coordinateEnemy = enemy[i].transform.position;
            if(Vector2.Distance(coordinatePlayer, coordinateEnemy) <= 0.3)
            {
                point += 5000;
                near_one++;
            }
            else if(Vector2.Distance(coordinatePlayer, coordinateEnemy) <= 0.5)
            {
                point += 3000;
                near_two++;
            }
            else if (Vector2.Distance(coordinatePlayer, coordinateEnemy) <= 1.5)
            {
                point += 1000;
                near_three++;
            }
            else
            {
                point += 100;
                Debug.Log(Vector2.Distance(coordinatePlayer, coordinateEnemy));
                near_four++;
            }

            Destroy(enemy[i]);
        }
    }
    void DelayMethod()
    {
        this.startText.GetComponent<Text>().text = "タップでもう一度";
        if (Input.GetMouseButtonDown(0))
        {

            //GameSceneを読み込む
            SceneManager.LoadScene("GameScene");
        }
    }
}
