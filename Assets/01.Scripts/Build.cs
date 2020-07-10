using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SoundManager;

public class Build : MonoBehaviour
{
    // 모든 건물들의 부모클래스
    // Build    -   Tower   -   ArcherTower...
    //                      -   ...
    //          -   Barrack -   ...

    // 모든 건물들의 공통된 속성, 기능들

    // 체력, 건설비용, 판매비용, 다음업그레이드건물
    // 자신이 건설 된 게임 필드의 포지션
    // 업그레이드, 판매, 파괴
    // 파괴 함수는 Tower, Barrack 에서 각자 구현 후 상속

    protected float hp;
    protected float maxHp;
    public float HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            hpSlider.value = hp;
            print("이 건물의 현재 체력 : " + hp);

            //hpSlider.transform.Find("Fill").GetChild(0).GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, hp / maxHp);
            Image hpSlider_fill = hpSlider.transform.Find("Fill Area").GetChild(0).GetComponent<Image>();
            hpSlider_fill.color = Color.Lerp(Color.red, Color.green, hp / maxHp);
            //if (hp <= hp * 0.7f)
            //{
            //    // 70퍼보다 적으면 슬라이더의 색 변경
            //}
            //else if (hp <= hp * 0.35f)
            //{
            //    // 35퍼보다 적으면
            //}
            if (hp <= 0)
            {
                // 0이하면 건물 파괴
                BuildDestroy();
            }
        }
    }

    public virtual void BuildDestroy()
    {
        print("건물 파괴됨");
        // 판매,업그레이드가 아니라 파괴 될 때
        if (destroyEffectF != null)
        {
            GameObject destroyE = Instantiate(destroyEffectF);
            destroyE.transform.position = transform.position + Vector3.up;
            ScaleManager.Instance.ScaleFixForAR(destroyE);
            Destroy(destroyE, 1.5f);
            destroyEffectClip = EffectClipsEnum.TowerDestroy;
            SoundManager.Instance.PlayEffect(destroyEffectClip, 0.5f);
        }
    }


    protected int buildCost, sellGold;
    public int Cost { get { return buildCost; } set { buildCost = value; } }
    public int SellGold { get { return sellGold; } set { sellGold = value; } }

    protected GameObject nextUpgradeF;

    // 건물이 건설된 필드의 정보를 가지고 있다
    protected GameObject buildedField;

    GameObject hpUIFactory;
    protected Slider hpSlider;

    // 사운드이펙트 클립 설정 변수
    protected EffectClipsEnum buildEffectClip;
    protected EffectClipsEnum attackEffectClip;
    protected EffectClipsEnum sellEffectClip;
    protected EffectClipsEnum destroyEffectClip;

    // 생성, 판매 이펙트
    GameObject spawnEffectF;
    GameObject sellEffectF;
    GameObject destroyEffectF;

    public virtual void Awake()
    {
        spawnEffectF = Resources.Load<GameObject>("VFX_TowerSpawn");
        sellEffectF = Resources.Load<GameObject>("VFX_TowerSell");
        destroyEffectF = Resources.Load<GameObject>("VFX_TowerDestroy");
        hpUIFactory = Resources.Load<GameObject>("UI_HP");
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        //// 건물 생성시 크리스탈 반대방향을 바라보게 한다
        //Vector3 dir = BuildManager.Instance.CrystalPosition - transform.position;
        //dir.Normalize();  
        //transform.forward = dir;

        GameObject spawnE = Instantiate(spawnEffectF);
        ScaleManager.Instance.ScaleFixForAR(spawnE);
        spawnE.transform.position = transform.position;
        Destroy(spawnE, 1.5f);

        buildEffectClip = EffectClipsEnum.TowerBuild;
        SoundManager.Instance.PlayEffect(buildEffectClip, 0.5f);

        GameObject hpUI = Instantiate(hpUIFactory);
        hpUI.transform.parent = transform;
        Vector3 offset = new Vector3(0, 0.5f, 0);
        hpUI.transform.position = transform.Find("Point").transform.position + offset;
        hpSlider = hpUI.transform.GetComponentInChildren<Slider>();

    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    int nextCost;
    internal int GetNextCost()
    {
        if (nextUpgradeF != null)
        {
            GameObject nextTower = Instantiate(nextUpgradeF);
            ScaleManager.Instance.ScaleFixForAR(nextTower);
            nextCost = nextTower.GetComponent<Build>().Cost;
            Destroy(nextTower);
        }
        return nextCost;
    }

    public virtual void Sell()
    {
        // 자신의 sellGold만큼 골드를 추가하고 자신 파괴
        GoldManager.Instance.Gold += SellGold;
        // 타워, 배럭에서 자신의 Destroy 함수 추가필요
        // 아마 이게 인터페이스 인가 
        GameObject sellE = Instantiate(sellEffectF);
        sellE.transform.position = transform.position;
        ScaleManager.Instance.ScaleFixForAR(sellE);
        Destroy(sellE, 1.5f);
        destroyEffectF = null;
        sellEffectClip = EffectClipsEnum.TowerSell;
        SoundManager.Instance.PlayEffect(sellEffectClip, 0.5f);
    }

    public virtual void Upgrade()
    {
        if (nextUpgradeF != null)
        {
            // 업그레이드 비용 체크
            GameObject nextTower = Instantiate(nextUpgradeF);
            ScaleManager.Instance.ScaleFixForAR(nextTower);
            nextCost = nextTower.GetComponent<Build>().Cost;
            if (GoldManager.Instance.Gold >= nextCost)
            {
                nextTower.GetComponent<Build>().SetBuildedField(buildedField);
                nextTower.transform.position = transform.position;
                Destroy(gameObject);
            }
            else
            {
                Destroy(nextTower);
                UIManager.Instance.SetMessageUI("Not Enough Money.");
            }

        }
        else
        {
            UIManager.Instance.SetMessageUI("Tower Max Upgrade.");
        }
    }

    public void SetBuildedField(GameObject clickedField)
    {
        buildedField = clickedField;
    }


}
