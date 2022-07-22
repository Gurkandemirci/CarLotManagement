using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;


public class ParkSlotManager : MonoBehaviour
{
    public ObjectTypes myObjectType;
    public bool isEmpty = true;
    public bool isDisabled;
    public int slotCost = 100;
    [SerializeField]  Transform timerImageTransform;
    [SerializeField]  Transform slotNumberTransform;
    [SerializeField]  Transform unlockButtonTransform;
    [SerializeField]  TMP_Text costText;
    [SerializeField] Image disableImg;
 
    private void Start()
    {
        if(myObjectType == ObjectTypes.leftParkSlot)
        {
            timerImageTransform.localRotation=Quaternion.Euler(-timerImageTransform.localRotation.eulerAngles.x, 
                                                                timerImageTransform.localRotation.eulerAngles.y, 
                                                                timerImageTransform.localRotation.eulerAngles.z);
            timerImageTransform.gameObject.GetComponent<Image>().fillClockwise = true;

            unlockButtonTransform.localRotation = Quaternion.Euler(unlockButtonTransform.localRotation.eulerAngles.x,
                                                                             unlockButtonTransform.localRotation.eulerAngles.y + 180, 
                                                                             unlockButtonTransform.localRotation.eulerAngles.z);

            slotNumberTransform.localRotation = Quaternion.Euler(timerImageTransform.localRotation.eulerAngles.x,
                                                                timerImageTransform.localRotation.eulerAngles.y + 180,
                                                                timerImageTransform.localRotation.eulerAngles.z);

        }

        costText.text = slotCost.ToString();



    }

    public void DefineCarToSlot()
    {
        isEmpty = false;
    }

    public void StartTimer(GameObject car)
    {
        StartCoroutine(DrawTimer(car));
    }

    public IEnumerator DrawTimer(GameObject car)
    {
        timerImageTransform.gameObject.GetComponent<Image>().DOFillAmount(1, 8);
        yield return new WaitForSeconds(8f);
        
        timerImageTransform.gameObject.GetComponent<Image>().fillAmount = 0;
        car.GetComponent<CarManager>().TakeOutAndDestroy();
        isEmpty = true;
        MoneyManager.instance.AddMoney(car.GetComponent<CarManager>().carMoney);
    }





}
