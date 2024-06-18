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

    //�巡�� ����
    public void OnBeginDrag(PointerEventData eventData)
    {
        resetPosition = resultPosition.position;
    }

    //�巡�� ����
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

    //���
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 min, max;

        if (playCon.cookIndex == 1)
        {
            min = playCon.gizmos[0].position + new Vector3(-368, -82, 0);
            max = playCon.gizmos[0].position + new Vector3(368, 82, 0);

            transform.position = resetPosition;

            //������ ��Ḧ ��� ���
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

            //������ ������ ���
            if (eventData.position.x > min.x && eventData.position.x < max.x && eventData.position.y > min.y && eventData.position.y < max.y)
            {
                audioSource.PlayOneShot(conpleteClip);

                for (int i = 0; i < playCon.bowlMara.Length; i++)
                {
                    playCon.bowlMara[i].color = new Color(1, 1, 1, 0);
                    playCon.bowlMara[i].sprite = null;
                }

                playCon.NextMoving();
                playCon.nextButton.SetActive(false);    //�����ܰ� ��ư

                playCon.ConsumerStar();

                //���� �ڸ�Ʈ
                if (playCon.chefComentBox.activeSelf == true) playCon.chefComentBox.SetActive(false);

                playCon.ChoiceIngredients.Clear();      //�޴� ��� �ʱ�ȭ
                playCon.ChoiceSauce.Clear();            //�޴� �ҽ� �ʱ�ȭ

                playCon.MenuIngredients.Clear();
                playCon.MenuSauce.Clear();

                playCon.choiceSpicy = 0;                //�޴� �ʱ� �ʱ�ȭ
                playCon.doneMara = 0;                   //���̱� �ʱ�ȭ
                playCon.menuSpicy = 0;


                playCon.resultPot.sprite = playCon.potImage[0];

                StartCoroutine("ResetPosition");
            }

            else transform.position = resetPosition;
        }

        else transform.position = resetPosition;

        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
    }

    //������ �ܰ� ����ġ
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
