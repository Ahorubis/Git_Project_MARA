using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MaraList))]
public class PlayCon : MonoBehaviour
{
    [Header("���� ǥ��")]
    [SerializeField] private float leftDistanceMin = 82;
    [SerializeField] private float leftDistanceMax = 368;
    [SerializeField] private float rightDistanceMin = 200;
    [SerializeField] private float rightDistanceMax = 350;
    public Transform[] gizmos;

    [Header("�ý��� ���� UI �� ����")]
    [SerializeField] private TextMeshProUGUI minuteInterface;   //���ӽð� �� ǥ��
    [SerializeField] private TextMeshProUGUI secondInterface;   //���ӽð� �� ǥ��
    public float timerSecond = 0;                               //��
    public int timerMinute = 6;                                 //��

    [Header("������ ����â")]
    [SerializeField] private TextMeshProUGUI chefComentText;
    [SerializeField] private RectTransform tablePosition;
    [SerializeField] private RectTransform menuRotation;
    [SerializeField] private RectTransform leftPotPosition;
    [SerializeField] private float rotationSpeed = 50f;
    public GameObject nextButton;
    public GameObject chefComentBox;
    public float settingTimer = 2f;

    [Header("�ֹ����� UI ����")]
    [SerializeField] private TextMeshProUGUI[] OrderContents;
    [SerializeField] private TextMeshProUGUI[] OrderGram;
    [SerializeField] private TextMeshProUGUI balanceTotal;
    [SerializeField] private TextMeshProUGUI spicyPhase;
    [SerializeField] private TextMeshProUGUI sauce;

    [Header("���� ����")]
    [SerializeField] private GameObject[] orderPapers;      //�ֹ��� ���
    [SerializeField] private GameObject orderPaperPrint;    //�ֹ��� ����
    [SerializeField] private GameObject visitorComentBox;   //�մ� �� ��ǳ��
    [SerializeField] private GameObject visitor;            //�մ�
    public TextMeshProUGUI visitorComent;                   //�մ� ��
    public float[] OrderTimerList;                          //�ֹ� �ð� ����

    [Header("����")]
    [SerializeField] private RectTransform pot;                 //��鸱 ����
    [SerializeField] private Image boiledPotImage;              //���� ����
    [SerializeField] private AudioSource boiledAudioSource;
    [SerializeField] private float shakeSize = 36;              //�����Ը�
    [SerializeField] private float shakeTime = 1;               //Ÿ�̸�
    public Image[] bowlMara;                                    //���� ���� ��� �̹���
    public Sprite[] potImage;                                   //��� ���� �׷�
    public Image resultPot;                                     //���� �̹���

    [Header("���� ����")]
    [SerializeField] private AudioClip printAudio;
    [SerializeField] private AudioClip boiledAudio;

    private MaraList maraList;
    private SystemCon systemCon;

    private RectTransform resetTable;           //�丮 ���̺� ������ġ
    private RectTransform resetLeftPot;         //���� ���� ������ġ
    private RectTransform resetRightPot;        //������ ���� ������ġ
    private RectTransform visitorTransform;     //�մ�

    private Animator orderAnimeCon;        //�ֹ��� �ִϸ��̼�
    private Image visitorImage;            //�մ� �̹���
    private AudioSource audioSource;
    private AudioSource printAudioSource;

    private List<int> CharaIndex = new List<int>();                 //ĳ���� ���� ���� ����Ʈ

    private int[] CharaIndexGroup = new int[8];

    private bool movingTable = false;   //���̺� ���� ����
    private bool shakingPot = false;    //����ȿ�� ���� ����
    private bool visitAnime = false;    //�մ� ���� �� ���� ����

    private float startingPoint;        //���̺� ���� x��ǥ
    private float speedMultiply = 1;    //ȸ���ӵ� ���
    private float cookTimer = 0;        //�ֹ� ����� �ð�
    private float orderTimer = 0;       //�ֹ��� ����Ǵ� �ð�

    private int menuPerfect = 0;        //�ش� �ֹ��� ����
    private int visitorIndex = 0;       //�մ� ����

    [HideInInspector] public List<string> ChoiceIngredients = new List<string>();    //������ ���
    [HideInInspector] public List<string> ChoiceSauce = new List<string>();          //������ �ҽ�
    [HideInInspector] public List<string> MenuIngredients = new List<string>();      //������ ���
    [HideInInspector] public List<string> MenuSauce = new List<string>();            //������ �ҽ�

    [HideInInspector] public OrderCon[] OrderCon = new OrderCon[5];

    [HideInInspector] public Vector2 innerVisitor;         //�մ� �� ��ġ
    [HideInInspector] public Vector2 outerVisitor;        //�մ� ���� ��ġ

    [HideInInspector] public int ingredientWeight = 0;      //������� ��� ����
    [HideInInspector] public int cookIndex = 0;             //�丮 �ܰ�(0 : ����, 1 : ��� ���� ���̺�, 2 : �ҽ� �� �ʱ� ���� ���̺�)
    [HideInInspector] public int choiceSpicy = 0;           //������ �ʱ� ����
    [HideInInspector] public int orderPapernumber = 0;      //������ �ֹ��� ��ȣ
    [HideInInspector] public int doneMara = 0;              //������ �ܰ�
    [HideInInspector] public int menuSpicy = 0;             //������ �ʱ� ����

    [HideInInspector] public bool[] OrderPaperOn = new bool[5];

    [HideInInspector] public bool PaperPrint = false;

    private void Awake()
    {
        orderAnimeCon = orderPaperPrint.GetComponent<Animator>();
        printAudioSource = orderPaperPrint.GetComponent<AudioSource>();


        //�ֹ��� ���� ���� �ʱ�ȭ
        for (int i = 0; i < OrderCon.Length; i++)
        {
            OrderCon[i] = orderPapers[i].GetComponent<OrderCon>();
            OrderCon[i].ThisIndex = i;
        }

        systemCon = GameObject.Find("SystemCon").GetComponent<SystemCon>();

        visitorImage = visitor.GetComponent<Image>();
        visitorTransform = visitor.GetComponent<RectTransform>();
        audioSource = GetComponent<AudioSource>();
        maraList = GetComponent<MaraList>();

        //������Ʈ ��Ȱ��ȭ
        visitorComentBox.SetActive(false);
        nextButton.SetActive(false);
        chefComentBox.SetActive(false);
        visitorComentBox.SetActive(true);

        //�丮���̺� ��ġ �ʱ�ȭ
        tablePosition.anchoredPosition = new Vector2(2880, 0);

        //���� �ʱ�ȭ
        systemCon.TotalScore = 0;

        resetTable = tablePosition;
        resetLeftPot = leftPotPosition;

        startingPoint = tablePosition.anchoredPosition.x;

        balanceTotal.text = "0";

        //���� ��� �̹��� �ʱ�ȭ
        for (int i = 0; i < bowlMara.Length; i++)
        {
            bowlMara[i].sprite = null;
            bowlMara[i].color = new Color(1, 1, 1, 0);
        }

        OrderPaperOn = new bool[5] { false, false, false, false, false };

        RandomCharaIndex(0, CharaIndexGroup.Length);
    }

    private void Start()
    {
        visitorImage.sprite = maraList.VisitorGroup[CharaIndexGroup[systemCon.VisitorIndex]][0];
        innerVisitor = new Vector2(-700, 166);
        outerVisitor = innerVisitor + new Vector2(-500, 0);

        visitorTransform.anchoredPosition = innerVisitor;

        resultPot.sprite = potImage[0];

        visitorComent.text = ConsumerComent(0);

        StartCoroutine(OrderPaperAnime(0));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) systemCon.GameOff();

        if (ingredientWeight == 0) balanceTotal.text = "0";
        else balanceTotal.text = $"{ingredientWeight}00";

        RotationCircleMenu();
        GameTime();

        //�丮���� ���ѽð�
        if (cookTimer > 0) cookTimer -= Time.deltaTime;
        else if (cookIndex != 0 && cookTimer <= 0)
        {
            cookTimer = 0;
            systemCon.VisitorIndex++;

            StartCoroutine(VisitorMoving(innerVisitor, outerVisitor));

            if (!movingTable) StartCoroutine(TableMove(tablePosition.anchoredPosition));
        }
    }

    //���ȸ���� ȸ�� �Լ�
    private void RotationCircleMenu()
    {
        Vector3 angles = menuRotation.eulerAngles;

        if (Input.GetKeyDown(KeyCode.R)) speedMultiply++;   //ȸ���ӵ��� �����

        if (speedMultiply > 9) speedMultiply = 1;   //���� �ܰ踦 �Ѿ�� �ӵ� �ʱ�ȭ

        angles.z = angles.z - rotationSpeed * speedMultiply * Time.deltaTime;
        menuRotation.eulerAngles = angles;
    }

    //�ʱ� ���� ��� �Լ�
    private void Spicy(int num)
    {
        //index Ư����ȣ ���� : 2, 3, 5, 8 ~ 10, 13, 15, 17, 21 ~ 25, 29
        if (num == 2 || num == 23)                      spicyPhase.text = "�Ŷ�� �ʱ�� ���ּ���";
        else if (num == 3 || num == 29)                 spicyPhase.text = "�� �ʰ� ���ּ���";
        else if (num == 5)                              spicyPhase.text = "������ Ż ��ŭ �ʰ� ���ּ���";
        else if (num == 8)                              spicyPhase.text = "������ �� �ʰ� ���ּ���";
        else if (num == 9)                              spicyPhase.text = "���� �־��ּ���";
        else if (num == 10)                             spicyPhase.text = "�԰� ���� �� �Ͼ��ŭ\n�ʰ� ���ּ���";
        else if (num == 13)                             spicyPhase.text = "��������� ������ּ���";
        else if (num == 15 || num == 21 || num == 22)   spicyPhase.text = "������ ���� ����������\n�ʰ� ���ּ���";
        else if (num == 17)                             spicyPhase.text = "ȫ��õ�� �ſ� �����麸��\n�� �ʰ� ���ּ���";
        else if (num == 24)                             spicyPhase.text = "�԰� ������ŭ �ʰ� ���ּ���";
        else if (num == 25)                             spicyPhase.text = "�� �巡����� ���̿���";

        //������ ����� �ʱ� ǥ��
        else spicyPhase.text = "�ʱ�� " + maraList.MaraSpicy[num] + "�ܰ�� ���ּ���";
    }

    //���� ��ü�ð� �帧
    private void GameTime()
    {
        minuteInterface.text = $"{timerMinute:D2}";

        if ((int)timerSecond == 60) secondInterface.text = "00";
        else secondInterface.text = $"{(int)timerSecond:D2}";

        timerSecond -= Time.deltaTime;

        if (timerSecond < 0)
        {
            timerMinute--;

            if (timerMinute < 0) systemCon.NextScene("EndingScene");

            timerSecond = 60;
        }
    }

    //ĳ���� ������� �ߺ����� ����
    private void RandomCharaIndex(int min, int max)
    {
        for (int i = 0; i < max;)
        {
            int temp = Random.Range(min, max);

            if (CharaIndex.Contains(temp)) temp = Random.Range(min, max);
            else
            {
                CharaIndex.Add(temp);
                i++;
            }
        }

        CharaIndexGroup = CharaIndex.ToArray();
    }

    //���� �˸���
    private IEnumerator TimeComent()
    {
        yield return new WaitForSeconds(45);

        chefComentBox.SetActive(true);

        int index = Random.Range(0, maraList.ChefComent.Length);

        chefComentText.text = maraList.ChefComent[index];

        yield return new WaitForSeconds(15);

        StartCoroutine(TableMove(tablePosition.anchoredPosition));
        cookIndex = 2;
        chefComentBox.SetActive(false);

        ChoiceIngredients.Clear();      //�޴� ��� �ʱ�ȭ
        ChoiceSauce.Clear();            //�޴� �ҽ� �ʱ�ȭ

        MenuIngredients.Clear();
        MenuSauce.Clear();

        choiceSpicy = 0;                //�޴� �ʱ� �ʱ�ȭ
        doneMara = 0;                   //���̱� �ʱ�ȭ
        menuSpicy = 0;
    }

    //�ֹ��� ���� �� ���λ��� ����
    public void OrderRandom(int choiceIndex)
    {
        if (movingTable || OrderCon[choiceIndex].OrderMoving) return;
        int index = Random.Range(0, 30);

        string[] TempIngredients = maraList.MaraIngredients[index];     //��� ��� �ӽù迭
        string[] TempSauce = maraList.MaraSauce[index];                 //�ҽ� ��� �ӽù迭
        
        int[] TempWeight = maraList.MaraWeight[index];      //���� ��� �ӽù迭

        orderPapernumber = choiceIndex;

        //���� �� �޴� å�� UI
        for (int i = 0; i < 10; i++)
        {
            if (i < TempIngredients.Length)
            {
                OrderContents[i].text = TempIngredients[i];
                OrderGram[i].text = $"{TempWeight[i]}00";
            }

            else
            {
                OrderContents[i].text = null;
                OrderGram[i].text = null;
            }
        }

        //��� ����Ʈ ����
        for (int i = 0; i < TempIngredients.Length; i++) for (int j = 0; j < TempWeight[i]; j++)
        {
            MenuIngredients.Add(TempIngredients[i]);
        }

        //�ʱ� �ܰ� ����
        menuSpicy = maraList.MaraSpicy[index];

        Spicy(index);

        //�ҽ� ����
        for (int i = 0; i < TempSauce.Length; i++) MenuSauce.Add(TempSauce[i]);

        if (TempSauce[0] != TempSauce[1])      sauce.text = $"{TempSauce[0]} 1��, \n{TempSauce[1]} 1��";
        else sauce.text = $"{TempSauce[0]} 2��";

        //�ֹ��� ���� ���� ���̺� ���� �� �� �ֵ��� ���ǹ� �ۼ�
        if (cookIndex != 0) return;
        ingredientWeight = 0;
        cookTimer = 60;

        StartCoroutine(TableMove(tablePosition.anchoredPosition));
    }

    //��� ����
    public void MenuChoice(string menu)
    {
        //���� �� �ִ� ��� ���
        string[] menuGroup = new string[13]
        {
            "���", "���̹���", "����", "�и���", "�񿣳�", "����",
            "������", "����", "�縻", "������", "û��ä", "���̹���", "Ǫ��"
        };

        int menuIndex = 0;

        for (int i = 0; i < menuGroup.Length; i++)
        {
            if (menuGroup[i] == menu)
            {
                menuIndex = i;
                break;
            }
        }

        if (ingredientWeight < 10)
        {
            nextButton.SetActive(false);
            ChoiceIngredients.Add(menu);
            bowlMara[ingredientWeight].sprite = maraList.BowlIngredients[menuIndex];
            bowlMara[ingredientWeight].color = new Color(1, 1, 1, 1);
            ingredientWeight++;

            if (ingredientWeight == 10) nextButton.SetActive(true);
        }

        else if (!shakingPot && ingredientWeight >= 10) StartCoroutine(PotShake(shakeSize, shakeTime));

        balanceTotal.text = $"{ingredientWeight}00";
    }

    //�ʱ� ���� ����
    public void SpicyAmount(int phase)
    {
        if (choiceSpicy != 0) return;
        else
        {
            choiceSpicy = phase;
            doneMara++;
            StartCoroutine("CookMaraBoiled");
        }
    }

    //�ҽ� ����
    public void SauceName(string sauce)
    {
        if (ChoiceSauce.Count >= 2) return;
        else
        {
            ChoiceSauce.Add(sauce);
            doneMara++;
            StartCoroutine("CookMaraBoiled");
        }
    }

    //��ư���� ���̺� �۵�
    public void NextMoving()
    {
        StartCoroutine(TableMove(tablePosition.anchoredPosition));
    }

    //�մ��� ��
    public void ConsumerStar()
    {
        if (menuPerfect >= 120)
        {
            visitorImage.sprite = maraList.VisitorGroup[CharaIndexGroup[visitorIndex]][4];
            visitorComent.text = ConsumerComent(5);
        }
        else if (menuPerfect >= 90)
        {
            visitorImage.sprite = maraList.VisitorGroup[CharaIndexGroup[visitorIndex]][3];
            visitorComent.text = ConsumerComent(4);
        }
        else if (menuPerfect >= 50)
        {
            visitorImage.sprite = maraList.VisitorGroup[CharaIndexGroup[visitorIndex]][0];
            visitorComent.text = ConsumerComent(3);
        }
        else if (menuPerfect >= 20)
        {
            visitorImage.sprite = maraList.VisitorGroup[CharaIndexGroup[visitorIndex]][2];
            visitorComent.text = ConsumerComent(2);
        }
        else if (menuPerfect >= 0)
        {
            visitorImage.sprite = maraList.VisitorGroup[CharaIndexGroup[visitorIndex]][1];
            visitorComent.text = ConsumerComent(1);
        }
    }

    //�ֹ��� ��� ����
    public float OrderPaperTimer()
    {
        float alarm;

        if (timerMinute == 6 || timerMinute == 5) alarm = OrderTimerList[0];        //5�д�, 40s

        else if (timerMinute == 4)
        {
            if ((int)timerSecond >= 40) alarm = OrderTimerList[0];      //4�� 40�� �̻�, 40s
            else alarm = OrderTimerList[1];                             //4�� 39�� ����, 30s
        }

        else if (timerMinute == 3)
        {
            if ((int)timerSecond >= 10) alarm = OrderTimerList[1];      //3�� 10�� �̻�, 30s
            else alarm = OrderTimerList[2];                             //3�� 9�� ����, 25s
        }

        else if (timerMinute == 2) alarm = OrderTimerList[2];       //2�д�, 25s

        else if (timerMinute == 1)
        {
            if ((int)timerSecond >= 55) alarm = OrderTimerList[2];      //1�� 55�� �̻�, 25s
            else alarm = OrderTimerList[3];                             //1�� 54�� ����, 20s
        }

        else
        {
            if ((int)timerSecond >= 55) alarm = OrderTimerList[3];          //55�� �̻�, 20s
            else if ((int)timerSecond >= 10) alarm = OrderTimerList[4];     //10�� �̻�, 15s
            else alarm = OrderTimerList[5];                                 //������, 10s
        }

        return alarm;
    }

    //���� ���
    private int TotalScoreCalculate()
    {
        int calculate = 0;

        MenuIngredients.Sort();         //������ ��� ��� �������� ����
        ChoiceIngredients.Sort();       //������ ��� ��� �������� ����

        MenuSauce.Sort();       //������ �ҽ� ��� �������� ����
        ChoiceSauce.Sort();     //������ �ҽ� ��� �������� ����

        //��� ����� �ߺ����� ������ ����, �ش� ����Ʈ�� ��� ������ŭ ���� �߰�(���� +10��)
        List<string> IngredientsList = new List<string>();
        for (int i = 0; i < MenuIngredients.Count; i++) if (MenuIngredients[i] == ChoiceIngredients[i]) IngredientsList.Add(MenuIngredients[i]);

        calculate += IngredientsList.Count * 10;

        //�ҽ� ����� �ߺ����� ������ ����, �ش� ����Ʈ�� ��� ������ŭ ���� �߰�(���� +10��)
        List<string> SauceList = new List<string>();
        for (int i = 0; i < MenuSauce.Count; i++) if (MenuSauce[i] == ChoiceSauce[i]) SauceList.Add(MenuSauce[i]);
        calculate += SauceList.Count * 10;

        //�ʱ�ܰ� ���� ���, +20��
        if (choiceSpicy == menuSpicy) calculate += 20;

        menuPerfect = calculate;

        return systemCon.TotalScore += calculate;
    }

    //ĳ���� ���
    private string ConsumerComent(int index)
    {
        string coment;

        coment = maraList.ConsumerComent[CharaIndexGroup[visitorIndex]][index];

        return coment;
    }

    //������Ʈ ����
    private IEnumerator PotShake(float size, float alarm)
    {
        shakingPot = true;

        float timer = 0;
        Vector2 start = pot.anchoredPosition;

        while (timer < alarm)
        {
            pot.anchoredPosition = start + Random.insideUnitCircle * size;
            timer += Time.deltaTime;
            yield return null;
        }

        pot.anchoredPosition = start;
        shakingPot = false;
    }

    //�մ� ���� �� ����
    public IEnumerator VisitorMoving(Vector2 start, Vector2 end)
    {
        float timer = 0;

        visitAnime = true;

        if (start == innerVisitor) visitorComentBox.SetActive(false);

        while (timer < settingTimer)
        {
            float speed = timer * settingTimer;

            timer += Time.deltaTime;
            visitorTransform.anchoredPosition = Vector2.Lerp(start, end, speed);
            yield return null;

            if (timer > 0.99f * settingTimer)
            {
                visitorTransform.anchoredPosition = end;

                if (visitorTransform.anchoredPosition == innerVisitor)
                {
                    visitorComent.text = ConsumerComent(0);
                    visitorComentBox.SetActive(true);
                }

                else if (visitorTransform.anchoredPosition == outerVisitor)
                {
                    visitorIndex++;
                    visitorIndex %= CharaIndexGroup.Length;

                    visitorImage.sprite = maraList.VisitorGroup[CharaIndexGroup[visitorIndex]][0];
                    StartCoroutine(VisitorMoving(outerVisitor, innerVisitor));
                }

                visitAnime = false;

                break;
            }
        }
    }

    //������ ���̱�
    private IEnumerator CookMaraBoiled()
    {
        float timer = 2;
        float setting = timer;

        if (doneMara == 3)
        {
            boiledAudioSource.PlayOneShot(boiledAudio);

            while (timer > 0)
            {
                boiledPotImage.fillAmount = Mathf.Lerp(0, 1, timer / setting);
                timer -= Time.deltaTime;
                yield return null;
            }

            yield return null;
            boiledAudioSource.Stop();
            boiledPotImage.fillAmount = 0;

            TotalScoreCalculate();

            if (menuPerfect >= 60) resultPot.sprite = potImage[1];        //�� ������� ���
            else if (menuPerfect < 60) resultPot.sprite = potImage[2];    //�� ������� ���
        }

        else
        {
            resultPot.sprite = potImage[0];
            yield return null;
        }
    }

    //���̺� �̵�
    public IEnumerator TableMove(Vector2 start)
    {
        float timer = 0;
        movingTable = true;

        if (cookIndex == 0) StartCoroutine("TimeComent");

        cookIndex++;
        if (cookIndex > 2)
        {
            cookIndex = 0;
            StopCoroutine("TimeComent");
        }

        Vector2 end = new Vector2(startingPoint - (1920 * cookIndex), 0);

        while (timer < settingTimer)
        {
            float realTime = timer * settingTimer;

            yield return null;
            timer += Time.deltaTime;
            tablePosition.anchoredPosition = Vector2.Lerp(start, end, realTime);

            if (realTime > 0.99f)
            {
                tablePosition.anchoredPosition = end;
                movingTable = false;
                break;
            }
        }
    }

    //�ֹ��� ���� �� ����
    public IEnumerator OrderPaperAnime(int index)
    {
        orderAnimeCon.SetTrigger("Order");
        printAudioSource.PlayOneShot(printAudio);
        PaperPrint = true;

        yield return new WaitForSeconds(1);
        OrderCon[index].StartCoroutine(OrderCon[index].OrderAnime(OrderCon[index].underPosition, OrderCon[index].upperPosition));
        OrderCon[index].OrderReceive = true;

        for (int i = 0; i < OrderCon.Length; i++)
        {
            if (OrderCon[i].TimerThis <= 0 && !OrderCon[i].OrderReceive)
            {
                OrderCon[i].TimerThis = OrderPaperTimer();
                continue;
            }
        }

        if (OrderCon[0].OrderReceive && OrderCon[1].OrderReceive && OrderCon[2].OrderReceive
        && OrderCon[3].OrderReceive && OrderCon[4].OrderReceive)
        {
            OrderCon[orderPapernumber].OrderReceive = false;
            OrderCon[orderPapernumber].TimerThis = 5;
        }

        PaperPrint = false;
    }
}
