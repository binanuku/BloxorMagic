using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region SingleTon
    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // 기존 인스턴스가 없으면 새로운 인스턴스 생성
                _instance = FindObjectOfType<PlayerManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<PlayerManager>();
                    singletonObject.name = typeof(PlayerManager).ToString() + " (Singleton)";
                }
            }
            return _instance;
        }
    }

    private static PlayerManager _instance;
    #endregion

    public Transform _contactWall; //플레이어가 쏘는 레이가 충돌할 벽
    public Transform _mainCamera; //카메라
    public GameObject InteractionUp; //Panel에 상호작용할 콜라이더 오브젝트
    public GameObject InteractionDown; //Panel에 상호작용할 콜라이더 오브젝트

    void Awake()
    {
        // 중복된 인스턴스가 생성되지 않도록 체크
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 파괴되지 않도록 설정
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
}
