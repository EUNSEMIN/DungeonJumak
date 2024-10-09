// Engine
using UnityEngine;

public class Mo_MoveHandler
{
    #region Variables

    private float speed; 
    private Transform monsterTransform; 
    private Transform playerTransform;

    #endregion

    public Mo_MoveHandler(Transform _monsterTransform, Transform _playerTransform, float _speed)
    {
        this.monsterTransform = _monsterTransform;
        this.playerTransform = _playerTransform;
        this.speed = _speed;
    }

    public void FixedUpdate()
    {
        Moving();
    }

    public void Moving()
    {
        if (playerTransform != null)
        {
            monsterTransform.position = Vector3.MoveTowards(
                monsterTransform.position,      // 몬스터의 현재 위치
                playerTransform.position,       // 플레이어의 위치
                speed * Time.deltaTime          // 속도
            );
        }
    }


    //public void StopMoving()
    //{
    //    // 몬스터의 이동을 멈추는 로직
    //}

}
