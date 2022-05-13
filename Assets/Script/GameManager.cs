using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour{

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    [SerializeField]
    private GameObject wolf;

    private int score;

    [SerializeField]
    private Text scoreTxt;
    [SerializeField]
    private Transform objbox;

    [SerializeField]
    private Text bestScore;
    [SerializeField]
    private GameObject panel;

    // Use this for initialization
    void Start()
    {
        Screen.SetResolution(768, 1024, false);

    }

    // Update is called once per frame
    void Update(){

    }

    public bool stopTrigger = true;
    public void GameOver()
    {
        stopTrigger = false;

        StopCoroutine(CreatewolfRoutine());

        if (score >= PlayerPrefs.GetInt("BestScore", 0))
            PlayerPrefs.SetInt("BestScore", score);

        bestScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();

        panel.SetActive(true);
    }

    public void GameStart()
    {
        score = 0;
        scoreTxt.text = "지킨 바구니 : " + score;
        stopTrigger = true;
        StartCoroutine(CreatewolfRoutine());
        panel.SetActive(false);
    }

    public void Score()
    {
        if (stopTrigger)
            score++;
        scoreTxt.text = "지킨 바구니 : " + score;
    }

    IEnumerator CreatewolfRoutine()
    {
        while (stopTrigger)
        {
            CreateWolf();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void CreateWolf()
    {
        float rx = UnityEngine.Random.Range(0.0f, 1.0f) * 6 - 4;
        Vector3 pos = new Vector3(rx,6,0);
        //Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0.0f, 1.0f), 1.1f, 0)); //카메라안에서만 생성 가능하게 만듬
        //pos.z = 0.0f;
        //Wolf w = 
        Instantiate(wolf, pos, Quaternion.identity);
        
    }
}