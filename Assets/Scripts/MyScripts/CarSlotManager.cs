using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class CarSlotManager : MonoBehaviour
{
    public bool isEmpty;
    public bool isUnlocked;
    public int slotCost;
    public GameObject carClone;
    public UnlockSlotManager unlockSlotManager;
    public TMP_Text costText;

    void Start()
    {
        costText.text = slotCost.ToString();
        //StartCoroutine(FreqControl());
    }


    Coroutine carSpawner;
    void Update()
    {
        if (isEmpty && isUnlocked)
        {
            if(carSpawner==null)
            {
                carSpawner = StartCoroutine(CreateCar());
            }            
        }
    }
    void RandomSpawn()
    {

    }

    /*[System.Obsolete]
    private IEnumerator FreqControl()
    {
        while (!LevelManager.instance.isGameEnded)
        {
            if (isEmpty && isUnlocked && Random.RandomRange(0, 5000) < 2)
            {
                CreateCar();
            }

            yield return new WaitForSeconds(5);

            if(isEmpty && isUnlocked)
            {
                CreateCar();
            }
        }

    }*/

  
    IEnumerator CreateCar()
    {
        isEmpty = false;
        int waitTime = Random.Range(2, 6);
        yield return new WaitForSeconds(waitTime);
        
        GameObject newCar = Instantiate(carClone, new Vector3(transform.position.x, transform.position.y, transform.position.z - 4.5f), Quaternion.identity);
        newCar.transform.DOMoveZ(-2.5f, 4);
        if (Random.Range(0, 7) < 2 && unlockSlotManager.currentParkSlot > 1)
        {
            newCar.transform.GetComponent<CarManager>().isDisabled = true;
            newCar.transform.GetChild(6).gameObject.transform.GetChild(2).gameObject.SetActive(true);
        }
        newCar.transform.GetComponent<CarManager>().carSlot = transform.gameObject;
        newCar.transform.GetComponent<CarManager>().StartTimer(gameObject);
        carSpawner = null;
        
    }
}
