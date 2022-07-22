using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public ObjectTypes myObjectType;

    // objelere enum manageri ekle
    
    [SerializeField]
    private GameObject selectedSlot;

    [SerializeField]
    private CarManager carManager;

    private GameObject selectedCar;

    EnumManager myEnumManager;
   
 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickObject();
        }
    }

    public void ClickObject()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            myEnumManager = hitInfo.transform.gameObject.GetComponent<EnumManager>();
            if (myEnumManager != null)
            {
               
                if (myEnumManager.myObjectType == ObjectTypes.car)
                {
                    if (selectedCar != null)
                    {
                        selectedCar.GetComponent<Outline>().enabled = false;
                    }

                    selectedCar = hitInfo.transform.gameObject;
                    selectedCar.GetComponent<Outline>().enabled = true;
                }

                else if (myEnumManager.myObjectType == ObjectTypes.leftParkSlot ||
                        myEnumManager.myObjectType == ObjectTypes.rightParkSlot)
                {

                    selectedSlot = hitInfo.transform.parent.parent.gameObject;
                    if (!selectedSlot.GetComponent<ParkSlotManager>().isEmpty)
                    {
                        SetClickColor(Color.red);
                    }
                    

                    carManager = selectedCar.GetComponent<CarManager>();


                    if (!carManager.isClicked && selectedSlot.gameObject.GetComponent<ParkSlotManager>().isEmpty && !carManager.isGoingToDestroy)
                    {
                        if (!(carManager.isDisabled && !selectedSlot.GetComponent<ParkSlotManager>().isDisabled))
                        {
                            SetClickColor(Color.green);
                            carManager.carSlot.GetComponent<CarSlotManager>().isEmpty = true;
                            selectedCar.GetComponent<Outline>().enabled = false;
                            carManager.addToPath(selectedSlot);
                        }
                        else
                        {
                            SetClickColor(Color.red);
                        }

                    }
                }
            }
            
        }
    }

    void SetClickColor(Color color)
    {
        selectedSlot.transform.GetChild(2).GetChild(0).GetComponent<MeshRenderer>().material.DOColor(color, 0.1f);
        selectedSlot.transform.GetChild(2).GetChild(0).GetComponent<MeshRenderer>().material.DOColor(Color.white, 0.1f).SetDelay(0.1f);
    }
    
}
