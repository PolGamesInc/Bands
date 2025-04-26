using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

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
    [Range(1,8)]
    [SerializeField] private float CurrenArmsSlots = 1f;
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
        CurrenArmsSlots = Mathf.Clamp(CurrenArmsSlots, 1f, 8f);
        CurrenArmsSlots += Input.mouseScrollDelta.y * 1f;
        
        for(int i = 0; i < ArmsSlots.Length; i++)
        {
            if(i == (int)(CurrenArmsSlots - 1))
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
