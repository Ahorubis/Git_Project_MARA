using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropCon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private AudioClip trashClip;
    [SerializeField] private AudioClip conpleteClip;


    private PlayCon playCon;

    private Transform resultPosition;
    private Image image;
    private AudioSource audioSource;

    private Vector2 resetPosition;

    private void Awake()
    {
        playCon = GameObject.Find("Canvas").GetComponent<PlayCon>();

        resultPosition = GetComponent<Transform>();
        image = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
    }

    //드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        resetPosition = resultPosition.position;
    }

    //드래그 도중
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.8f);

        for (int i = 0; i < playCon.bowlMara.Length; i++)
        {
            if (playCon.bowlMara[i].sprite == null) playCon.bowlMara[i].color = new Color(1, 1, 1, 0);
            else playCon.bowlMara[i].color = new Color(1, 1, 1, 0.8f);
        }
    }

    //드랍
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 min, max;

        if (playCon.cookIndex == 1)
        {
            min = playCon.gizmos[0].position + new Vector3(-368, -82, 0);
            max = playCon.gizmos[0].position + new Vector3(368, 82, 0);

            transform.position = resetPosition;

            //마라탕 재료를 비울 경우
            if (eventData.position.x > min.x && eventData.position.x < max.x && eventData.position.y > min.y && eventData.position.y < max.y)
            {
                audioSource.PlayOneShot(trashClip);

                for (int i = 0; i < playCon.bowlMara.Length; i++)
                {
                    playCon.bowlMara[i].sprite = null;
                    playCon.bowlMara[i].color = new Color(1, 1, 1, 0);
                }

                playCon.nextButton.SetActive(false);

                playCon.ingredientWeight = 0;
                playCon.ChoiceIngredients.Clear();
            }

            else
            {
                for (int i = 0; i < playCon.bowlMara.Length; i++)
                {
                    if (playCon.bowlMara[i].sprite == null) playCon.bowlMara[i].color = new Color(1, 1, 1, 0);
                    else playCon.bowlMara[i].color = new Color(1, 1, 1, 1);
                }
            }
        }

        else if (playCon.cookIndex == 2)
        {
            min = playCon.gizmos[1].position + new Vector3(-200, -350, 0);
            max = playCon.gizmos[1].position + new Vector3(200, 350, 0);

            //마라탕 제출할 경우
            if (eventData.position.x > min.x && eventData.position.x < max.x && eventData.position.y > min.y && eventData.position.y < max.y)
            {
                audioSource.PlayOneShot(conpleteClip);

                for (int i = 0; i < playCon.bowlMara.Length; i++)
                {
                    playCon.bowlMara[i].color = new Color(1, 1, 1, 0);
                    playCon.bowlMara[i].sprite = null;
                }

                playCon.NextMoving();
                playCon.nextButton.SetActive(false);    //다음단계 버튼

                playCon.ConsumerStar();

                //셰프 코멘트
                if (playCon.chefComentBox.activeSelf == true) playCon.chefComentBox.SetActive(false);

                playCon.ChoiceIngredients.Clear();      //메뉴 재료 초기화
                playCon.ChoiceSauce.Clear();            //메뉴 소스 초기화

                playCon.MenuIngredients.Clear();
                playCon.MenuSauce.Clear();

                playCon.choiceSpicy = 0;                //메뉴 맵기 초기화
                playCon.doneMara = 0;                   //끓이기 초기화
                playCon.menuSpicy = 0;


                playCon.resultPot.sprite = playCon.potImage[0];

                StartCoroutine("ResetPosition");
            }

            else transform.position = resetPosition;
        }

        else transform.position = resetPosition;

        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
    }

    //마지막 단계 원위치
    private IEnumerator ResetPosition()
    {
        yield return null;

        transform.position = resetPosition;

        yield return new WaitForSeconds(playCon.settingTimer);

        OrderCon orderCon = playCon.OrderCon[playCon.orderPapernumber].GetComponent<OrderCon>();
        orderCon.StartCoroutine(orderCon.OrderAnime(orderCon.upperPosition, orderCon.underPosition));
        playCon.OrderCon[playCon.orderPapernumber].OrderReceive = false;
    }
}
