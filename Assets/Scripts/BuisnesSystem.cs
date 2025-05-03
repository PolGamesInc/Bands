using TMPro;
using UnityEngine;

public class BuisnesSystem : MonoBehaviour
{
    private float DillerMoneyAccumulated = 0f;
    private int CountDillerMoney = 0;
    [SerializeField] private TextMeshPro CountDillerMoneyText;

    private void Start()
    {
        CountDillerMoney = Mathf.Clamp(CountDillerMoney, 0, 16000);
    }

    private void Update()
    {
        CountDillerMoneyText.text = CountDillerMoney.ToString() + " можно отмыть";
        DillerMoneyAccumulated += 1 * Time.deltaTime;
        CountDillerMoney = Mathf.FloorToInt(DillerMoneyAccumulated);
    }

    public void GetCash()
    {
        gameObject.GetComponent<MoneySystem>().CountCash += CountDillerMoney;
        DillerMoneyAccumulated = 0;
    }
}
