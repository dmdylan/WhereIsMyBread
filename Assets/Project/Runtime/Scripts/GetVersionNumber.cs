using UnityEngine;
using TMPro;

public class GetVersionNumber : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI versionTextField;

    // Start is called before the first frame update
    void Start()
    {
        versionTextField.text = $"V {Application.version}";
    }
}
