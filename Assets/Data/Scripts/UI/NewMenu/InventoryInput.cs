using UnityEngine;

public class InventoryInput : MonoBehaviour {
    [SerializeField] GameObject characterPanelGameObject;
    [SerializeField] GameObject equipmentPanelGameObject;
    [SerializeField] KeyCode[] toggleCharacterPanelKeys;
    [SerializeField] KeyCode[] toggleEquipmentPanelKeys;

    void Update() {
        for (int i = 0; i < toggleCharacterPanelKeys.Length; i++) {
            if (Input.GetKeyDown(toggleCharacterPanelKeys[i])) {
                characterPanelGameObject.SetActive(!characterPanelGameObject.activeSelf);
                break;
            }
        }

        for (int i = 0; i < toggleEquipmentPanelKeys.Length; i++) {
            if (Input.GetKeyDown(toggleEquipmentPanelKeys[i])) {
                equipmentPanelGameObject.SetActive(!equipmentPanelGameObject.activeSelf);
                break;
            }
        }
    }
}
