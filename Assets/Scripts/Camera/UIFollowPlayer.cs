using UnityEngine;

public class UIFollowPlayer : MonoBehaviour
{
    public GameObject player;  // Nhân vật chính
    public RectTransform moveForwardButton;  // Nút Tiến lên
    public RectTransform moveBackwardButton;  // Nút Lùi lại
    public RectTransform attackButton;  // Nút Tấn công
    public RectTransform restButton;

    public float offsetX = 100f;  // Khoảng cách của các nút so với nhân vật
    public float offsetY = 130f;

    void Update()
    {
        // Lấy vị trí của nhân vật trên màn hình
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(player.transform.position);

        // Cập nhật vị trí của nút tiến lên bên phải nhân vật
        moveForwardButton.position = screenPosition + new Vector3(offsetX, 0, 0);

        // Cập nhật vị trí của nút lùi lại bên trái nhân vật
        moveBackwardButton.position = screenPosition - new Vector3(offsetX, 0, 0);

        attackButton.position = screenPosition + new Vector3(offsetX, offsetY, 0);

        restButton.position = screenPosition - new Vector3(offsetX, -offsetY, 0);
    }
}
