using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingList : MonoBehaviour
{
    [SerializeField] private Sprite[] endingBG;
    [SerializeField] private Image bg;
    [SerializeField] private TextMeshProUGUI coment;

    [Header("엔딩 음악")]
    [SerializeField] private AudioClip good;
    [SerializeField] private AudioClip bad;

    private SystemCon system;

    private AudioSource audioSource;

    private void Awake()
    {
        system = GameObject.Find("SystemCon").GetComponent<SystemCon>();

        audioSource = GetComponent<AudioSource>();

        //별점 0점
        if (system.TotalScore >= 0 && system.TotalScore <= 140)
        {
            bg.sprite = endingBG[0];
            coment.text = "버.러.지";
            AudioPlayEnding(bad);
        }

        //별점 1점
        else if (system.TotalScore > 140 && system.TotalScore <= 420)
        {
            bg.sprite = endingBG[1];
            coment.text = "파산 신청 하고\n오는 길이다 해~";
            AudioPlayEnding(bad);
        }

        //별점 2점
        else if (system.TotalScore > 420 && system.TotalScore <= 840)
        {
            bg.sprite = endingBG[2];
            coment.text = "내일부터 나오지 마라 해~";
            AudioPlayEnding(bad);
        }

        //별점 3점
        else if (system.TotalScore > 840 && system.TotalScore <= 1260)
        {
            bg.sprite = endingBG[3];
            coment.text = "설거지나 하라해...";
            AudioPlayEnding(bad);
        }

        //별점 4점
        else if (system.TotalScore > 1260 && system.TotalScore <= 1820)
        {
            bg.sprite = endingBG[4];
            coment.text = "역시 내 미래제자라 해~";
            AudioPlayEnding(good);
        }

        //별점 5점
        else if (system.TotalScore > 1820 && system.TotalScore <= 2240)
        {
            bg.sprite = endingBG[5];
            coment.text = "너 재능있어 그렇게만 해~";
            AudioPlayEnding(good);
        }
    }

    private void AudioPlayEnding(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
