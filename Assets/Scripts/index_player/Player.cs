﻿using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	// Các thuộc tính của nhân vật
	public string playerName;
	public int attack = 1;
	public int defense = 1;
	public int hp = 1;
	public int stamina = 1;
	public int dodge = 1;
	public int skillPoints = 9;

	// Tham chiếu đến các UI Text để hiển thị giá trị thuộc tính
	public InputField nameInputField;
	public Text attackText;
	public Text defenseText;
	public Text hpText;
	public Text staminaText;
	public Text dodgeText;
	public Text skillPointsText;
    private static bool isFirstTimeRun = true;


    private string saveFilePath;

    private void Start()
    {
        // Lấy đường dẫn thư mục lưu trữ dựa trên Application.persistentDataPath
        string directoryPath = Application.persistentDataPath + "/DB";

        // Tạo thư mục nếu chưa tồn tại
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Chỉ định đường dẫn lưu trữ dữ liệu
        saveFilePath = directoryPath + "/playerData.json";
        Debug.Log("File Path: " + saveFilePath);

        LoadData();
        UpdateUI();
    }

    // Hàm lưu tên
    public void SaveName()
	{
		playerName = nameInputField.text;
	}

	// Hàm cập nhật UI
	private void UpdateUI()
	{
		attackText.text = attack.ToString();
		defenseText.text = defense.ToString();
		hpText.text = hp.ToString();
		staminaText.text = stamina.ToString();
		dodgeText.text = dodge.ToString();
		skillPointsText.text = skillPoints.ToString();
	}

	// Hàm để tăng thuộc tính
	public void IncreaseAttribute(string attribute)
	{
		if (skillPoints > 0)
		{
			switch (attribute)
			{
				case "Attack":
					attack++;
					break;
				case "Defense":
					defense++;
					break;
				case "HP":
					hp++;
					break;
				case "Stamina":
					stamina++;
					break;
				case "Dodge":
					dodge++;
					break;
			}
			skillPoints--;
			UpdateUI();
		}
	}

	// Hàm để giảm thuộc tính
	public void DecreaseAttribute(string attribute)
	{
		switch (attribute)
		{
			case "Attack":
				if (attack > 1) { attack--; skillPoints++; }
				break;
			case "Defense":
				if (defense > 1) { defense--; skillPoints++; }
				break;
			case "HP":
				if (hp > 1) { hp--; skillPoints++; }
				break;
			case "Stamina":
				if (stamina > 1) { stamina--; skillPoints++; }
				break;
			case "Dodge":
				if (dodge > 1) { dodge--; skillPoints++; }
				break;
		}
		UpdateUI();
	}

	// Hàm để lưu dữ liệu vào file
	public void SaveData()
	{
		PlayerData data = new PlayerData
		{
			playerName = this.playerName,
			attack = this.attack,
			defense = this.defense,
			hp = this.hp,
			stamina = this.stamina,
			dodge = this.dodge,
			skillPoints = this.skillPoints
		};

		string json = JsonUtility.ToJson(data, true);
		File.WriteAllText(saveFilePath, json);
		Debug.Log("Data Saved Successfully at: " + saveFilePath);
	}

    public void SaveDataButton()
    {
        SaveName(); // Gọi để lưu tên trước khi lưu dữ liệu
        SaveData(); // Gọi để lưu tất cả dữ liệu vào file

        // Kiểm tra nếu đây là lần đầu tiên chạy trong phiên hiện tại
        if (isFirstTimeRun)
        {
            // Lần đầu, chuyển sang NormalMap và đánh dấu không phải lần đầu nữa
            SceneManager.LoadScene("NormalMap");
            isFirstTimeRun = false; // Đánh dấu đã hoàn thành lần đầu tiên trong phiên chạy hiện tại
        }
        else
        {
            // Các lần tiếp theo, chuyển sang Scene khác (ví dụ: "DifferentMap")
            SceneManager.LoadScene("Lobby");
        }
    }



    // Hàm tải dữ liệu từ file
    public void LoadData()
	{
		if (File.Exists(saveFilePath))
		{
			string json = File.ReadAllText(saveFilePath);
			PlayerData data = JsonUtility.FromJson<PlayerData>(json);

			this.playerName = data.playerName;
			this.attack = data.attack;
			this.defense = data.defense;
			this.hp = data.hp;
			this.stamina = data.stamina;
			this.dodge = data.dodge;
			this.skillPoints = data.skillPoints;

			// Cập nhật InputField với tên đã lưu
			if (nameInputField != null) // Kiểm tra để tránh lỗi nếu InputField không tồn tại trong Scene mới
			{
				nameInputField.text = playerName;
			}

			Debug.Log("Data Loaded Successfully");
		}
		else
		{
			Debug.Log("Save file not found at path: " + saveFilePath);
		}
	}

	// Hàm xóa dữ liệu và đặt lại giá trị mặc định
	public void ResetData()
	{
		// Xóa file lưu trữ
		if (File.Exists(saveFilePath))
		{
			File.Delete(saveFilePath);
			Debug.Log("Save file deleted.");
		}

		// Đặt lại tất cả thuộc tính về giá trị mặc định
		playerName = "";
		attack = 1;
		defense = 1;
		hp = 1;
		stamina = 1;
		dodge = 1;
		skillPoints = 9;

		// Cập nhật giao diện
		if (nameInputField != null) // Kiểm tra xem InputField có được tham chiếu không
		{
			nameInputField.text = ""; // Đặt lại tên trong InputField về chuỗi rỗng
		}

		UpdateUI();
	}


	// Cấu trúc dữ liệu để lưu vào file
	[System.Serializable]
	public class PlayerData
	{
		public string playerName;
		public int attack;
		public int defense;
		public int hp;
		public int stamina;
		public int dodge;
		public int skillPoints;
	}
}
