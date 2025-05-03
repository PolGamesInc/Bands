using TMPro;
using UnityEngine;

public class ShootSystem : MonoBehaviour
{
    private bool IsShootingAk;
    private bool IsShootingPistol;
    private float RangeShoot = 1000f;
    private AudioSource ShootAS;
    private AudioSource ReloadAKAS;
    private AudioSource NoneCountShootAS;

    [SerializeField] private float TimeNextShoot = 0f;
    [SerializeField] private float FireRate = 1f;
    [SerializeField] private GameObject FPSCamera;

    public Vector3 PositionOffset;
    public Vector3 RotationOffset;

    [SerializeField] private AudioClip ShootAudioClip;

    [SerializeField] private int CountShootStoreAk;
    [SerializeField] private int CountShootAk;
    [SerializeField] private TextMeshProUGUI CountShootStoreAkTMP;
    [SerializeField] private TextMeshProUGUI CountShootAkTMP;

    [SerializeField] private int CountShootStorePistol;
    [SerializeField] private int CountShootPistol;
    [SerializeField] private TextMeshProUGUI CountShootStorePistolTMP;
    [SerializeField] private TextMeshProUGUI CountShootPistolTMP;

    [SerializeField] private AudioClip ReloadAKSound;
    [SerializeField] private AudioClip NoneCountShootSound;

    private void Start()
    {
        ShootAS = gameObject.AddComponent<AudioSource>();
        ShootAS.clip = ShootAudioClip;

        ReloadAKAS = gameObject.AddComponent<AudioSource>();
        ReloadAKAS.clip = ReloadAKSound;

        NoneCountShootAS = gameObject.AddComponent<AudioSource>();
        NoneCountShootAS.clip = NoneCountShootSound;
    }

    private void Update()
    {
        gameObject.GetComponent<InteractiveSystems>().CurrenItemInArms.transform.position = FPSCamera.transform.position + FPSCamera.transform.TransformVector(PositionOffset);
        gameObject.GetComponent<InteractiveSystems>().CurrenItemInArms.transform.rotation = FPSCamera.transform.rotation * Quaternion.Euler(RotationOffset);

        if(gameObject.GetComponent<InteractiveSystems>().CurrenItemInArms.name == "Pistol")
        {
            gameObject.GetComponent<InteractiveSystems>().PistolInArms.SetActive(true);
            CountShootStorePistolTMP.text = CountShootStorePistol.ToString();
            CountShootPistolTMP.text = CountShootPistol.ToString() + "/";
        }
        if (gameObject.GetComponent<InteractiveSystems>().CurrenItemInArms.name != "Pistol")
        {
            gameObject.GetComponent<InteractiveSystems>().PistolInArms.SetActive(false);
            CountShootStorePistolTMP.text = "";
            CountShootPistolTMP.text = "";
        }

        if (gameObject.GetComponent<InteractiveSystems>().CurrenItemInArms.name == "AK")
        {
            gameObject.GetComponent<InteractiveSystems>().AkInArms.SetActive(true);
            CountShootStoreAkTMP.text = CountShootStoreAk.ToString();
            CountShootAkTMP.text = CountShootAk.ToString() + "/";
        }
        if(gameObject.GetComponent<InteractiveSystems>().CurrenItemInArms.name != "AK")
        {
            gameObject.GetComponent<InteractiveSystems>().AkInArms.SetActive(false);
            CountShootStoreAkTMP.text = "";
            CountShootAkTMP.text = "";
        }

        ///////////////

        if (CountShootAk <= 0)
        {
            IsShootingAk = false;
            if (Input.GetMouseButton(0) && Time.time >= TimeNextShoot)
            {
                NoneCountShootAS.Play();
            }
        }
        else
        {
            IsShootingAk = true;
        }

        if(CountShootPistol <= 0)
        {
            IsShootingPistol = false;
            if(Input.GetMouseButton(0) && Time.time >= TimeNextShoot)
            {
                NoneCountShootAS.Play();
            }
        }
        else
        {
            IsShootingPistol = true;
        }


        if (gameObject.GetComponent<InteractiveSystems>().CurrenItemInArms.name == "AK")
        {
            FireRate = 10f;
            CountShootStoreAk = 30;
            if (IsShootingAk == true)
            {
                if (Input.GetMouseButton(0) && Time.time >= TimeNextShoot)
                {
                    TimeNextShoot = Time.time + 1f / FireRate;
                    ShootAK();
                }
            }
            ReloadAk();
        }

        if (gameObject.GetComponent<InteractiveSystems>().CurrenItemInArms.name == "Pistol")
        {
            CountShootStorePistol = 12;
            FireRate = 3f;
            if (IsShootingPistol == true)
            {
                if (Input.GetMouseButton(0) && Time.time >= TimeNextShoot)
                {
                    TimeNextShoot = Time.time + 1f / FireRate;
                    ShootPistol();
                }
            }
            ReloadPistol();
        }
    }

    private void ShootPistol()
    {
        CountShootPistol--;
        RaycastHit Hit;
        if (Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out Hit, RangeShoot))
        {
            ShootAS.Play();
        }
        else
        {
            ShootAS.Play();
        }
    }

    private void ShootAK()
    {
        CountShootAk--;
        RaycastHit Hit;
        if(Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward,out Hit, RangeShoot))
        {
            ShootAS.Play();
        }
        else
        {
            ShootAS.Play();
        }
    }

    private void ReloadAk()
    {
        if (CountShootAk < CountShootStoreAk)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                CountShootAk = CountShootStoreAk;
                ReloadAKAS.Play();
            }
        }
    }

    private void ReloadPistol()
    {
        if (CountShootPistol < CountShootStorePistol)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                CountShootPistol = CountShootStorePistol;
                ReloadAKAS.Play();
            }
        }
    }
}
