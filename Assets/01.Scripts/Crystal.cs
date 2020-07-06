using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public static Crystal Instance;
    private void Awake()
    {
        Instance = this;
    }

    public GameObject beamEffectF;
    public GameObject clearEffectF;

    float hp;
    public float HP {
        get { return hp; }
        set 
        { 
            hp = value;
            print("크리스탈의 HP : " + HP);
            if(hp <= 0)
            {
                print("크리스탈 파괴");
                Destroy(gameObject);
                GameManager.Instance.GameOver();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        HP = 1000f;
        GameObject beam = Instantiate(beamEffectF);
        beam.transform.position = transform.Find("BeamPoint").transform.position;
        beam.transform.parent = gameObject.transform;
        Vector3 beamRotate = new Vector3(-90f, 0, 0);
        beam.transform.rotation = Quaternion.Euler(beamRotate);

        clearEffectF.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
