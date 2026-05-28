using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Bank : MonoBehaviour {
    public static UnityEvent<int> AddMoney = new();
    public static UnityEvent<int> SpendMoney = new();
    public static UnityEvent<int> MoneyChanged = new();
    public static Bank Instance { private set; get; } = null;
    public int Balance { get; private set; } = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        if (Instance) {
            Debug.LogError("There was an attempt to instantiate a second Bank");
            Destroy(this);
            return;
        }
        Instance = this;
        AddMoney.AddListener(OnAddMoney);
        SpendMoney.AddListener(OnSpendMoney);
        MoneyChanged.Invoke(Balance);
    }

    void Update() {
        // Dev cheats
        if (Keyboard.current.rightShiftKey.isPressed) {
            if (Keyboard.current.rKey.wasPressedThisFrame) {
                AddMoney.Invoke(100);
            }

            if (Keyboard.current.pKey.wasPressedThisFrame) {
                SpendMoney.Invoke(100);
            }
        }
    }

    void OnAddMoney(int amount) {
        Balance += amount;
        MoneyChanged.Invoke(Balance);
    }

    void OnSpendMoney(int amount) {
        Balance -= amount;
        MoneyChanged.Invoke(Balance);
    }
}
