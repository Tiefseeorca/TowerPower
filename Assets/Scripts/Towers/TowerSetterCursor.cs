using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerSetterCursor : MonoBehaviour {
    public List<GameObject> PlaceableTowers;
    public int SelectedTowerIndex;
    public List<GameObject> Holograms;

    void Start() {
        SelectedTowerIndex = 0;
        SwapToTurret(0);
    }
    
    void Update() {
        TowerSelection();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);
        if (Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Towers"))) {
            Transform objectHit = hit.transform;
            transform.position = Vector3.Lerp(transform.position, objectHit.position, Time.deltaTime * 40);
            //transform.position = objectHit.position;
            if (!Mouse.current.leftButton.wasPressedThisFrame) return;
            AttemptPlacingTower(objectHit);
        }
    }

    private void AttemptPlacingTower(Transform objectHit) {
        TowerSocket socket = objectHit.GetComponent<TowerSocket>();
        if (socket) {
            if (!socket.CanPlace()) {
                Debug.Log("Tower could not be placed because socket is occupied");
                return;
            }

            GameObject towerToPlace = PlaceableTowers[SelectedTowerIndex];
            int price = towerToPlace.GetComponent<Tower>().Stats.price;
            if (price > Bank.Instance.Balance) {
                Debug.Log("Tower could not be placed because it is too expensive");
                return;
            }
            socket.Place(Instantiate(towerToPlace, objectHit.position, Quaternion.identity));
            Bank.SpendMoney.Invoke(price);
        }
    }

    private void TowerSelection() {
        if (Keyboard.current.digit1Key.wasPressedThisFrame) SwapToTurret(0);
        else if (Keyboard.current.digit2Key.wasPressedThisFrame) SwapToTurret(1);
        else if (Keyboard.current.digit3Key.wasPressedThisFrame) SwapToTurret(2);
    }

    private void SwapToTurret(int index) {
        Holograms[SelectedTowerIndex].SetActive(false);
        SelectedTowerIndex = index;
        Holograms[SelectedTowerIndex].SetActive(true);
    }
    
}
