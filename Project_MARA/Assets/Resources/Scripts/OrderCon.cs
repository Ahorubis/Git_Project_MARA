using System.Collections;
using UnityEngine;

public class OrderCon : MonoBehaviour
{
    [SerializeField] private float orderTimer = 0.5f;

    private PlayCon playCon;
    private RectTransform orderPosition;

    [HideInInspector] public Vector2 underPosition;      //주문서 나오기 전 위치
    [HideInInspector] public Vector2 upperPosition;      //주문서 나온 후 위치

    [HideInInspector] public float TimerThis;

    [HideInInspector] public int ThisIndex;

    [HideInInspector] public bool OrderMoving;
    [HideInInspector] public bool OrderReceive;

    private void Awake()
    {
        playCon = GameObject.Find("Canvas").GetComponent<PlayCon>();
        orderPosition = GetComponent<RectTransform>();

        underPosition = orderPosition.anchoredPosition;
        upperPosition = underPosition + new Vector2(0, 155);        //y좌표 -475

        TimerThis = 0;

        OrderMoving = false;
        OrderReceive = false;
    }

    private void Update()
    {
        if (TimerThis <= 0) TimerThis = 0;
        else
        {
            TimerThis -= Time.deltaTime;

            if (TimerThis <= 0 && !playCon.PaperPrint)
            {
                playCon.StartCoroutine(playCon.OrderPaperAnime(ThisIndex));
                TimerThis = 0;
                OrderReceive = false;
                playCon.OrderPaperOn[ThisIndex] = false;
            }
        }
    }

    //주문서 이동
    public IEnumerator OrderAnime(Vector2 start, Vector2 end)
    {
        float timer = 0;

        OrderMoving = true;

        if (start == upperPosition) playCon.StartCoroutine(playCon.VisitorMoving(playCon.innerVisitor, playCon.outerVisitor));

        while (timer < orderTimer)
        {
            orderPosition.anchoredPosition = Vector2.Lerp(start, end, timer / orderTimer);
            timer += Time.deltaTime;

            if (timer > orderTimer * 0.99f)
            {
                orderPosition.anchoredPosition = end;
                OrderMoving = false;
                break;
            }

            yield return null;
        }
    }
}
