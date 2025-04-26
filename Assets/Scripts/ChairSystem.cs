using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class ChairSystem : MonoBehaviour
{
    [SerializeField] private GameObject TMPOpenToPressE;
    [SerializeField] private GameObject TMPCloseToPressESC;
    [SerializeField] private GameObject SlotsBigChair;
    [SerializeField] private GameObject SlotsLocker;
    [SerializeField] private Volume SkyAndFogVolume;
    [SerializeField] private GameObject PlayerCamera;
    [SerializeField] private GameObject Aim;

    private DepthOfField depthOfField;

    private void Start()
    {
        TMPOpenToPressE.SetActive(false);
        SlotsBigChair.SetActive(false);
        SlotsLocker.SetActive(false);
        TMPCloseToPressESC.SetActive(false);
        Aim.SetActive(true);
    }

    private void Update()
    {
        Camera CameraFirstPersonController = Camera.main;

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
                            SlotsBigChair.SetActive(true);
                            TMPCloseToPressESC.SetActive(true);
                            Aim.SetActive(false);
                            PlayerCamera.GetComponent<FirstPersonLook>().enabled = false;
                            gameObject.GetComponent<FirstPersonMovement>().enabled = false;
                            gameObject.GetComponent<Jump>().enabled = false;
                            gameObject.GetComponent<Crouch>().enabled = false;
                            UnlockCursor();
                            if (SkyAndFogVolume.profile.TryGet<DepthOfField>(out depthOfField))
                            {
                                depthOfField.active = true;
                            }
                        }
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                            SlotsBigChair.SetActive(false);
                            TMPCloseToPressESC.SetActive(false);
                            Aim.SetActive(true);
                            LockCursor();
                            PlayerCamera.GetComponent<FirstPersonLook>().enabled = true;
                            gameObject.GetComponent<FirstPersonMovement>().enabled = true;
                            gameObject.GetComponent<Jump>().enabled = true;
                            gameObject.GetComponent<Crouch>().enabled = true;
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

                if(Hit.collider.tag == "Locker")
                {
                    if(Hit.distance <= 4f)
                    {
                        TMPOpenToPressE.SetActive(true);
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            SlotsLocker.SetActive(true);
                            TMPCloseToPressESC.SetActive(true);
                            Aim.SetActive(false);
                            PlayerCamera.GetComponent<FirstPersonLook>().enabled = false;
                            gameObject.GetComponent<FirstPersonMovement>().enabled = false;
                            gameObject.GetComponent<Jump>().enabled = false;
                            gameObject.GetComponent<Crouch>().enabled = false;
                            UnlockCursor();
                            if (SkyAndFogVolume.profile.TryGet<DepthOfField>(out depthOfField))
                            {
                                depthOfField.active = true;
                            }
                        }
                        if(Input.GetKeyDown(KeyCode.Escape))
                        {
                            SlotsLocker.SetActive(false);
                            TMPCloseToPressESC.SetActive(false);
                            Aim.SetActive(true);
                            LockCursor();
                            PlayerCamera.GetComponent<FirstPersonLook>().enabled = true;
                            gameObject.GetComponent<FirstPersonMovement>().enabled = true;
                            gameObject.GetComponent<Jump>().enabled = true;
                            gameObject.GetComponent<Crouch>().enabled = true;
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
            }
            else
            {
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
}
