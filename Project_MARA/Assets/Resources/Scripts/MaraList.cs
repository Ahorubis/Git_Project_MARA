using UnityEngine;

public class MaraList : MonoBehaviour
{
    //0 : 기본값, 1 : 최악, 2 : 나쁨, 3 : 좋음, 4 : 완벽
    [Header("방문객 모음")]
    public Sprite[] firstVisitor;       //멘헤라
    public Sprite[] secondVisitor;      //체대입시생
    public Sprite[] thirdVisitor;       //은발 미소녀
    public Sprite[] fourthVisitor;      //흑발적안 미소녀
    public Sprite[] fifthVisitor;       //맛있으면 짖는 강아지 주인
    public Sprite[] sixthVisitor;       //미시룩
    public Sprite[] seventhVisitor;     //안대 쓴 교복미소녀
    public Sprite[] eighthVisitor;      //세라복

    [Header("재료 모음")]
    public Sprite[] BowlIngredients;    //재료 낱개 모음

    [HideInInspector] public Sprite[][] VisitorGroup;       //손님 목록

    [HideInInspector] public string[][] MaraIngredients;    //마라탕 메뉴 목록
    [HideInInspector] public string[][] MaraSauce;          //마라탕 소스 목록
    [HideInInspector] public string[][] ConsumerComent;     //손님 대사 목록
    [HideInInspector] public int[][] MaraWeight;            //마라탕 메뉴 무게

    [HideInInspector] public string[] ChefComent;           //주방장 멘트
    [HideInInspector] public int[] MaraSpicy;               //마라탕 매운 단계

    private void Awake()
    {
        VisitorGroup = new Sprite[8][]
        {
            new Sprite [5] { firstVisitor[0], firstVisitor[1], firstVisitor[2], firstVisitor[3], firstVisitor[4] },
            new Sprite [5] { secondVisitor[0], secondVisitor[1], secondVisitor[2], secondVisitor[3], secondVisitor[4] },
            new Sprite [5] { thirdVisitor[0], thirdVisitor[1], thirdVisitor[2], thirdVisitor[3], thirdVisitor[4] },
            new Sprite [5] { fourthVisitor[0], fourthVisitor[1], fourthVisitor[2], fourthVisitor[3], fourthVisitor[4] },
            new Sprite [5] { fifthVisitor[0], fifthVisitor[1], fifthVisitor[2], fifthVisitor[3], fifthVisitor[4] },
            new Sprite [5] { sixthVisitor[0], sixthVisitor[1], sixthVisitor[2], sixthVisitor[3], sixthVisitor[4] },
            new Sprite [5] { seventhVisitor[0], seventhVisitor[1], seventhVisitor[2], seventhVisitor[3], seventhVisitor[4] },
            new Sprite [5] { eighthVisitor[0], eighthVisitor[1], eighthVisitor[2], eighthVisitor[3], eighthVisitor[4] }
        };

        MaraIngredients = new string[30][]
        {
            new string [] {"숙주", "푸주", "분모자", "고기"},
            new string [] {"숙주", "청경채", "배추", "새우", "비엔나"},
            new string [] {"청경채", "고기", "새우", "비엔나", "팽이버섯"},
            new string [] {"목이버섯", "고기", "푸주", "배추", "분모자", "새우", "비엔나"},
            new string [] {"숙주", "목이버섯", "푸주", "배추", "새우"},
            new string [] {"청경채", "고기", "푸주", "분모자", "생선뼈"},
            new string [] {"숙주", "청경채", "목이버섯", "푸주", "배추", "분모자", "비엔나"},
            new string [] {"숙주", "고기", "배추", "비엔나"},
            new string [] {"청경채", "목이버섯", "푸주", "분모자", "새우", "비엔나"},
            new string [] {"숙주", "비엔나", "오레오"},
            new string [] {"새우", "비엔나"},
            new string [] {"청경채", "분모자", "새우", "비엔나"},
            new string [] {"목이버섯", "고기", "배추", "분모자", "비엔나"},
            new string [] {"숙주", "청경채", "목이버섯", "고기", "푸주", "배추", "분모자"},
            new string [] {"숙주", "청경채", "목이버섯", "고기", "푸주", "배추", "분모자", "새우", "비엔나"},
            new string [] {"양말", "생선뼈", "오레오"},
            new string [] {"목이버섯", "고기", "배추", "새우", "비엔나"},
            new string [] {"고기", "분모자", "새우", "비엔나", "생선뼈"},
            new string [] {"청경채", "고기", "푸주", "배추", "분모자", "비엔나"},
            new string [] {"숙주", "목이버섯", "새우", "비엔나", "팽이버섯"},
            new string [] {"청경채", "고기", "푸주", "배추", "팽이버섯"},
            new string [] {"목이버섯", "푸주", "배추", "분모자", "새우", "팽이버섯"},
            new string [] {"숙주", "청경채", "목이버섯", "고기", "분모자", "새우", "비엔나", "팽이버섯"},
            new string [] {"새우", "비엔나", "팽이버섯"},
            new string [] {"청경채", "목이버섯", "고기", "푸주", "비엔나", "양말"},
            new string [] {"팽이버섯"},
            new string [] {"분모자", "생선뼈", "오레오"},
            new string [] {"숙주", "고기", "푸주", "새우", "팽이버섯"},
            new string [] {"목이버섯", "푸주", "배추", "새우", "비엔나", "팽이버섯"},
            new string [] {"숙주", "청경채", "목이버섯", "고기", "푸주", "배추", "분모자", "새우", "비엔나"}
        };

        MaraSauce = new string[30][]
        {
            new string [2] {"땅콩소스", "고추기름"},
            new string [2] {"땅콩소스", "마유소스"},     
            new string [2] {"마유소스", "마유소스"},     //마유소스 하나
            new string [2] {"땅콩소스", "땅콩소스"},     //땅콩소스 하나
            new string [2] {"마유소스", "고추기름"},
            new string [2] {"땅콩소스", "마유소스"},
            new string [2] {"휘핑크림", "휘핑크림"},     //휘핑크림 하나
            new string [2] {"땅콩소스", "마유소스"},
            new string [2] {"땅콩소스", "마유소스"},
            new string [2] {"마유소스", "마유소스"},     //마유소스 하나
            new string [2] {"마유소스", "고추기름"},
            new string [2] {"땅콩소스", "고추기름"},
            new string [2] {"고추기름", "고추기름"},     //고추기름 하나
            new string [2] {"땅콩소스", "마유소스"},
            new string [2] {"고추기름", "고추기름"},     //고추기름 하나
            new string [2] {"마유소스", "휘핑크림"},
            new string [2] {"땅콩소스", "고추기름"},
            new string [2] {"땅콩소스", "땅콩소스"},     //땅콩소스 하나
            new string [2] {"고추기름", "고추기름"},     //고추기름 하나
            new string [2] {"마유소스", "고추기름"},
            new string [2] {"고추기름", "고추기름"},     //고추기름 하나
            new string [2] {"땅콩소스", "마유소스"},
            new string [2] {"땅콩소스", "땅콩소스"},     //땅콩소스 하나
            new string [2] {"마유소스", "고추기름"},
            new string [2] {"마유소스", "휘핑크림"},
            new string [2] {"땅콩소스", "땅콩소스"},     //땅콩소스 하나
            new string [2] {"땅콩소스", "고추기름"},
            new string [2] {"고추기름", "고추기름"},     //고추기름 하나
            new string [2] {"마유소스", "고추기름"},
            new string [2] {"마유소스", "고추기름"}
        };

        ConsumerComent = new string[8][]
        {
            new string [6]
            {
                "건강하지 않은 마라탕이 \n좋아요.", "으… 끔찍해.", "…?",
                "뭐, 그냥저냥이네요.", "먹을만한데요?", "집에 포장해 가고 싶네요!"
            },
            new string [6]
            {
                "맛 없으면 돈 안 내도 되죠?", "우욱… 욱… 우욱…… \n우웨에에에에에에에엑", "이게 무슨 맛이죠?",
                "…나쁘지 않아요.", "근성장에 도움이 되겠네요!", "단백질 쉐이크가 \n필요 없겠어요."
            },
            new string [6]
            {
                "여기 소고기 원산지가 \n어딘가요?", "…………………………………\n…………………", "음………",
                "채소가 조금 더 있었으면 \n좋겠어요.", "적당히 괜찮네요. \n번창하세요.", "……저희 집 뽀삐보다 자주 \n보고 싶은 맛이에요!"
            },
            new string [6]
            {
                "남자친구 만나야 하는데 \n살 안찌는 재료는 없나요?", "……저 토할 거 같은데 \n봉지 있나요?", "…오묘한 맛이네요.",
                "…기름이 너무 많이 \n들어간 거 아닌가요?", "많이 파세요!", "재료가 신선해서 \n맛있네요."
            },
            new string [6]
            {
                "왈왈!", "사장님, 이건 아닌것 \n같습니다.", "………으르르르르",
                "……", "멍멍!", "왘올로일오라라아오앙앍ㄱ!!"
            },
            new string [6]
            {
                "저희 아기도 먹일건데 \n맛있게 해주세요.", "에…? 사장님 미각이 \n잘못된거 아닌가요?", "충격적인 맛이네요…",
                "…괜찮네요.", "다음에 또 오고 싶네요.", "이런 맛은 처음이에요…"
            },
            new string [6]
            {
                "학원 가야돼서 스트레스 \n받으니까 맛있게 해주세요.", "으… 으…… 울고싶다.", "으음…? 음…?",
                "분모자가 더 많았으면 \n좋겠어요.", "맛있어요!", "대박!!!!!!! 너무 맛있어!!!!!!!"
            },
            new string []
            {
                "촬영 가기 전에 배 채우고 \n싶어요!", "저… 저… 혀가 이상한데요?", "돈 안내고 가도 되나요?",
                "잘 먹었습니다!", "저희 할머니도 박수칠 \n맛입니다.", "우효오오옷!!! 초 맛있다제!"
            }
        };

        MaraWeight = new int[30][]
        {
            new int[] {4, 2, 2, 2},
            new int[] {2, 2, 2, 1, 3},
            new int[] {3, 3, 1, 1, 2},
            new int[] {1, 2, 2, 1, 1, 2, 1},
            new int[] {4, 2, 1, 1, 2},
            new int[] {2, 3, 1, 2, 2},
            new int[] {1, 2, 1, 2, 2, 1, 1},
            new int[] {5, 2, 1, 2},
            new int[] {3, 1, 2, 1, 1, 2},
            new int[] {4, 3, 3},
            new int[] {8, 2},
            new int[] {3, 2, 2, 3},
            new int[] {2, 3, 2, 1, 2},
            new int[] {2, 3, 1, 1, 1, 1, 1},
            new int[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            new int[] {3, 5, 2},
            new int[] {2, 1, 3, 2, 2},
            new int[] {4, 2, 1, 1, 2},
            new int[] {1, 3, 2, 2, 1, 1},
            new int[] {3, 2, 1, 2, 2},
            new int[] {2, 3, 2, 1, 2},
            new int[] {1, 3, 2, 1, 1, 2},
            new int[] {1, 2, 1, 1, 1, 2, 1, 1},
            new int[] {3, 1, 6},
            new int[] {2, 1, 1, 2, 2, 2},
            new int[] {10},
            new int[] {5, 3, 2},
            new int[] {1, 4, 2, 2, 1},
            new int[] {2, 1, 1, 2, 1, 3},
            new int[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
        };

        ChefComent = new string[8]
        {
            "빨리 빨리 하라 해", "손님이 기다린다 해", "이렇게 해서 가게\n 못 준다 해", "재료가 아깝다 해",
            "월급 받고 싶지 않냐 해", "내일 다 만들거냐 해", "우리집 할머니도\n 이거보다는 빠르겠다 해",
            "베어그릴스도\n 못 먹겠다 해"
        };

        MaraSpicy = new int[30]
        {3, 2, 2, 1, 1, 4, 2, 3, 2, 1, 4, 2, 4, 1, 4, 4, 3, 3, 1, 2, 4, 4, 4, 2, 4, 4, 3, 3, 2, 1};
    }
}
