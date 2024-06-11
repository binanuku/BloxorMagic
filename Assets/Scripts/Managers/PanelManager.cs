using System;
using System.Collections;
using UnityEngine;

public class PanelManager : SingleTon<PanelManager>
{
    GameObject[] invisiblePanel;

    private void Start()
    {
        invisiblePanel = GameObject.FindGameObjectsWithTag("InvisiblePanel");
        for (int i = 0; i < invisiblePanel.Length; i++)
        {
            invisiblePanel[i].SetActive(false);
        }
    }

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

    public void F_MakePanel(Collider col)
    {
        if (col.transform.CompareTag("Interaction"))
        {
            StartCoroutine(F_CreateAnim(invisiblePanel));
        }
    }

    IEnumerator F_CreateAnim(GameObject[] panel) //�г��� ũ�� ��ȭ �ð��� ǥ��
    {
        for (int i = 0; i < panel.Length; i++)
        {
            panel[i].gameObject.SetActive(true);

            Vector3 obj = panel[i].transform.localScale;
            float time = 0;
            float duration = 0.05f;

            if (obj.x > 0.5f)
            {
                while (time < duration)
                {
                    panel[i].transform.localScale = Vector3.Lerp(obj, new Vector3(0, 0f, 0), time / duration); //���������� ũ�� ��ȭ�� ����
                    time += Time.deltaTime; // ��� �ð� ����
                    yield return new WaitForSeconds(0.01f); // ���� �����ӱ��� ���
                }
                panel[i].transform.localScale = new Vector3(0, 0f, 0);
                panel[i].gameObject.SetActive(false);
            }
            else
            {
                panel[i].gameObject.SetActive(true);
                while (time < duration)
                {
                    panel[i].transform.localScale = Vector3.Lerp(obj, new Vector3(1, 0.1f, 1), time / duration);
                    time += Time.deltaTime; // ��� �ð� ����
                    yield return new WaitForSeconds(0.01f); // ���� �����ӱ��� ���
                }
                panel[i].transform.localScale = new Vector3(1, 0.1f, 1);
            }
        }
    }
}
