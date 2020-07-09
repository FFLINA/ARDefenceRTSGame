using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static SoundManager;

public class Crystal : MonoBehaviour
{
    public static Crystal Instance;
    private void Awake()
    {
        Instance = this;
    }

    public GameObject beamEffect;
    public GameObject clearEffect;
    public GameObject destroyEffectF;
    GameObject destroyEffect;
    public GameObject afterEffectF;
    GameObject afterEffect;
    float hp;
    public float HP {
        get { return hp; }
        set 
        { 
            hp = value;
            //print("크리스탈의 HP : " + HP);
            if(hp <= 0)
            {
                UIManager.Instance.SetMessageUI("Crystal Destroyed...");

                Vector3 offset = new Vector3(-90f, 0, 0);
                destroyEffect = Instantiate(destroyEffectF);
                ScaleManager.Instance.ScaleFixForAR(destroyEffect);
                destroyEffect.transform.position = transform.position;
                destroyEffect.transform.eulerAngles = offset;

                afterEffect = Instantiate(afterEffectF);
                ScaleManager.Instance.ScaleFixForAR(afterEffect);
                afterEffect.transform.position = transform.position;
                afterEffect.transform.eulerAngles = offset;

                EffectClipsEnum effectClips = EffectClipsEnum.CrystalDestroy;
                SoundManager.Instance.PlayEffect(effectClips, 0.7f);

                Destroy(gameObject);
                GameManager.Instance.GameOver();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        HP = 1000f;

        //beamEffect.transform.position = transform.Find("BeamPoint").transform.position;
        //beamEffect.transform.parent = gameObject.transform;
        Vector3 beamRotate = new Vector3(-90f, 0, 0);
        beamEffect.transform.rotation = Quaternion.Euler(beamRotate);

        clearEffect.SetActive(false);

        EffectClipsEnum effectClips = EffectClipsEnum.CrystalBuild;
        SoundManager.Instance.PlayEffect(effectClips, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
