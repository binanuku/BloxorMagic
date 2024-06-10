using UnityEngine;

public class PanelManager : MonoBehaviour
{
    #region SingleTon
    public static PanelManager Instance { get; private set; }

    private void Awake()
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

    public void F_Interaction(Collider col, Rigidbody rb) {
        if (col.transform.CompareTag("Interaction"))
        {
            rb.isKinematic = false;
            //�гο� �÷��̾ �ε����� ������
        }
    }
    public void F_GlassActive(Collider col, Rigidbody rb)
    {
        F_Interaction(col, rb);
        StartCoroutine(UIManager.Instance.F_OnDeadUI());
    }

    public void F_CreatePanel(Collision col, Rigidbody rb)
    {
        if (col.transform.CompareTag("Interaction"))
        {

        }
    }
}
