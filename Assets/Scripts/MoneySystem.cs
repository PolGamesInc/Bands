using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    [SerializeField] private int CountCashOnCard;
    public int CountCash;
    [SerializeField] private int CountCard;
    [SerializeField] private TextMeshProUGUI CountCashTMP;
    [SerializeField] private TextMeshProUGUI CountCardTMP;

    public int PriceCard;

    private AudioSource PayCardAS;
    [SerializeField] private AudioClip PayCardSound;

    private AudioSource DontPayCardAS;
    [SerializeField] private AudioClip DontPayCardSound;

    private void Start()
    {
        PayCardAS = gameObject.AddComponent<AudioSource>();
        PayCardAS.clip = PayCardSound;

        DontPayCardAS = gameObject.AddComponent<AudioSource>();
        DontPayCardAS.clip = DontPayCardSound;
    }

    private void Update()
    {
        int.TryParse(gameObject.GetComponent<InteractiveSystems>().InputFieldCashOnCard.text, out CountCashOnCard);
        CountCashTMP.text = CountCash.ToString();
        CountCardTMP.text = CountCard.ToString();
    }

    public void CashOnCard()
    {
        if (CountCashOnCard <= CountCash)
        {
            CountCard += CountCashOnCard;
            CountCash -= CountCashOnCard;
        }
        else
        {
            print("не достаточно налички!");
        }
    }

    public void ByCard()
    {
        if(CountCard >= PriceCard)
        {
            CountCard -= PriceCard;
            PayCardAS.Play();
        }
        else
        {
            DontPayCardAS.Play();
            print("недостаточно средств!");
        }
    }
}
