using UnityEngine;
using TMPro;

public class ShowItems : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private ItemInventory _inventory;

    [Header("Prefab References")]
    [SerializeField] private GameObject _showItems;
    [SerializeField] private GameObject _exitInteract;
    [SerializeField] private GameObject _fuelInteract;
    private FuelDoor _fuelDoor;

    [Header("Text References")]
    [SerializeField] private TextMeshProUGUI _branchBerries;
    [SerializeField] private TextMeshProUGUI _glowFlowers;

    private void Start()
    {
        _fuelDoor = GetComponent<FuelDoor>();
        _fuelDoor.OnFueled += FuelDoor_OnFueled;

        _showItems.SetActive(false);
        _exitInteract.SetActive(false);
        _fuelInteract.SetActive(false);
    }

    private void FuelDoor_OnFueled()
    {
        _exitInteract.SetActive(true);
        _showItems.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.TryGetComponent<PlayerMovement>(out _)) return;

        if (!_fuelDoor.IsFueled)
        {
            _showItems.SetActive(true);
            _branchBerries.text = $"{_inventory.BranchBerries} / {_fuelDoor.BerryAmount}";
            _glowFlowers.text = $"{_inventory.GlowFlowers} / {_fuelDoor.FlowerAmount}";

            if (_inventory.GlowFlowers >= _fuelDoor.FlowerAmount && _inventory.BranchBerries >= _fuelDoor.BerryAmount)
                _fuelInteract.SetActive(true);

        }
        else _exitInteract.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.transform.TryGetComponent<PlayerMovement>(out _)) return;

        _exitInteract.SetActive(false);
        _showItems.SetActive(false);
    }
}