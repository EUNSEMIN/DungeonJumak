using UnityEngine;
using Data.Object;

[DisallowMultipleComponent]
public class Skill : MonoBehaviour
{
    public SkillDataSO skillData; // SO

    private Rigidbody2D rigid;

    private float damage;
    private float per;
    private float knockBack;
    private SkillController controller;

    public void Init(Vector3 direction)
    {
        this.damage = skillData.damage;
        this.per = skillData.per;
        this.knockBack = skillData.knockBack;

        if (per > -1)
        {
            rigid.velocity = direction * 10f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster") || per == -1)
            return;

        per--;

        if (per == -1)
        {
            rigid.velocity = Vector2.zero;

            gameObject.SetActive(false);
        }

    }
}
