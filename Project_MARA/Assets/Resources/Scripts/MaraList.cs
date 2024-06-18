using UnityEngine;

public class MaraList : MonoBehaviour
{
    //0 : �⺻��, 1 : �־�, 2 : ����, 3 : ����, 4 : �Ϻ�
    [Header("�湮�� ����")]
    public Sprite[] firstVisitor;       //�����
    public Sprite[] secondVisitor;      //ü���Խû�
    public Sprite[] thirdVisitor;       //���� �̼ҳ�
    public Sprite[] fourthVisitor;      //������� �̼ҳ�
    public Sprite[] fifthVisitor;       //�������� ¢�� ������ ����
    public Sprite[] sixthVisitor;       //�̽÷�
    public Sprite[] seventhVisitor;     //�ȴ� �� �����̼ҳ�
    public Sprite[] eighthVisitor;      //����

    [Header("��� ����")]
    public Sprite[] BowlIngredients;    //��� ���� ����

    [HideInInspector] public Sprite[][] VisitorGroup;       //�մ� ���

    [HideInInspector] public string[][] MaraIngredients;    //������ �޴� ���
    [HideInInspector] public string[][] MaraSauce;          //������ �ҽ� ���
    [HideInInspector] public string[][] ConsumerComent;     //�մ� ��� ���
    [HideInInspector] public int[][] MaraWeight;            //������ �޴� ����

    [HideInInspector] public string[] ChefComent;           //�ֹ��� ��Ʈ
    [HideInInspector] public int[] MaraSpicy;               //������ �ſ� �ܰ�

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
            new string [] {"����", "Ǫ��", "�и���", "���"},
            new string [] {"����", "û��ä", "����", "����", "�񿣳�"},
            new string [] {"û��ä", "���", "����", "�񿣳�", "���̹���"},
            new string [] {"���̹���", "���", "Ǫ��", "����", "�и���", "����", "�񿣳�"},
            new string [] {"����", "���̹���", "Ǫ��", "����", "����"},
            new string [] {"û��ä", "���", "Ǫ��", "�и���", "������"},
            new string [] {"����", "û��ä", "���̹���", "Ǫ��", "����", "�и���", "�񿣳�"},
            new string [] {"����", "���", "����", "�񿣳�"},
            new string [] {"û��ä", "���̹���", "Ǫ��", "�и���", "����", "�񿣳�"},
            new string [] {"����", "�񿣳�", "������"},
            new string [] {"����", "�񿣳�"},
            new string [] {"û��ä", "�и���", "����", "�񿣳�"},
            new string [] {"���̹���", "���", "����", "�и���", "�񿣳�"},
            new string [] {"����", "û��ä", "���̹���", "���", "Ǫ��", "����", "�и���"},
            new string [] {"����", "û��ä", "���̹���", "���", "Ǫ��", "����", "�и���", "����", "�񿣳�"},
            new string [] {"�縻", "������", "������"},
            new string [] {"���̹���", "���", "����", "����", "�񿣳�"},
            new string [] {"���", "�и���", "����", "�񿣳�", "������"},
            new string [] {"û��ä", "���", "Ǫ��", "����", "�и���", "�񿣳�"},
            new string [] {"����", "���̹���", "����", "�񿣳�", "���̹���"},
            new string [] {"û��ä", "���", "Ǫ��", "����", "���̹���"},
            new string [] {"���̹���", "Ǫ��", "����", "�и���", "����", "���̹���"},
            new string [] {"����", "û��ä", "���̹���", "���", "�и���", "����", "�񿣳�", "���̹���"},
            new string [] {"����", "�񿣳�", "���̹���"},
            new string [] {"û��ä", "���̹���", "���", "Ǫ��", "�񿣳�", "�縻"},
            new string [] {"���̹���"},
            new string [] {"�и���", "������", "������"},
            new string [] {"����", "���", "Ǫ��", "����", "���̹���"},
            new string [] {"���̹���", "Ǫ��", "����", "����", "�񿣳�", "���̹���"},
            new string [] {"����", "û��ä", "���̹���", "���", "Ǫ��", "����", "�и���", "����", "�񿣳�"}
        };

        MaraSauce = new string[30][]
        {
            new string [2] {"����ҽ�", "���߱⸧"},
            new string [2] {"����ҽ�", "�����ҽ�"},     
            new string [2] {"�����ҽ�", "�����ҽ�"},     //�����ҽ� �ϳ�
            new string [2] {"����ҽ�", "����ҽ�"},     //����ҽ� �ϳ�
            new string [2] {"�����ҽ�", "���߱⸧"},
            new string [2] {"����ҽ�", "�����ҽ�"},
            new string [2] {"����ũ��", "����ũ��"},     //����ũ�� �ϳ�
            new string [2] {"����ҽ�", "�����ҽ�"},
            new string [2] {"����ҽ�", "�����ҽ�"},
            new string [2] {"�����ҽ�", "�����ҽ�"},     //�����ҽ� �ϳ�
            new string [2] {"�����ҽ�", "���߱⸧"},
            new string [2] {"����ҽ�", "���߱⸧"},
            new string [2] {"���߱⸧", "���߱⸧"},     //���߱⸧ �ϳ�
            new string [2] {"����ҽ�", "�����ҽ�"},
            new string [2] {"���߱⸧", "���߱⸧"},     //���߱⸧ �ϳ�
            new string [2] {"�����ҽ�", "����ũ��"},
            new string [2] {"����ҽ�", "���߱⸧"},
            new string [2] {"����ҽ�", "����ҽ�"},     //����ҽ� �ϳ�
            new string [2] {"���߱⸧", "���߱⸧"},     //���߱⸧ �ϳ�
            new string [2] {"�����ҽ�", "���߱⸧"},
            new string [2] {"���߱⸧", "���߱⸧"},     //���߱⸧ �ϳ�
            new string [2] {"����ҽ�", "�����ҽ�"},
            new string [2] {"����ҽ�", "����ҽ�"},     //����ҽ� �ϳ�
            new string [2] {"�����ҽ�", "���߱⸧"},
            new string [2] {"�����ҽ�", "����ũ��"},
            new string [2] {"����ҽ�", "����ҽ�"},     //����ҽ� �ϳ�
            new string [2] {"����ҽ�", "���߱⸧"},
            new string [2] {"���߱⸧", "���߱⸧"},     //���߱⸧ �ϳ�
            new string [2] {"�����ҽ�", "���߱⸧"},
            new string [2] {"�����ҽ�", "���߱⸧"}
        };

        ConsumerComent = new string[8][]
        {
            new string [6]
            {
                "�ǰ����� ���� �������� \n���ƿ�.", "���� ������.", "��?",
                "��, �׳������̳׿�.", "�������ѵ���?", "���� ������ ���� �ͳ׿�!"
            },
            new string [6]
            {
                "�� ������ �� �� ���� ����?", "��� �� ����� \n�������������������", "�̰� ���� ������?",
                "�������� �ʾƿ�.", "�ټ��忡 ������ �ǰڳ׿�!", "�ܹ��� ����ũ�� \n�ʿ� ���ھ��."
            },
            new string [6]
            {
                "���� �Ұ�� �������� \n��򰡿�?", "��������������������������\n��������������", "��������",
                "ä�Ұ� ���� �� �־����� \n���ھ��.", "������ �����׿�. \n��â�ϼ���.", "�������� �� �ǻߺ��� ���� \n���� ���� ���̿���!"
            },
            new string [6]
            {
                "����ģ�� ������ �ϴµ� \n�� ����� ���� ������?", "������ ���� �� ������ \n���� �ֳ���?", "�������� ���̳׿�.",
                "���⸧�� �ʹ� ���� \n�� �� �ƴѰ���?", "���� �ļ���!", "��ᰡ �ż��ؼ� \n���ֳ׿�."
            },
            new string [6]
            {
                "�п�!", "�����, �̰� �ƴѰ� \n�����ϴ�.", "����������������",
                "����", "�۸�!", "�Ŀ÷��Ͽ����ƿ��Ӿ̤�!!"
            },
            new string [6]
            {
                "���� �Ʊ⵵ ���ϰǵ� \n���ְ� ���ּ���.", "����? ����� �̰��� \n�߸��Ȱ� �ƴѰ���?", "������� ���̳׿䡦",
                "�������׿�.", "������ �� ���� �ͳ׿�.", "�̷� ���� ó���̿��䡦"
            },
            new string [6]
            {
                "�п� ���ߵż� ��Ʈ���� \n�����ϱ� ���ְ� ���ּ���.", "���� ������ ���ʹ�.", "������? ����?",
                "�и��ڰ� �� �������� \n���ھ��.", "���־��!", "���!!!!!!! �ʹ� ���־�!!!!!!!"
            },
            new string []
            {
                "�Կ� ���� ���� �� ä��� \n�;��!", "���� ���� ���� �̻��ѵ���?", "�� �ȳ��� ���� �ǳ���?",
                "�� �Ծ����ϴ�!", "���� �ҸӴϵ� �ڼ�ĥ \n���Դϴ�.", "��ȿ������!!! �� ���ִ���!"
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
            "���� ���� �϶� ��", "�մ��� ��ٸ��� ��", "�̷��� �ؼ� ����\n �� �ش� ��", "��ᰡ �Ʊ��� ��",
            "���� �ް� ���� �ʳ� ��", "���� �� ����ų� ��", "�츮�� �ҸӴϵ�\n �̰ź��ٴ� �����ڴ� ��",
            "����׸�����\n �� �԰ڴ� ��"
        };

        MaraSpicy = new int[30]
        {3, 2, 2, 1, 1, 4, 2, 3, 2, 1, 4, 2, 4, 1, 4, 4, 3, 3, 1, 2, 4, 4, 4, 2, 4, 4, 3, 3, 2, 1};
    }
}
