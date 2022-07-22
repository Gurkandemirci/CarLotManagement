using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarManager : MonoBehaviour
{
    List<Vector3> levelCoords = new List<Vector3>();
    Tween curPath = null;

    public Canvas moneyCanvas;

    public GameObject carSlot;
    public GameObject parkSlot;

    public GameObject rightNode;
    public GameObject leftNode;
    public AudioSource carHorn;
    public TMP_Text moneyText;
    [SerializeField] Transform timerImageTransform;


    public bool isInRight = false;
    public bool isClicked = false;
    public bool isInRoad = false;
    public bool isDisabled = false;
    public bool isGoingToDestroy = false;

    public int carMoney;

    void Start()
    {
        carMoney = Random.Range(150, 200);
        moneyText.text = carMoney.ToString();
        carHorn = GetComponent<AudioSource>();
    }

    public void addToPath(GameObject selectedSlot)
    {
        isInRoad = true;
        moneyCanvas.enabled = false;

        isClicked = true;
        levelCoords.Add(transform.GetChild(0).position);

        if (selectedSlot.GetComponent<ParkSlotManager>().myObjectType == ObjectTypes.rightParkSlot)
        {
            levelCoords.Add(rightNode.transform.position);
            isInRight = true;
        }
        else
            levelCoords.Add(leftNode.transform.position);
        

        levelCoords.Add(selectedSlot.transform.GetChild(0).position);
        levelCoords.Add(selectedSlot.transform.GetChild(1).position);

        var waypointArray = levelCoords.ToArray();
        

        curPath = transform.DOPath(waypointArray, 5, PathType.CatmullRom).SetLookAt(0.01f);
        selectedSlot.transform.gameObject.GetComponent<ParkSlotManager>().DefineCarToSlot();
        carSlot.GetComponent<CarSlotManager>().isEmpty = true;


        curPath.SetEase(Ease.Linear);
        curPath.Play();

        curPath.OnComplete(() => { selectedSlot.gameObject.GetComponent<ParkSlotManager>().StartTimer(gameObject); });



    }

    public void TakeOutAndDestroy()
    {
        isGoingToDestroy = true;
        transform.gameObject.GetComponent<EnumManager>().myObjectType = ObjectTypes.unclickableCar;
        if (isInRoad)
        {
            if(isInRight)
                transform.DOLocalMoveX(5, 4).OnComplete(() => { Destroy(gameObject); });
            else
                transform.DOLocalMoveX(-5, 4).OnComplete(() => { Destroy(gameObject); });
        }
            
        else
            transform.DOLocalMoveZ(-5, 4).OnComplete(() => { Destroy(gameObject); });
    }

    public void StartTimer(GameObject carSlot)
    {
        StartCoroutine(DrawTimer(carSlot));
    }

    IEnumerator DrawTimer(GameObject carSlot)
    {
        yield return new WaitForSeconds(5);
        if (!isInRoad)
        {
            timerImageTransform.gameObject.GetComponent<Image>().DOFillAmount(1, 6);
            timerImageTransform.gameObject.GetComponent<Image>().DOColor(Color.red,6f);
            yield return new WaitForSeconds(3.25f);

            if (!isInRoad)
            {
                carHorn.Play();
                transform.GetChild(7).gameObject.SetActive(true);
                yield return new WaitForSeconds(0.25f);
                transform.GetChild(7).gameObject.SetActive(false);
                yield return new WaitForSeconds(0.25f);
                transform.GetChild(7).gameObject.SetActive(true);
                yield return new WaitForSeconds(0.25f);
                transform.GetChild(7).gameObject.SetActive(false);
            }
            

            yield return new WaitForSeconds(1);

            
            if (!isInRoad)
            {
                carSlot.GetComponent<CarSlotManager>().isEmpty = true;
                TakeOutAndDestroy();
                timerImageTransform.gameObject.GetComponent<Image>().enabled = false;
            }
        }


    }
    
}
