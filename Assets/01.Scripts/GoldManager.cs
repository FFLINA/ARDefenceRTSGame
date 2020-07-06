using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    // 게임의 전체 골드를 관리하는 골드매니저 스크립트


    public static GoldManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public Text txtGold;
    string goldMent = "$ ";
    int curGold;
    public int Gold
    {
        get
        {
            return curGold;
        }
        set
        {
            curGold = value;
            txtGold.text = goldMent + curGold.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        curGold = 1000;
        txtGold.text = goldMent + curGold.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
