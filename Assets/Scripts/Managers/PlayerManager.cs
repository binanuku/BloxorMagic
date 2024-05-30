using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public Transform _contactWall; //�÷��̾ ��� ���̰� �浹�� ��
    public Transform _mainCamera; //ī�޶�
    public GameObject InteractionUp; //Panel�� ��ȣ�ۿ��� �ݶ��̴� ������Ʈ
    public GameObject InteractionDown; //Panel�� ��ȣ�ۿ��� �ݶ��̴� ������Ʈ

    #region SingleTon
    public static PlayerManager Instance { get; private set; }
    void Awake()
    {
        // �ߺ��� �ν��Ͻ��� �������� �ʵ��� üũ
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� �ı����� �ʵ��� ����
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
