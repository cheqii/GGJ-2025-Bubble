using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SheetData", menuName = "SheetData")]
public class SheetData : ScriptableObject
{
    public List<Sprite> sheetList;
}
public class SheetReader : MonoBehaviour
{
    [Header("Sheet Data")]
    public SheetData sheetData;
    [Header("Behavior")]
    public bool sheetIsActive;
    public List<Sprite> sheetList;
    public Image sheetImage;
    public int sheetNumber;

    private void Awake()
    {
        sheetList = sheetData.sheetList;
    }

    [ContextMenu("Start Sheet")]
    public void StartSheet()
    {
        sheetImage.gameObject.SetActive(true);
        sheetImage.sprite = sheetList[sheetNumber];
        sheetIsActive = true;
    }
    private void Update()
    {
        if (sheetIsActive && Input.GetMouseButtonDown(0))
        {
            sheetNumber += 1;
            if (sheetNumber > sheetList.Count - 1)
            {
                ReadComplete();
            }
            else
            {
                sheetImage.sprite = sheetList[sheetNumber];
            }
        }
        else if (sheetIsActive && Input.GetMouseButtonDown(1))
        {
            sheetNumber -= 1;
            if (sheetNumber < 0)
            {
                Debug.Log("Can't Back");
                sheetNumber = 0;
            }
            else
            {
                sheetImage.sprite = sheetList[sheetNumber];
            }
        }
    }

    private void ReadComplete()
    {
        sheetNumber = 0;
        sheetImage.gameObject.SetActive(false);
        sheetIsActive = false;
    }
}
