using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemCon : MonoBehaviour
{
    public static SystemCon System;

    [SerializeField] private AudioClip buttonClick;

    [HideInInspector] public int TotalScore = 0;        //�� ���� �հ�
    [HideInInspector] public int VisitorIndex = 0;      //�湮�� �մ� ��
    [HideInInspector] public int OrderIndex = 0;

    private AudioSource audioSource;

    private void Awake()
    {
        if (System != null)
        {
            Destroy(gameObject);
            return;
        }

        else
        {
            System = this;
            DontDestroyOnLoad(gameObject);
        }

        VisitorIndex = 0;
    }

    //��ư Ŭ��
    public void ButtonClick()
    {
        audioSource = GameObject.Find("Canvas").GetComponent<AudioSource>();
        audioSource.PlayOneShot(buttonClick);
    }

    //�� ��ȯ
    public void NextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //��������
    public void GameOff()
    {
        Application.Quit();
    }
}
