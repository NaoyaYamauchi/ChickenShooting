using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {

    public GameObject enemyPrefab;
    //経過時間
    private float oneSecond;


    //出現範囲
    //上から
    float minXup = -3.0f;
    float maxXup = 3.0f;
    float minYup = 5.00f;
    float maxYup = 5.01f;
    //左から
    float minXleft = -3.1f;
    float maxXleft = -3.0f;
    float minYleft = -5.0f;
    float maxYleft = 5.0f;
    //右から
    float minXright = 3.0f;
    float maxXright = 3.1f;
    float minYright = -5.0f;
    float maxYright = 5.0f;
    //下から
    float minXunder = -3.0f;
    float maxXunder = 3.0f;
    float minYunder = -5.1f;
    float maxYunder = -5.0f;

    int loop = 0;

    //ゲーム終了判定
    public PlayerController playerController;
    public bool isEnd = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // GameObject[] enemyTags = GameObject.FindGameObjectsWithTag("EnemyTag");
        //isEnd = playerController.isEnd;
        Debug.Log(isEnd);
        //弾が増えすぎても問題なので制限
        if(isEnd==false)
        {
            //1秒ごとに生成できるように
            oneSecond -= Time.deltaTime;
            if(oneSecond <= 0.0)
            {
                oneSecond = 0.5f;
                Instantiate(enemyPrefab);
                Debug.Log("これは1秒ごとに動く処理");
                Vector3 randomPos = new Vector3(
                    Random.Range(minXup, maxXup),
                    Random.Range(minYup, maxYup),
                    0.0f);
                if (loop % 4 == 2)
                {
                    randomPos = new Vector3(
                    Random.Range(minXleft, maxXleft),
                    Random.Range(minYleft, maxYleft),
                    0.0f);
                }
                if (loop % 4 == 3)
                {
                    randomPos = new Vector3(
                    Random.Range(minXright, maxXright),
                    Random.Range(minYright, maxYright),
                    0.0f);
                }
                if (loop % 4 == 0)
                {
                    randomPos = new Vector3(
                    Random.Range(minXunder, maxXunder),
                    Random.Range(minYunder, maxYunder),
                    0.0f);

                }
                
                enemyPrefab.transform.position = randomPos;
                loop++;
            }

        }
    }
    public void End()
    {

    }
}
