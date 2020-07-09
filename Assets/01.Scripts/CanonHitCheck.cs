using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundManager;

public class CanonHitCheck : MonoBehaviour
{
    // 부모에 의해 포물선으로 날아가면서
    // 땅이나 적에게 부딪히면
    // 그 주위 스피어 레이캐스트로 범위안에 들어온 적들 체크
    // 그 적들에게 데미지
    // Start is called before the first frame update
    void Start()
    {
        explosionF = Resources.Load<GameObject>("VFX_Canon_Explosion");
    }
    float explosionRadius = 1f;
    // Update is called once per frame
    void Update()
    {
    }

    GameObject explosionF;
    Enemy enemy;

    bool isTriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered == true)
            return;

        // 부딪힌 위치에 explosion표시, 범위안에 있는 오브젝트 destroy
        GameObject explosion = Instantiate(explosionF);
        ScaleManager.Instance.ScaleFixForAR(explosion);
        explosion.transform.position = transform.position;
        Destroy(explosion, 4.0f);

        EffectClipsEnum effectClips = EffectClipsEnum.CanonExplosion;
        SoundManager.Instance.PlayEffect(effectClips, 0.5f);

        Collider[] hitEnemies =
        Physics.OverlapSphere(transform.position, explosionRadius);
        // 상대들 파괴
        for (int i = 0; i < hitEnemies.Length; i++)
        {
            if (hitEnemies[i].gameObject.tag == "Enemy")
            {
                //체력 감소 시키고 체력이 0이면 죽음신호

                enemy = hitEnemies[i].gameObject.transform.parent.transform.GetComponent<Enemy>();

                enemy.HP -= transform.parent.GetComponent<Bullet>().AttackPower;
                // enemy.HP -= Grenade.attackPower;

            }
        }

        // 나 파괴
        Destroy(gameObject);
        isTriggered = true;
    }
}
