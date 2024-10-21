// Engine
using UnityEngine;

// Ect
using Data.Object;

namespace Skill
{
    public class Skill : MonoBehaviour
    {
        [Header("SO")]
        public SkillDataSO skillData;

        private Rigidbody2D rigid;
        private float per;

        public void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        public void Init(Vector3 direction)
        {
            this.per = skillData.per;

            if (per > -1)
            {
                rigid.velocity = direction * 10f;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Monster") || per == -1)
                return;

            Debug.Log("Hit Monster");

            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);

            /*per--;

            if (per == -1)
            {
                rigid.velocity = Vector2.zero;
                gameObject.SetActive(false);
            }*/

        }
    }

}
