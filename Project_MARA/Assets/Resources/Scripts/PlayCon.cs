using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MaraList))]
public class PlayCon : MonoBehaviour
{
    [Header("영역 표시")]
    [SerializeField] private float leftDistanceMin = 82;
    [SerializeField] private float leftDistanceMax = 368;
    [SerializeField] private float rightDistanceMin = 200;
    [SerializeField] private float rightDistanceMax = 350;
    public Transform[] gizmos;

    [Header("시스템 관련 UI 및 설정")]
    [SerializeField] private TextMeshProUGUI minuteInterface;   //게임시간 분 표시
    [SerializeField] private TextMeshProUGUI secondInterface;   //게임시간 초 표시
    public float timerSecond = 0;                               //초
    public int timerMinute = 6;                                 //분

    [Header("마라탕 제조창")]
    [SerializeField] private TextMeshProUGUI chefComentText;
    [SerializeField] private RectTransform tablePosition;
    [SerializeField] private RectTransform menuRotation;
    [SerializeField] private RectTransform leftPotPosition;
    [SerializeField] private float rotationSpeed = 50f;
    public GameObject nextButton;
    public GameObject chefComentBox;
    public float settingTimer = 2f;

    [Header("주문내역 UI 설정")]
    [SerializeField] private TextMeshProUGUI[] OrderContents;
    [SerializeField] private TextMeshProUGUI[] OrderGram;
    [SerializeField] private TextMeshProUGUI balanceTotal;
    [SerializeField] private TextMeshProUGUI spicyPhase;
    [SerializeField] private TextMeshProUGUI sauce;

    [Header("계산대 관련")]
    [SerializeField] private GameObject[] orderPapers;      //주문지 목록
    [SerializeField] private GameObject orderPaperPrint;    //주문지 발행
    [SerializeField] private GameObject visitorComentBox;   //손님 평가 말풍선
    [SerializeField] private GameObject visitor;            //손님
    public TextMeshProUGUI visitorComent;                   //손님 평가
    public float[] OrderTimerList;                          //주문 시간 구간

    [Header("냄비")]
    [SerializeField] private RectTransform pot;                 //흔들릴 냄비
    [SerializeField] private Image boiledPotImage;              //끓일 냄비
    [SerializeField] private AudioSource boiledAudioSource;
    [SerializeField] private float shakeSize = 36;              //지진규모
    [SerializeField] private float shakeTime = 1;               //타이머
    public Image[] bowlMara;                                    //냄비에 들어가는 재료 이미지
    public Sprite[] potImage;                                   //결과 냄비 그룹
    public Image resultPot;                                     //냄비 이미지

    [Header("음악 모음")]
    [SerializeField] private AudioClip printAudio;
    [SerializeField] private AudioClip boiledAudio;

    private MaraList maraList;
    private SystemCon systemCon;

    private RectTransform resetTable;           //요리 테이블 원래위치
    private RectTransform resetLeftPot;         //왼쪽 냄비 원래위치
    private RectTransform resetRightPot;        //오른쪽 냄비 원래위치
    private RectTransform visitorTransform;     //손님

    private Animator orderAnimeCon;        //주문지 애니메이션
    private Image visitorImage;            //손님 이미지
    private AudioSource audioSource;
    private AudioSource printAudioSource;

    private List<int> CharaIndex = new List<int>();                 //캐릭터 등장 순서 리스트

    private int[] CharaIndexGroup = new int[8];

    private bool movingTable = false;   //테이블 동작 판정
    private bool shakingPot = false;    //지진효과 동작 판정
    private bool visitAnime = false;    //손님 등장 및 퇴장 판정

    private float startingPoint;        //테이블 시작 x좌표
    private float speedMultiply = 1;    //회전속도 배수
    private float cookTimer = 0;        //주문 만드는 시간
    private float orderTimer = 0;       //주문지 발행되는 시간

    private int menuPerfect = 0;        //해당 주문의 점수
    private int visitorIndex = 0;       //손님 순서

    [HideInInspector] public List<string> ChoiceIngredients = new List<string>();    //선택한 재료
    [HideInInspector] public List<string> ChoiceSauce = new List<string>();          //선택한 소스
    [HideInInspector] public List<string> MenuIngredients = new List<string>();      //지정된 재료
    [HideInInspector] public List<string> MenuSauce = new List<string>();            //지정된 소스

    [HideInInspector] public OrderCon[] OrderCon = new OrderCon[5];

    [HideInInspector] public Vector2 innerVisitor;         //손님 선 위치
    [HideInInspector] public Vector2 outerVisitor;        //손님 나간 위치

    [HideInInspector] public int ingredientWeight = 0;      //마라재료 담긴 무게
    [HideInInspector] public int cookIndex = 0;             //요리 단계(0 : 계산대, 1 : 재료 선택 테이블, 2 : 소스 및 맵기 선택 테이블)
    [HideInInspector] public int choiceSpicy = 0;           //선택한 맵기 정도
    [HideInInspector] public int orderPapernumber = 0;      //선택한 주문서 번호
    [HideInInspector] public int doneMara = 0;              //마지막 단계
    [HideInInspector] public int menuSpicy = 0;             //지정된 맵기 정도

    [HideInInspector] public bool[] OrderPaperOn = new bool[5];

    [HideInInspector] public bool PaperPrint = false;

    private void Awake()
    {
        orderAnimeCon = orderPaperPrint.GetComponent<Animator>();
        printAudioSource = orderPaperPrint.GetComponent<AudioSource>();


        //주문지 관련 변수 초기화
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

        //오브젝트 비활성화
        visitorComentBox.SetActive(false);
        nextButton.SetActive(false);
        chefComentBox.SetActive(false);
        visitorComentBox.SetActive(true);

        //요리테이블 위치 초기화
        tablePosition.anchoredPosition = new Vector2(2880, 0);

        //총점 초기화
        systemCon.TotalScore = 0;

        resetTable = tablePosition;
        resetLeftPot = leftPotPosition;

        startingPoint = tablePosition.anchoredPosition.x;

        balanceTotal.text = "0";

        //마라 재료 이미지 초기화
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

        //요리제작 제한시간
        if (cookTimer > 0) cookTimer -= Time.deltaTime;
        else if (cookIndex != 0 && cookTimer <= 0)
        {
            cookTimer = 0;
            systemCon.VisitorIndex++;

            StartCoroutine(VisitorMoving(innerVisitor, outerVisitor));

            if (!movingTable) StartCoroutine(TableMove(tablePosition.anchoredPosition));
        }
    }

    //재료회전판 회전 함수
    private void RotationCircleMenu()
    {
        Vector3 angles = menuRotation.eulerAngles;

        if (Input.GetKeyDown(KeyCode.R)) speedMultiply++;   //회전속도의 배수값

        if (speedMultiply > 9) speedMultiply = 1;   //일정 단계를 넘어가면 속도 초기화

        angles.z = angles.z - rotationSpeed * speedMultiply * Time.deltaTime;
        menuRotation.eulerAngles = angles;
    }

    //맵기 관련 대사 함수
    private void Spicy(int num)
    {
        //index 특수번호 설정 : 2, 3, 5, 8 ~ 10, 13, 15, 17, 21 ~ 25, 29
        if (num == 2 || num == 23)                      spicyPhase.text = "신라면 맵기로 해주세요";
        else if (num == 3 || num == 29)                 spicyPhase.text = "안 맵게 해주세요";
        else if (num == 5)                              spicyPhase.text = "위장이 탈 만큼 맵게 해주세요";
        else if (num == 8)                              spicyPhase.text = "적당히 덜 맵게 해주세요";
        else if (num == 9)                              spicyPhase.text = "물만 넣어주세요";
        else if (num == 10)                             spicyPhase.text = "먹고 내일 못 일어날만큼\n맵게 해주세요";
        else if (num == 13)                             spicyPhase.text = "사골탕으로 만들어주세요";
        else if (num == 15 || num == 21 || num == 22)   spicyPhase.text = "고향의 맛이 생각나도록\n맵게 해주세요";
        else if (num == 17)                             spicyPhase.text = "홍○천의 매운 볶음면보다\n덜 맵게 해주세요";
        else if (num == 24)                             spicyPhase.text = "먹고 죽을만큼 맵게 해주세요";
        else if (num == 25)                             spicyPhase.text = "제 장래희망은 용이에요";

        //나머지 평범한 맵기 표기
        else spicyPhase.text = "맵기는 " + maraList.MaraSpicy[num] + "단계로 해주세요";
    }

    //게임 전체시간 흐름
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

    //캐릭터 등장순번 중복없이 난수
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

    //셰프 알리미
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

        ChoiceIngredients.Clear();      //메뉴 재료 초기화
        ChoiceSauce.Clear();            //메뉴 소스 초기화

        MenuIngredients.Clear();
        MenuSauce.Clear();

        choiceSpicy = 0;                //메뉴 맵기 초기화
        doneMara = 0;                   //끓이기 초기화
        menuSpicy = 0;
    }

    //주문지 선택 후 세부사항 설정
    public void OrderRandom(int choiceIndex)
    {
        if (movingTable || OrderCon[choiceIndex].OrderMoving) return;
        int index = Random.Range(0, 30);

        string[] TempIngredients = maraList.MaraIngredients[index];     //재료 담는 임시배열
        string[] TempSauce = maraList.MaraSauce[index];                 //소스 담는 임시배열
        
        int[] TempWeight = maraList.MaraWeight[index];      //무게 담는 임시배열

        orderPapernumber = choiceIndex;

        //무게 및 메뉴 책정 UI
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

        //재료 리스트 저장
        for (int i = 0; i < TempIngredients.Length; i++) for (int j = 0; j < TempWeight[i]; j++)
        {
            MenuIngredients.Add(TempIngredients[i]);
        }

        //맵기 단계 설정
        menuSpicy = maraList.MaraSpicy[index];

        Spicy(index);

        //소스 설정
        for (int i = 0; i < TempSauce.Length; i++) MenuSauce.Add(TempSauce[i]);

        if (TempSauce[0] != TempSauce[1])      sauce.text = $"{TempSauce[0]} 1번, \n{TempSauce[1]} 1번";
        else sauce.text = $"{TempSauce[0]} 2번";

        //주문지 받을 때만 테이블 모드로 들어갈 수 있도록 조건문 작성
        if (cookIndex != 0) return;
        ingredientWeight = 0;
        cookTimer = 60;

        StartCoroutine(TableMove(tablePosition.anchoredPosition));
    }

    //재료 선택
    public void MenuChoice(string menu)
    {
        //넣을 수 있는 재료 목록
        string[] menuGroup = new string[13]
        {
            "고기", "목이버섯", "배추", "분모자", "비엔나", "새우",
            "생선뼈", "숙주", "양말", "오레오", "청경채", "팽이버섯", "푸주"
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

    //맵기 정도 선택
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

    //소스 선택
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

    //버튼으로 테이블 작동
    public void NextMoving()
    {
        StartCoroutine(TableMove(tablePosition.anchoredPosition));
    }

    //손님의 평가
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

    //주문지 출력 간격
    public float OrderPaperTimer()
    {
        float alarm;

        if (timerMinute == 6 || timerMinute == 5) alarm = OrderTimerList[0];        //5분대, 40s

        else if (timerMinute == 4)
        {
            if ((int)timerSecond >= 40) alarm = OrderTimerList[0];      //4분 40초 이상, 40s
            else alarm = OrderTimerList[1];                             //4분 39초 이하, 30s
        }

        else if (timerMinute == 3)
        {
            if ((int)timerSecond >= 10) alarm = OrderTimerList[1];      //3분 10초 이상, 30s
            else alarm = OrderTimerList[2];                             //3분 9초 이하, 25s
        }

        else if (timerMinute == 2) alarm = OrderTimerList[2];       //2분대, 25s

        else if (timerMinute == 1)
        {
            if ((int)timerSecond >= 55) alarm = OrderTimerList[2];      //1분 55초 이상, 25s
            else alarm = OrderTimerList[3];                             //1분 54초 이하, 20s
        }

        else
        {
            if ((int)timerSecond >= 55) alarm = OrderTimerList[3];          //55초 이상, 20s
            else if ((int)timerSecond >= 10) alarm = OrderTimerList[4];     //10초 이상, 15s
            else alarm = OrderTimerList[5];                                 //마지막, 10s
        }

        return alarm;
    }

    //점수 계산
    private int TotalScoreCalculate()
    {
        int calculate = 0;

        MenuIngredients.Sort();         //지정된 재료 목록 오름차순 정렬
        ChoiceIngredients.Sort();       //선택한 재료 목록 오름차순 정렬

        MenuSauce.Sort();       //지정된 소스 목록 오름차순 정렬
        ChoiceSauce.Sort();     //선택한 소스 목록 오름차순 정렬

        //재료 목록을 중복값만 저장한 이후, 해당 리스트의 요소 개수만큼 점수 추가(개당 +10점)
        List<string> IngredientsList = new List<string>();
        for (int i = 0; i < MenuIngredients.Count; i++) if (MenuIngredients[i] == ChoiceIngredients[i]) IngredientsList.Add(MenuIngredients[i]);

        calculate += IngredientsList.Count * 10;

        //소스 목록을 중복값만 저장한 이후, 해당 리스트의 요소 개수만큼 점수 추가(개당 +10점)
        List<string> SauceList = new List<string>();
        for (int i = 0; i < MenuSauce.Count; i++) if (MenuSauce[i] == ChoiceSauce[i]) SauceList.Add(MenuSauce[i]);
        calculate += SauceList.Count * 10;

        //맵기단계 맞을 경우, +20점
        if (choiceSpicy == menuSpicy) calculate += 20;

        menuPerfect = calculate;

        return systemCon.TotalScore += calculate;
    }

    //캐릭터 대사
    private string ConsumerComent(int index)
    {
        string coment;

        coment = maraList.ConsumerComent[CharaIndexGroup[visitorIndex]][index];

        return coment;
    }

    //오브젝트 떨기
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

    //손님 입장 및 퇴장
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

    //마라탕 끓이기
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

            if (menuPerfect >= 60) resultPot.sprite = potImage[1];        //잘 만들었을 경우
            else if (menuPerfect < 60) resultPot.sprite = potImage[2];    //못 만들었을 경우
        }

        else
        {
            resultPot.sprite = potImage[0];
            yield return null;
        }
    }

    //테이블 이동
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

    //주문지 발행 및 생성
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
