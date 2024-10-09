//Engine
using UnityEngine;

//Interface
using Interfaces;

//Ect
using Data.Character;

public class Monster : MonoBehaviour, IDamageable, ITurnable, IMovable
{
    #region

    // SO
    public MonsterDataSO data;

    // 플레이어 transform
    [SerializeField] private Transform playerTransform;

    // 핸들러
    private Mo_AnimationHandler animationHandler;
    private Mo_MoveHandler moveHandler;

    // 컴포넌트
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    #endregion

    private void Awake()
    {
        //--- 컴포넌트 할당 ---//
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //--- 핸들러 인스턴스 생성 ---//
        animationHandler = new Mo_AnimationHandler(spriteRenderer, animator);
        moveHandler = new Mo_MoveHandler(transform, playerTransform, data.Speed);
    }

    private void Update()
    {
        moveHandler.FixedUpdate();
    }
}
