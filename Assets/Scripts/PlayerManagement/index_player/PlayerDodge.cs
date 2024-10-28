﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDodge : MonoBehaviour
{
    public float playerDodge;
    private string saveFilePath;
    // Start is called before the first frame update
    void Start()
    {
        // Lấy đường dẫn lưu trữ file JSON
        string directoryPath = Application.persistentDataPath + "/DB";

        // Tạo thư mục nếu chưa tồn tại
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Đường dẫn tới file lưu trữ dữ liệu
        saveFilePath = directoryPath + "/playerData.json";
        Debug.Log("File Path: " + saveFilePath);

        // Gọi hàm để tải dữ liệu defend
        LoadPlayerDodge();
    }

    // Update is called once per frame
    private void LoadPlayerDodge()
    {
        if (File.Exists(saveFilePath))
        {
            // Đọc nội dung file JSON
            string json = File.ReadAllText(saveFilePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            // Lấy giá trị defend từ file JSON
            playerDodge = 5 + data.dodge;
            Debug.Log("Player Defense loaded successfully: " + playerDodge + "%");
        }
        else
        {
            Debug.LogError("Save file not found at path: " + saveFilePath);
        }
    }

    // Cấu trúc dữ liệu PlayerData để lưu và lấy dữ liệu từ file JSON
    [System.Serializable]
    public class PlayerData
    {
        public string playerName;
        public int attack;
        public int defense;  // Lưu thuộc tính defend
        public int hp;
        public int stamina;
        public int dodge;
        public int skillPoints;
    }
}