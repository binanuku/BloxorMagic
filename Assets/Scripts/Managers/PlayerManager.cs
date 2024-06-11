using UnityEngine;

public class PlayerManager : SingleTon<PlayerManager>
{
    public Transform _contactWall; //플레이어가 쏘는 레이가 충돌할 벽
    public Transform _mainCamera; //카메라
    public GameObject InteractionUp; //Panel에 상호작용할 콜라이더 오브젝트
    public GameObject InteractionDown; //Panel에 상호작용할 콜라이더 오브젝트
}
