using UnityEngine;

public class PanelManager : MonoBehaviour
{
    #region SingleTon
    public static PanelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // ���� �ν��Ͻ��� ������ ���ο� �ν��Ͻ� ����
                _instance = FindObjectOfType<PanelManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<PanelManager>();
                    singletonObject.name = typeof(PlayerManager).ToString() + " (Singleton)";
                }
            }
            return _instance;
        }
    }
    #endregion

    private static PanelManager _instance;

    private void Awake()
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

    public void F_Interaction(Collider col, Rigidbody rb) {
        if (col.transform.CompareTag("Interaction"))
        {
            rb.isKinematic = false;
            //�гο� �÷��̾ �ε����� ������
        }
    }
}
