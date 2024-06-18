using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemCon : MonoBehaviour
{
    public static SystemCon System;

    [SerializeField] private AudioClip buttonClick;

    [HideInInspector] public int TotalScore = 0;        //총 별점 합계
    [HideInInspector] public int VisitorIndex = 0;      //방문한 손님 수
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

    //버튼 클릭
    public void ButtonClick()
    {
        audioSource = GameObject.Find("Canvas").GetComponent<AudioSource>();
        audioSource.PlayOneShot(buttonClick);
    }

    //씬 전환
    public void NextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //게임종료
    public void GameOff()
    {
        Application.Quit();
    }
}
