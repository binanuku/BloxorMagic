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
                // ���� �ν��Ͻ��� ������ ���ο� �ν��Ͻ� ����
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

    public Transform _contactWall; //�÷��̾ ��� ���̰� �浹�� ��
    public Transform _mainCamera; //ī�޶�
    public GameObject InteractionUp; //Panel�� ��ȣ�ۿ��� �ݶ��̴� ������Ʈ
    public GameObject InteractionDown; //Panel�� ��ȣ�ۿ��� �ݶ��̴� ������Ʈ

    void Awake()
    {
        // �ߺ��� �ν��Ͻ��� �������� �ʵ��� üũ
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� �ı����� �ʵ��� ����
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
}
