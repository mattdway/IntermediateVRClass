using UnityEngine;
using TMPro;

/// <summary>
/// Apply forward force to instantiated prefab
/// </summary>
public class Launch_Dart_With_Count : MonoBehaviour
{
    // Adds a tooltip message that will appear when the user hovers their cursor over the variable in the Inspector
    [Tooltip("The projectile that's created")]
    // Creates a public variable of the GameObject data type called ProjectilePrefab
    // The GameObject data type is able to get and manipulate the properties of a linked game object
    // The Projectile_Dart_Modified prefab game object will be linked here via the Inspector window
    public GameObject ProjectilePrefab = null;

    // Adds a tooltip message that will appear when the user hovers their cursor over the variable in the Inspector
    [Tooltip("The point that the project is created")]
    // Creates a public variable of the Transform data type called StartPoint
    // The Transform data type gets the position, rotation and scale of a linked game object
    // The Dart_StartPoint game object in the Hierarchy window will be linked here via the Inspector window
    public Transform StartPoint = null;

    // Adds a tooltip message that will appear when the user hovers their cursor over the variable in the Inspector
    [Tooltip("The speed at which the projectile is launched")]
    // Creates a public variable of the float data type
    // Float denotes that the value will have a decimal place
    // Here we are initializing the  variablem, which means we are setting its value in the same line that we declare the variable using the =
    // The f is used to denote that this is a float value
    public float LaunchSpeed = 1.0f;

    // Creates a private variable _dartCount of the int (integer) data type to hold the dart count amount
    // [SerialzeField] exposes a private variable to the Inspector window
    // You'll be able to see this field in the Inspector window and you can even change its value (if numerical) on the fly
    [SerializeField] private int _dartCount = 25;

    // Creates a private varaible _dartCountStart of the int (integer) data type to hold the original dart count from the beginning of the game
    private int _dartCountStart;

    // Creates a private variable to link the TextMeshPro game object
    // [SerialzeField] exposes a private variable to the Inspector window
    // You'll be able to see this field in the Inspector window where you will link the TextMeshPro game object you created
    [SerializeField] private TextMeshProUGUI _text;

    // Creates a private variable to link and the NerfGun_Trigger game object
    // [SerialzeField] exposes a private variable to the Inspector window
    // You'll be able to see this field in the Inspector window where you will link the NerfGun_Trigger game object
    [SerializeField] private GameObject _nerfGunTrigger;

    // Creates the private variable m_animator of the data type Animator
    // The Animator data type is used to play and control animations for a GameObject
    private Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        // Finds and adds the first active loaded object that matches the specified type - deprecated (original) code
        // _text = FindObjectOfType<TextMeshProUGUI>();

        // Per https://gamedev.stackexchange.com/questions/132569/how-do-i-find-an-object-by-type-and-name-in-unity-using-c/132575
        // Find GameObject by both name and type
        _text = GameObject.Find("Darts_Remaining_Text (TMP)").GetComponent<TextMeshProUGUI>();

        // Gets the Animator component from the _nerfGunTrigger game object and assigns it to the variable m_animator
        m_animator = _nerfGunTrigger.GetComponent<Animator>();

        // Sets the _dartCount variable's starting value to the variable _dartCountStart for the purposes of the Reload() method 
        _dartCountStart = _dartCount;
}
    // Update is called once per frame
    void Update()
    {
        // Write text and variable value to TextMeshPro, updated once per frame
        _text.text = "Darts Remaining: " + _dartCount;
    }

    public void Fire()
    {
        // Trigger _nerfGunTrigger pull and release animation
        m_animator.SetTrigger("Fire");

        // Check to see if _dartCount is greater than 0
        if (_dartCount > 0)
        {
            // Instantiate projectile prefab with start position and start rotation
            GameObject newObject = Instantiate(ProjectilePrefab, StartPoint.position, StartPoint.rotation);

            // If the instantiated ProjectilePrefab has a rigidbody component then
            // The TryGetComponent method of the GameObject class returns a bool of true or false
            // The out keyword is used to declare a new variable named rigidBody, which holds the reference to the rigidbody component, if it exists on newObject
            if (newObject.TryGetComponent(out Rigidbody rigidBody))
            {
                // Call ApplyForce method
                ApplyForce(rigidBody);
            }
        }
    }

    private void ApplyForce(Rigidbody rigidBody)
    {
        // Apply force and speed to projectile
        Vector3 force = StartPoint.forward * LaunchSpeed;
        rigidBody.AddForce(force);

        // Reduce _dartCount by one
        _dartCount -= 1;
    }

    public void Reload()
    {
        // When called set _dartCount back to starting value of 25. Set Reload as your Select Exited and/or Select events.
        _dartCount = _dartCountStart;
    }
}