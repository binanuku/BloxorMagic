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
            //패널에 플레이어가 부딪히면 떨어짐
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

    IEnumerator F_CreateAnim(GameObject[] panel) //패널의 크기 변화 시각적 표시
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
                    panel[i].transform.localScale = Vector3.Lerp(obj, new Vector3(0, 0f, 0), time / duration); //점차적으로 크기 변화가 보임
                    time += Time.deltaTime; // 경과 시간 갱신
                    yield return new WaitForSeconds(0.01f); // 다음 프레임까지 대기
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
                    time += Time.deltaTime; // 경과 시간 갱신
                    yield return new WaitForSeconds(0.01f); // 다음 프레임까지 대기
                }
                panel[i].transform.localScale = new Vector3(1, 0.1f, 1);
            }
        }
    }
}
