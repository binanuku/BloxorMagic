using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public Transform _contactWall; //플레이어가 쏘는 레이가 충돌할 벽
    public Transform _mainCamera; //카메라
    public GameObject InteractionUp; //Panel에 상호작용할 콜라이더 오브젝트
    public GameObject InteractionDown; //Panel에 상호작용할 콜라이더 오브젝트

    #region SingleTon
    public static PlayerManager Instance { get; private set; }
    void Awake()
    {
        // 중복된 인스턴스가 생성되지 않도록 체크
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 파괴되지 않도록 설정
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
