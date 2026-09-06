using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using System; // ✅ สำหรับ DateTime
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public string userName;
    public int coins;
    public string number;
    public string dateTime; // ✅ บันทึกเวลาเป็น string (เพื่อให้ JsonUtility รองรับ)
}

[System.Serializable]
public class PlayerDataList
{
    public List<PlayerData> players = new List<PlayerData>();
}

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private int maximumPlayer = 5;
    [SerializeField] private GameObject leaderStatsPrefab;
    [SerializeField] private GameObject leaderStatsTarget;

    private string savePathJson;
    private string savePathText;
    private PlayerDataList playerDataList = new PlayerDataList();

    void Awake()
    {
        savePathJson = Path.Combine(Application.persistentDataPath, "leaderboard.json");
        savePathText = Path.Combine(Application.persistentDataPath, "leaderboard_readable.txt");
        LoadData();

        // ✅ อัปเดตไฟล์อ่านง่ายทุกครั้งที่เปิดเกม
        SaveReadableText();
        CheckAndCreateLeaderStat();
    }


    // ✅ รับข้อมูลผู้เล่นใหม่
    public void GetLeaderStat(string _userName, string _number, int _coins)
    {
        if (string.IsNullOrEmpty(savePathJson))
            savePathJson = Path.Combine(Application.persistentDataPath, "leaderboard.json");

        PlayerData newPlayer = new PlayerData()
        {
            userName = _userName,
            coins = _coins,
            number = _number,
            dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") // ✅ เก็บเวลาปัจจุบัน
        };

        playerDataList.players.Add(newPlayer);

        // ✅ เรียงลำดับใหม่ก่อนเซฟ
        playerDataList.players = playerDataList.players
            .OrderByDescending(p => p.coins)
            .Take(maximumPlayer)
            .ToList();

        SaveData();
        SaveReadableText(); // ✅ สร้าง notepad อ่านง่าย
        CheckAndCreateLeaderStat();
    }

    // ✅ เซฟข้อมูลลงไฟล์ JSON
    private void SaveData()
    {
        string json = JsonUtility.ToJson(playerDataList, true);
        File.WriteAllText(savePathJson, json);
        Debug.Log("Saved JSON: " + savePathJson);
    }

    // ✅ สร้างไฟล์ .txt แยกไว้ให้อ่านง่าย
    private void SaveReadableText()
    {
        using (StreamWriter writer = new StreamWriter(savePathText, false))
        {
            writer.WriteLine("=== LEADERBOARD ===");

            for (int i = 0; i < playerDataList.players.Count; i++)
            {
                var p = playerDataList.players[i];
                writer.WriteLine($"#{i + 1} | {p.userName} | {p.coins} coins | {p.number} | {p.dateTime}");
            }

            writer.WriteLine("====================");
        }

        Debug.Log("Saved readable text: " + savePathText);
    }

    // ✅ โหลดข้อมูลกลับจากไฟล์ (ถ้ามี)
    private void LoadData()
    {
        if (File.Exists(savePathJson))
        {
            string json = File.ReadAllText(savePathJson);
            playerDataList = JsonUtility.FromJson<PlayerDataList>(json);
        }
        else
        {
            playerDataList = new PlayerDataList();
        }
    }

    // ✅ ตรวจสอบจำนวนผู้เล่น + แสดง leaderboard เรียงเหรียญ
    private void CheckAndCreateLeaderStat()
    {
        foreach (Transform child in leaderStatsTarget.transform)
            Destroy(child.gameObject);

        var sortedPlayers = playerDataList.players
            .OrderByDescending(p => p.coins)
            .Take(maximumPlayer)
            .ToList();

        for (int i = 0; i < sortedPlayers.Count; i++)
        {
            var data = sortedPlayers[i];
            GameObject obj = Instantiate(leaderStatsPrefab, leaderStatsTarget.transform);
            LeaderStats stats = obj.GetComponent<LeaderStats>();
            stats.SetBoard((i + 1).ToString(), data.userName, data.coins.ToString());
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
