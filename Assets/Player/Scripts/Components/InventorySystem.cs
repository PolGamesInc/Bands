using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class InventorySystem : MonoBehaviour
{
    //Base
    [SerializeField] private GameObject Slots;
    [SerializeField] GameObject PlayerCamera;

    //Graphis
    private DepthOfField depthOfField;
    [SerializeField] private Volume SkyAndFogVolume;

    //Audio
    private AudioSource OpenInventoryAS;
    private AudioSource CloseInventoryAS;
    [SerializeField] private AudioClip OpenInventory;
    [SerializeField] private AudioClip CloseInventory;

    //ChangeSlotArms
    [Range(0,9)]
    public float CurrenArmsSlots = 1f;
    [SerializeField] private Image[] ArmsSlots = new Image[8];


    private void Start()
    {
        Slots.SetActive(false);
        Cursor.visible = false;

        OpenInventoryAS = gameObject.AddComponent<AudioSource>();
        OpenInventoryAS.clip = OpenInventory;

        CloseInventoryAS = gameObject.AddComponent<AudioSource>();
        CloseInventoryAS.clip = CloseInventory;
    }

    private void Update()
    { 
        switch(CurrenArmsSlots)
        {
            case 1:
                gameObject.GetComponent<InteractiveSystems>().CurrenItemInArms = gameObject.GetComponent<InteractiveSystems>().CurrenArmsSlotsGO[0];
                break;
            case 2:
                gameObject.GetComponent<InteractiveSystems>().CurrenItemInArms = gameObject.GetComponent<InteractiveSystems>().CurrenArmsSlotsGO[1];
                break;
            case 3:
                gameObject.GetComponent<InteractiveSystems>().CurrenItemInArms = gameObject.GetComponent<InteractiveSystems>().CurrenArmsSlotsGO[2];
                break;
            case 4:
                gameObject.GetComponent<InteractiveSystems>().CurrenItemInArms = gameObject.GetComponent<InteractiveSystems>().CurrenArmsSlotsGO[3];
                break;
            case 5:
                gameObject.GetComponent<InteractiveSystems>().CurrenItemInArms = gameObject.GetComponent<InteractiveSystems>().CurrenArmsSlotsGO[4];
                break;
            case 6:
                gameObject.GetComponent<InteractiveSystems>().CurrenItemInArms = gameObject.GetComponent<InteractiveSystems>().CurrenArmsSlotsGO[5];
                break;
            case 7:
                gameObject.GetComponent<InteractiveSystems>().CurrenItemInArms = gameObject.GetComponent<InteractiveSystems>().CurrenArmsSlotsGO[6];
                break;
            case 8:
                gameObject.GetComponent<InteractiveSystems>().CurrenItemInArms = gameObject.GetComponent<InteractiveSystems>().CurrenArmsSlotsGO[7];
                break;
        }

        if (gameObject.GetComponent<InteractiveSystems>().IsSlotsArmsActive == true)
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                CurrenArmsSlots = 1;
            }
            if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                CurrenArmsSlots = 2;
            }
            if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                CurrenArmsSlots = 3;
            }
            if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                CurrenArmsSlots = 4;
            }
            if (Input.GetKeyUp(KeyCode.Alpha5))
            {
                CurrenArmsSlots = 5;
            }
            if (Input.GetKeyUp(KeyCode.Alpha6))
            {
                CurrenArmsSlots = 6;
            }
            if (Input.GetKeyUp(KeyCode.Alpha7))
            {
                CurrenArmsSlots = 7;
            }
            if (Input.GetKeyUp(KeyCode.Alpha8))
            {
                CurrenArmsSlots = 8;
            }
        }

        CurrenArmsSlots = Mathf.Clamp(CurrenArmsSlots, 1f, 8f);
        CurrenArmsSlots += Input.mouseScrollDelta.y * -1f;

        if(CurrenArmsSlots == 0)
        {
            CurrenArmsSlots = 8;
        }
        else if(CurrenArmsSlots == 9)
        {
            CurrenArmsSlots = 1;
        }

            for (int i = 0; i < ArmsSlots.Length; i++)
            {
                if (i == (int)(CurrenArmsSlots - 1))
                {
                    SetTranperentArmsSlot(i, 150);
                }
                else
                {
                    SetTranperentArmsSlot(i, 56);
                }
            }

        if (Input.GetKeyUp(KeyCode.I))
        {
            if (Cursor.visible == false)
            {
                Slots.SetActive(true);
                UnlockCursor();
                PlayerCamera.GetComponent<FirstPersonLook>().enabled = false;
                OpenInventoryAS.Play();
                if (SkyAndFogVolume.profile.TryGet<DepthOfField>(out depthOfField))
                {
                    depthOfField.active = true;
                }
            }
        }
        if (Cursor.visible == true)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                LockCursor();
                PlayerCamera.GetComponent<FirstPersonLook>().enabled = true;
                if (SkyAndFogVolume.profile.TryGet<DepthOfField>(out depthOfField))
                {
                    depthOfField.active = false;
                }
                Slots.SetActive(false);
                CloseInventoryAS.Play();
            }
        }
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void SetTranperentArmsSlot(int index,int alpha)
    {
        alpha = Mathf.Clamp(alpha, 0, 255);
        Color colorImage = ArmsSlots[index].color;
        colorImage.a = alpha / 255f;
        ArmsSlots[index].color = colorImage;
    }
}
