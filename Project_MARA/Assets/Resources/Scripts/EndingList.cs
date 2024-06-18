using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingList : MonoBehaviour
{
    [SerializeField] private Sprite[] endingBG;
    [SerializeField] private Image bg;
    [SerializeField] private TextMeshProUGUI coment;

    [Header("���� ����")]
    [SerializeField] private AudioClip good;
    [SerializeField] private AudioClip bad;

    private SystemCon system;

    private AudioSource audioSource;

    private void Awake()
    {
        system = GameObject.Find("SystemCon").GetComponent<SystemCon>();

        audioSource = GetComponent<AudioSource>();

        //���� 0��
        if (system.TotalScore >= 0 && system.TotalScore <= 140)
        {
            bg.sprite = endingBG[0];
            coment.text = "��.��.��";
            AudioPlayEnding(bad);
        }

        //���� 1��
        else if (system.TotalScore > 140 && system.TotalScore <= 420)
        {
            bg.sprite = endingBG[1];
            coment.text = "�Ļ� ��û �ϰ�\n���� ���̴� ��~";
            AudioPlayEnding(bad);
        }

        //���� 2��
        else if (system.TotalScore > 420 && system.TotalScore <= 840)
        {
            bg.sprite = endingBG[2];
            coment.text = "���Ϻ��� ������ ���� ��~";
            AudioPlayEnding(bad);
        }

        //���� 3��
        else if (system.TotalScore > 840 && system.TotalScore <= 1260)
        {
            bg.sprite = endingBG[3];
            coment.text = "�������� �϶���...";
            AudioPlayEnding(bad);
        }

        //���� 4��
        else if (system.TotalScore > 1260 && system.TotalScore <= 1820)
        {
            bg.sprite = endingBG[4];
            coment.text = "���� �� �̷����ڶ� ��~";
            AudioPlayEnding(good);
        }

        //���� 5��
        else if (system.TotalScore > 1820 && system.TotalScore <= 2240)
        {
            bg.sprite = endingBG[5];
            coment.text = "�� ����־� �׷��Ը� ��~";
            AudioPlayEnding(good);
        }
    }

    private void AudioPlayEnding(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
