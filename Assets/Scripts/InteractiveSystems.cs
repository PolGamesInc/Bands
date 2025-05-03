using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class InteractiveSystems : MonoBehaviour
{
    [SerializeField] private GameObject TMPOpenToPressE;
    [SerializeField] private GameObject TMPCloseToPressESC;
    [SerializeField] private GameObject SlotsBigChair;
    [SerializeField] private GameObject SlotsLocker;
    [SerializeField] private Volume SkyAndFogVolume;
    [SerializeField] private GameObject PlayerCamera;
    [SerializeField] private GameObject Aim;
    [SerializeField] private Animator OpenDoor;
    [SerializeField] private Animator CloseDoor;
    [SerializeField] private AudioClip OpenDoorSound;
    [SerializeField] private GameObject CashOnCard;
    [SerializeField] private GameObject AkItemPrefab;
    [SerializeField] private GameObject PistolItemPrefab;
    [SerializeField] private GameObject AkItemDestroy;
    [SerializeField] private GameObject PistolItemDestroy;
    [SerializeField] private GameObject CountPay;
    [SerializeField] private TextMeshPro CountPayText;
    [SerializeField] private GameObject CountDillerMoney;

    //
    public List<GameObject> CurrenArmsSlotsGO = new List<GameObject>();
    [HideInInspector]
    public GameObject CurrenItemInArms;
    public GameObject AkInArms;
    public GameObject PistolInArms;
    private int CurrentIndexToList;
    [SerializeField] private GameObject AkImage0;
    [SerializeField] private GameObject AkImage1;
    [SerializeField] private GameObject PistolImage0;
    [SerializeField] private GameObject PistolImage1;
    public bool IsDroping;


    public TMP_InputField InputFieldCashOnCard;

    private AudioSource OpenDoorAS;

    private DepthOfField depthOfField;
    private Rigidbody PlayerRigidbody;

    public bool IsSlotsArmsActive;

    private void Awake()
    {
        CurrenArmsSlotsGO = new List<GameObject>
        {
            new GameObject("Empty"),
            new GameObject("Empty"),
            new GameObject("Empty"),
            new GameObject("Empty"),
            new GameObject("Empty"),
            new GameObject("Empty"),
            new GameObject("Empty"),
            new GameObject("Empty")
        };
    }

    private void Start()
    {
        TMPOpenToPressE.SetActive(false);
        SlotsBigChair.SetActive(false);
        SlotsLocker.SetActive(false);
        TMPCloseToPressESC.SetActive(false);
        Aim.SetActive(true);
        PlayerRigidbody = GetComponent<Rigidbody>();

        OpenDoorAS = gameObject.AddComponent<AudioSource>();

        OpenDoorAS.clip = OpenDoorSound;

        CashOnCard.SetActive(false);
        InputFieldCashOnCard.text = "";
        IsSlotsArmsActive = true;

        AkInArms.SetActive(false);
        PistolInArms.SetActive(false);

        AkImage0.SetActive(false);
        AkImage1.SetActive(false);
        PistolImage0.SetActive(false);
        PistolImage1.SetActive(false);

        CountDillerMoney.SetActive(false);

        IsDroping = true;
    }

    private void Update()
    {
        Camera CameraFirstPersonController = Camera.main;

        AkItemDestroy = GameObject.Find("AKItem");
        PistolItemDestroy = GameObject.Find("PistolItem");

        //проверка инвентаря
        if (CurrenArmsSlotsGO[0].name == "Empty")
        {
            CurrentIndexToList = 0;
            AkImage0.SetActive(false);
            PistolImage0.SetActive(false);
        }
        if (CurrenArmsSlotsGO[0].name != "Empty")
        {
            CurrentIndexToList = 1;
            if (CurrenArmsSlotsGO[0].name == "AK")
            {
                AkImage0.SetActive(true);
                PistolImage0.SetActive(false);
            }
            if(CurrenArmsSlotsGO[0].name == "Pistol")
            {
                AkImage0.SetActive(false);
                PistolImage0.SetActive(true);
            }
        }
        if (CurrenArmsSlotsGO[1].name != "Empty" && CurrenArmsSlotsGO[0].name == "Empty")
        {
            CurrentIndexToList = 0;
        }
        if (CurrenArmsSlotsGO[1].name == "Empty" && CurrenArmsSlotsGO[0].name == "Empty")
        {
            CurrentIndexToList = 0;
        }
        if(CurrenArmsSlotsGO[1].name != "Empty")
        {
            if (CurrenArmsSlotsGO[1].name == "AK")
            {
                AkImage1.SetActive(true);
                PistolImage1.SetActive(false);
            }
            if(CurrenArmsSlotsGO[1].name == "Pistol")
            {
                PistolImage1.SetActive(true);
                AkImage1.SetActive(false);
            }
        }
        if(CurrenArmsSlotsGO[1].name == "Empty")
        {
            PistolImage1.SetActive(false);
            AkImage1.SetActive(false);
        }

        if (IsDroping == true)
        {
            //DropAkItem
            if (gameObject.GetComponent<InventorySystem>().CurrenArmsSlots == 1)
            {
                if (CurrenArmsSlotsGO[0].name == "AK")
                {
                    if (Input.GetKeyDown(KeyCode.G))
                    {
                        CurrenArmsSlotsGO[0] = GameObject.Find("Empty");
                        Vector3 Direction = PlayerCamera.transform.forward;
                        GameObject AkItem = Instantiate(AkItemPrefab, PlayerCamera.transform.position, PlayerCamera.transform.rotation);
                        Rigidbody AkItemRigidbody = AkItem.GetComponent<Rigidbody>();
                        AkItemRigidbody.AddForce(Direction * 10f, ForceMode.Impulse);
                        AkItem.name = "AKItem";
                    }
                }
            }

            if (gameObject.GetComponent<InventorySystem>().CurrenArmsSlots == 2)
            {
                if (CurrenArmsSlotsGO[1].name == "AK")
                {
                    if (Input.GetKeyDown(KeyCode.G))
                    {
                        CurrenArmsSlotsGO[1] = GameObject.Find("Empty");
                        Vector3 Direction = PlayerCamera.transform.forward;
                        GameObject AkItem = Instantiate(AkItemPrefab, PlayerCamera.transform.position, PlayerCamera.transform.rotation);
                        Rigidbody AkItemRigidbody = AkItem.GetComponent<Rigidbody>();
                        AkItemRigidbody.AddForce(Direction * 10f, ForceMode.Impulse);
                        AkItem.name = "AKItem";
                    }
                }
            }
            //DropAkItem

            //DropPistolItem
            if (gameObject.GetComponent<InventorySystem>().CurrenArmsSlots == 1)
            {
                if (CurrenArmsSlotsGO[0].name == "Pistol")
                {
                    if (Input.GetKeyDown(KeyCode.G))
                    {
                        CurrenArmsSlotsGO[0] = GameObject.Find("Empty");
                        Vector3 Direction = PlayerCamera.transform.forward;
                        GameObject PistolItem = Instantiate(PistolItemPrefab, PlayerCamera.transform.position, PlayerCamera.transform.rotation);
                        Rigidbody PistolItemRigidbody = PistolItem.GetComponent<Rigidbody>();
                        PistolItemRigidbody.AddForce(Direction * 10f, ForceMode.Impulse);
                        PistolItem.name = "PistolItem";
                    }
                }
            }

            if (gameObject.GetComponent<InventorySystem>().CurrenArmsSlots == 2)
            {
                if (CurrenArmsSlotsGO[1].name == "Pistol")
                {
                    if (Input.GetKeyDown(KeyCode.G))
                    {
                        CurrenArmsSlotsGO[1] = GameObject.Find("Empty");
                        Vector3 Direction = PlayerCamera.transform.forward;
                        GameObject PistolItem = Instantiate(PistolItemPrefab, PlayerCamera.transform.position, PlayerCamera.transform.rotation);
                        Rigidbody PistolItemRigidbody = PistolItem.GetComponent<Rigidbody>();
                        PistolItemRigidbody.AddForce(Direction * 10f, ForceMode.Impulse);
                        PistolItem.name = "PistolItem";
                    }
                }
            }
            //DropPistolItem
        }

        if (CashOnCard.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                gameObject.GetComponent<MoneySystem>().CashOnCard();
            }
        }

        if (CameraFirstPersonController != null)
        {
            Vector3 Direction = CameraFirstPersonController.transform.forward;
            Vector3 Origin = CameraFirstPersonController.transform.position;

            RaycastHit Hit;
            if (Physics.Raycast(Origin, Direction, out Hit))
            {
                if (Hit.collider.tag == "BigChair")
                {
                    if (Hit.distance <= 4f)
                    {
                        TMPOpenToPressE.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            IsDroping = false;
                            SlotsBigChair.SetActive(true);
                            TMPCloseToPressESC.SetActive(true);
                            Aim.SetActive(false);
                            PlayerCamera.GetComponent<FirstPersonLook>().enabled = false;
                            gameObject.GetComponent<FirstPersonMovement>().enabled = false;
                            gameObject.GetComponent<Jump>().enabled = false;
                            gameObject.GetComponent<Crouch>().enabled = false;
                            PlayerRigidbody.constraints |= RigidbodyConstraints.FreezePosition;
                            PlayerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                            gameObject.GetComponent<ShootSystem>().enabled = false;
                            UnlockCursor();
                            if (SkyAndFogVolume.profile.TryGet<DepthOfField>(out depthOfField))
                            {
                                depthOfField.active = true;
                            }
                        }
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            IsDroping = true;
                            SlotsBigChair.SetActive(false);
                            TMPCloseToPressESC.SetActive(false);
                            Aim.SetActive(true);
                            LockCursor();
                            PlayerCamera.GetComponent<FirstPersonLook>().enabled = true;
                            gameObject.GetComponent<FirstPersonMovement>().enabled = true;
                            gameObject.GetComponent<Jump>().enabled = true;
                            gameObject.GetComponent<Crouch>().enabled = true;
                            PlayerRigidbody.constraints &= ~RigidbodyConstraints.FreezePosition;
                            PlayerRigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationY;
                            gameObject.GetComponent<ShootSystem>().enabled = true;
                            if (SkyAndFogVolume.profile.TryGet<DepthOfField>(out depthOfField))
                            {
                                depthOfField.active = false;
                            }
                        }
                    }
                    else
                    {
                        TMPOpenToPressE.SetActive(false);
                    }
                }
                else if (Hit.collider.tag == "Untagged")
                {
                    TMPOpenToPressE.SetActive(false);
                }

                if (Hit.collider.tag == "Locker")
                {
                    if (Hit.distance <= 4f)
                    {
                        TMPOpenToPressE.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            IsDroping = false;
                            SlotsLocker.SetActive(true);
                            TMPCloseToPressESC.SetActive(true);
                            Aim.SetActive(false);
                            PlayerCamera.GetComponent<FirstPersonLook>().enabled = false;
                            gameObject.GetComponent<FirstPersonMovement>().enabled = false;
                            gameObject.GetComponent<Jump>().enabled = false;
                            gameObject.GetComponent<Crouch>().enabled = false;
                            PlayerRigidbody.constraints |= RigidbodyConstraints.FreezePosition;
                            PlayerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                            gameObject.GetComponent<ShootSystem>().enabled = false;
                            UnlockCursor();
                            if (SkyAndFogVolume.profile.TryGet<DepthOfField>(out depthOfField))
                            {
                                depthOfField.active = true;
                            }
                        }
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            IsDroping = true;
                            SlotsLocker.SetActive(false);
                            TMPCloseToPressESC.SetActive(false);
                            Aim.SetActive(true);
                            LockCursor();
                            PlayerCamera.GetComponent<FirstPersonLook>().enabled = true;
                            gameObject.GetComponent<FirstPersonMovement>().enabled = true;
                            gameObject.GetComponent<Jump>().enabled = true;
                            gameObject.GetComponent<Crouch>().enabled = true;
                            PlayerRigidbody.constraints &= ~RigidbodyConstraints.FreezePosition;
                            PlayerRigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationY;
                            gameObject.GetComponent<ShootSystem>().enabled = true;
                            if (SkyAndFogVolume.profile.TryGet<DepthOfField>(out depthOfField))
                            {
                                depthOfField.active = false;
                            }
                        }
                    }
                    else
                    {
                        TMPOpenToPressE.SetActive(false);
                    }
                }
                else if (Hit.collider.tag == "Untagged")
                {
                    TMPOpenToPressE.SetActive(false);
                }

                if (Hit.collider.tag == "Door")
                {
                    if (Hit.distance <= 4f)
                    {
                        TMPOpenToPressE.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            OpenDoor.SetTrigger("PlayAnimation");
                            OpenDoorAS.Play();
                        }
                    }
                    else
                    {
                        TMPOpenToPressE.SetActive(false);
                    }
                }
                else if (Hit.collider.tag == "Untagged")
                {
                    TMPOpenToPressE.SetActive(false);
                }

                if (Hit.collider.tag == "ATM")
                {
                    if (Hit.distance <= 4f)
                    {
                        TMPOpenToPressE.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            IsDroping = false;
                            IsSlotsArmsActive = false;
                            CashOnCard.SetActive(true);
                            Aim.SetActive(false);
                            TMPCloseToPressESC.SetActive(true);
                            PlayerCamera.GetComponent<FirstPersonLook>().enabled = false;
                            gameObject.GetComponent<FirstPersonMovement>().enabled = false;
                            gameObject.GetComponent<Jump>().enabled = false;
                            gameObject.GetComponent<Crouch>().enabled = false;
                            PlayerRigidbody.constraints |= RigidbodyConstraints.FreezePosition;
                            PlayerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                            gameObject.GetComponent<ShootSystem>().enabled = false;
                            UnlockCursor();
                            if (SkyAndFogVolume.profile.TryGet<DepthOfField>(out depthOfField))
                            {
                                depthOfField.active = true;
                            }
                        }
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            IsDroping = true;
                            IsSlotsArmsActive = true;
                            CashOnCard.SetActive(false);
                            TMPCloseToPressESC.SetActive(false);
                            Aim.SetActive(true);
                            LockCursor();
                            PlayerCamera.GetComponent<FirstPersonLook>().enabled = true;
                            gameObject.GetComponent<FirstPersonMovement>().enabled = true;
                            gameObject.GetComponent<Jump>().enabled = true;
                            gameObject.GetComponent<Crouch>().enabled = true;
                            PlayerRigidbody.constraints &= ~RigidbodyConstraints.FreezePosition;
                            PlayerRigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationY;
                            gameObject.GetComponent<ShootSystem>().enabled = true;
                            InputFieldCashOnCard.text = "";
                            if (SkyAndFogVolume.profile.TryGet<DepthOfField>(out depthOfField))
                            {
                                depthOfField.active = false;
                            }
                        }
                    }
                    else
                    {
                        TMPOpenToPressE.SetActive(false);
                    }
                }
                else if (Hit.collider.tag == "Untagged")
                {
                    TMPOpenToPressE.SetActive(false);
                }

                if(Hit.collider.tag == "Shop")
                {
                    if(Hit.distance <= 4f)
                    {
                        TMPOpenToPressE.SetActive(true);
                        CountPay.SetActive(true);
                        CountPayText.text = gameObject.GetComponent<MoneySystem>().PriceCard.ToString() + " к оплате";
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            gameObject.GetComponent<MoneySystem>().ByCard();
                            CountPay.SetActive(false);
                        }
                    }
                    else
                    {
                        TMPOpenToPressE.SetActive(false);
                        CountPay.SetActive(false);
                    }
                }
                else if (Hit.collider.tag == "Untagged")
                {
                    TMPOpenToPressE.SetActive(false);
                    CountPay.SetActive(false);
                }

                if(Hit.collider.name == "AKItem")
                {
                    if(Hit.distance <= 4f)
                    {
                        TMPOpenToPressE.SetActive(true);
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            CurrenItemInArms = AkInArms;
                            AkInArms.SetActive(true);
                            ReplaceGameObjectAtIndex(CurrentIndexToList, CurrenItemInArms);
                            Destroy(AkItemDestroy);
                        }
                    }
                }

                if(Hit.collider.name == "PistolItem")
                {
                    if(Hit.distance <= 4f)
                    {
                        TMPOpenToPressE.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            CurrenItemInArms = PistolInArms;
                            PistolInArms.SetActive(true);
                            ReplaceGameObjectAtIndex(CurrentIndexToList, CurrenItemInArms);
                            Destroy(PistolItemDestroy);
                        }
                    }
                }

                if(Hit.collider.tag == "Diller")
                {
                    if(Hit.distance <= 4f)
                    {
                        CountDillerMoney.SetActive(true);
                        TMPOpenToPressE.SetActive(true);
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            gameObject.GetComponent<BuisnesSystem>().GetCash();
                        }
                    }
                    else
                    {
                        CountDillerMoney.SetActive(false);
                        TMPOpenToPressE.SetActive(false);
                    }
                }
                else if (Hit.collider.tag == "Untagged")
                {
                    CountDillerMoney.SetActive(false);
                    TMPOpenToPressE.SetActive(false);
                }

                if(Hit.collider.tag == "ShopAssortment")
                {
                    if(Hit.distance <= 4f)
                    {
                        TMPOpenToPressE.SetActive(true);
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            print("Open!");
                        }
                    }
                }
            }
            else
            {
                CashOnCard.SetActive(false);
                TMPOpenToPressE.SetActive(false);
                SlotsBigChair.SetActive(false);
                SlotsLocker.SetActive(false);
                TMPCloseToPressESC.SetActive(false);
                PlayerCamera.GetComponent<FirstPersonLook>().enabled = true;
                if (SkyAndFogVolume.profile.TryGet<DepthOfField>(out depthOfField))
                {
                    depthOfField.active = false;
                }
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

    private void ReplaceGameObjectAtIndex(int index, GameObject newObject)
    {
        if(index >= 0 && index < CurrenArmsSlotsGO.Count)
        {
            CurrenArmsSlotsGO[index] = CurrenItemInArms;
        }

        if (CurrenArmsSlotsGO[0].name == "Empty")
        {
            index = 0;
            CurrentIndexToList = 0;
        }
        if(CurrenArmsSlotsGO[0].name != "Empty")
        {
            index = 1;
            CurrentIndexToList = 1;
        }
        if(CurrenArmsSlotsGO[1].name != "Empty" && CurrenArmsSlotsGO[0].name == "Empty")
        {
            index = 0;
            CurrentIndexToList = 0;
        }
        if(CurrenArmsSlotsGO[1].name == "Empty" && CurrenArmsSlotsGO[0].name == "Empty")
        {
            index = 0;
            CurrentIndexToList = 0;
        }
    }
}
