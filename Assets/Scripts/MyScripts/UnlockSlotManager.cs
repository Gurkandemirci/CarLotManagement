using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UnlockSlotManager : MonoBehaviour
{
    public GameObject[] parkSlots;
    public GameObject[] carSlots;
    public GameObject nextLevelButton;
    public Transform gameCamera;
    public int currentParkSlot = 0; 
    public int currentCarSlot = 0;


    void Start()
    {
        for(int i = 0; i < parkSlots.Length; i++)
        {
            parkSlots[i].transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = (i + 2).ToString();
        }
    }


    public void UnlockParkSlot()
    {
        int cost = parkSlots[currentParkSlot].GetComponent<ParkSlotManager>().slotCost;

        if(cost <= MoneyManager.instance.money)
        {
            MoneyManager.instance.SubtractMoney(cost);

            parkSlots[currentParkSlot].transform.GetChild(2).gameObject.SetActive(true);
            parkSlots[currentParkSlot].transform.GetChild(3).GetChild(1).gameObject.SetActive(false);

            currentParkSlot++;
            try
            {
                parkSlots[currentParkSlot].gameObject.SetActive(true);
                parkSlots[currentParkSlot].transform.GetChild(3).gameObject.SetActive(true);

            }
            catch
            {
                nextLevelButton.gameObject.SetActive(true);
            }
            /*if(parkSlots[currentCarSlot] != null)
            {
                
            }
            else
            {
                Debug.Log("wdasda");
                nextLevelButton.gameObject.SetActive(true);
            }*/
            
        }
    }
    public void UnlockCarSlot()
    {
        int cost = carSlots[currentCarSlot].GetComponent<CarSlotManager>().slotCost;

        if (cost <= MoneyManager.instance.money)
        {
            MoneyManager.instance.SubtractMoney(cost);

            carSlots[currentCarSlot].GetComponent<CarSlotManager>().isUnlocked = true;
            carSlots[currentCarSlot].transform.GetChild(0).gameObject.SetActive(true);
            carSlots[currentCarSlot].transform.GetChild(1).gameObject.SetActive(false);

            if(currentCarSlot == 2)
            {
                RaiseCameraPosition();
            }


            currentCarSlot++;
            try
            {
                carSlots[currentCarSlot].gameObject.SetActive(true);
                carSlots[currentCarSlot].transform.GetChild(1).gameObject.SetActive(true);
            }
            catch
            {

            }
        }
    }

    public void RaiseCameraPosition()
    {
        gameCamera.DOMoveY(6, 1);
    }
}
