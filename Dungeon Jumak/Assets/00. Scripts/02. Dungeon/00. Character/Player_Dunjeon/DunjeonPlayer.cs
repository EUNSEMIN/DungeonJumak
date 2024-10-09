//Engine
using UnityEngine;

//Interface
using Interfaces;

//Ect
using Data.Character;

public class DunjeonPlayer : MonoBehaviour, IDamageable, ITurnable, IMovable
{
    #region Variables

    public DunjeonPlayerDataSO data; // SO
    public Scanner scanner; // 스캐너

    // 핸들러
    private DP_AnimationHandler animationHandler;
    private DP_MoveHandler moveHandler;

    // 컴포넌트
    private Rigidbody2D rigidbody;
    private Transform transform;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    #endregion

    private void Awake()
    {
        //-- 컴포넌트 할당 --//
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //-- 핸들러 인스턴스 생성 --//
        animationHandler = new DP_AnimationHandler(spriteRenderer, animator);
        moveHandler = new DP_MoveHandler(transform, rigidbody, data.Speed, scanner);
    }

    private void Update()
    {
        moveHandler.FixedUpdate();
    }

    #region 플레이어 충돌 처리

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Background_Obj"))
        {
            Debug.Log("배경 오브젝트와 충돌");

            Vector2 collisionDirection = (rigidbody.position - (Vector2)collision.contacts[0].point).normalized;
            rigidbody.AddForce(collisionDirection * 10.0f, ForceMode2D.Impulse);

            moveHandler.isMoving = false;
        }
    }

    #endregion
}
