using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;  // ใช้สำหรับตรวจสอบรูปแบบ email และเบอร์โทร

public class TheEnd : MonoBehaviour
{
    [SerializeField] private TMP_InputField enterName, enterNumber;
    [SerializeField] private TMP_Text debugText;
    [SerializeField] private Button button;
    [SerializeField] private LeaderBoard leaderBoard;
    [SerializeField] private Coins_Bag coin_bag;

    private bool isShowingError = false; // ตัวแปรเพื่อเช็คว่า Debug Text กำลังแสดงข้อความอยู่หรือไม่

    public void EnterConfirm()
    {
        // เช็คว่า input fields ทั้งหมดไม่เป็นค่าว่าง
        if (string.IsNullOrWhiteSpace(enterName.text) || string.IsNullOrWhiteSpace(enterNumber.text))
        {
            ShowErrorText("Please fill in all fields.");
            return;
        }

        // เช็ครูปแบบหมายเลขโทรศัพท์ (เบอร์โทรต้องเป็นตัวเลข 10 หลัก)
        if (!IsValidPhoneNumber(enterNumber.text))
        {
            ShowErrorText("Invalid phone number.");
            return;
        }

        // ถ้าทุกอย่างถูกต้อง
        EnterName(enterName.text,enterNumber.text);
    }

    void EnterName(string userName,string number)
    {
        leaderBoard.gameObject.SetActive(true);
        leaderBoard.GetLeaderStat(userName, number, coin_bag.GetCurrentCoins());
        gameObject.SetActive(false);
    }

    private void ShowErrorText(string text)
    {
        // เช็คว่า Debug Text กำลังแสดงอยู่หรือไม่
        if (isShowingError) return;

        isShowingError = true; // เริ่มแสดงข้อความ
        StartCoroutine(ErrorText(text));
    }

    private IEnumerator ErrorText(string text)
    {
        debugText.text = text;
        yield return new WaitForSeconds(2);
        debugText.text = "";
        isShowingError = false; // รีเซ็ตสถานะหลังจากข้อความหายไป
    }


    // ฟังก์ชันตรวจสอบหมายเลขโทรศัพท์ (เบอร์โทร 10 หลัก)
    private bool IsValidPhoneNumber(string number)
    {
        // ตรวจสอบให้เบอร์โทรเป็นตัวเลข 10 หลัก
        return Regex.IsMatch(number, @"^\d{10}$");
    }
}
