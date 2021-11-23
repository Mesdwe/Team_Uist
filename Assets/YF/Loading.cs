using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections.Generic;

/**
 *两个背景图片平移 
 */
public class Loading : MonoBehaviour
{
    public GameObject back1;
    public GameObject back2;
    public int speed = 1;                       //背景平移的速度
    public Transform endPosition;       //到达该位置,把背景移动到开始位置
    public Transform startPosition;      //背景从开始位置移动到结束位置

    private GameObject[] ContentChilders = new GameObject[14];
    void Start()
    {
        //进入检测添加
        UnityAction<BaseEventData> click = new UnityAction<BaseEventData>(OnPointerEnter);
        EventTrigger.Entry myclick = new EventTrigger.Entry();
        myclick.eventID = EventTriggerType.PointerEnter;
        myclick.callback.AddListener(click);
        //移出检测添加
        UnityAction<BaseEventData> click1 = new UnityAction<BaseEventData>(OnPointerExit);
        EventTrigger.Entry myclick1 = new EventTrigger.Entry();
        myclick1.eventID = EventTriggerType.PointerExit;
        myclick1.callback.AddListener(click1);
        int i = 0;
        foreach (Transform childer in back1.transform)
        {
            ContentChilders[i] = childer.gameObject;
            EventTrigger trigger = ContentChilders[i].AddComponent<EventTrigger>();
            trigger.triggers.Add(myclick);
            trigger.triggers.Add(myclick1);
            i++;
        }
        foreach (Transform childer in back2.transform)
        {
            ContentChilders[i] = childer.gameObject;
            EventTrigger trigger = ContentChilders[i].AddComponent<EventTrigger>();
            trigger.triggers.Add(myclick);
            trigger.triggers.Add(myclick1);
            i++;
        }
    }
    private RectTransform selObject;
    private void OnPointerEnter(BaseEventData arg0)
    {
        isMove = false;
        PointerEventData P_EveDat = arg0 as PointerEventData;
        selObject = P_EveDat.pointerCurrentRaycast.gameObject.transform as RectTransform;
        selObject.sizeDelta = new Vector2(300, 180);
    }

    private void OnPointerExit(BaseEventData arg0)
    {
        isMove = true;
        selObject.sizeDelta = new Vector2(280, 160);
    }

    //是否移动
    private bool isMove = true;
    void Update()
    {
        if (isMove == true)
        {
            //两个图片的平移
            back1.transform.position = new Vector3(back1.transform.position.x - speed * Time.deltaTime, back1.transform.position.y, back1.transform.position.z);
            back2.transform.position = new Vector3(back2.transform.position.x - speed * Time.deltaTime, back2.transform.position.y, back2.transform.position.z);
            //到达结束位置,回到开始位置,切换图片
            if (back1.transform.position.x <= endPosition.transform.position.x)
            {
                back1.transform.position = startPosition.transform.position;
            }
            if (back2.transform.position.x <= endPosition.transform.position.x)
            {
                back2.transform.position = startPosition.transform.position;
            }
        }
    }
}