using TMPro;
using UnityEngine;

//It's Allows You To Drag One Same Script(You Can't Attach More Than One Same Script On Spesific Object);
[DisallowMultipleComponent]
public class TapTimeMove : MonoBehaviour
{
    [SerializeField] private TMP_Text powerText; //Reference Text
    [SerializeField] private Vector3 movVector; //To Determine The Range Of Object Position
    [SerializeField] private float period = 2f; //Period Time

    //To Show Object Movement Value In Editor
    [SerializeField]
    [Range(-1, 1)]
    private float movScale; //Moving Pointer Between Left = (-1) Not Move = (0) Right Move = (1)

    private Vector3 startingPos;

    private int tempPower;

    private bool isStopped;

    private void Start()
    {
        SettingStartPos();
    }

    //Set Object Start Position
    private void SettingStartPos()
    {
        startingPos = transform.position;
    }

    private void Update()
    {
        MovePointer();

        StopMovement();
    }

    //Stoping Object Move
    private void StopMovement()
    {
        //If Game Is Stopped Then Return
        if (isStopped) { return; }

        if (Input.GetMouseButton(0))
        {
            //Reset Period
            period = 0;
            //Update And Set Power
            powerText.text = tempPower.ToString();

            isStopped = true;
        }
    }

    //Moving Object Horizontally
    private void MovePointer()
    {
        if (period <= Mathf.Epsilon) { return; } //We Can't Equal Two Float Number, To Protect  Against Zero Error, Instead Of Using Zero, We Are Using Epsilon, Which Is The Closest Value To It
        float cycles = Time.time / period; //Grows Continually From 0

        const float tau = Mathf.PI * 2f; //Cons Tau Value About 6.28f
        float rawSinWave = Mathf.Sin(cycles * tau); //Goes From (-1 to 1)

        movScale = rawSinWave;
        //To Be Able To Adjust Value Later We Setted movScale To rawSinWave
        /*-------------- To Move Object Between (0,1) Area --------------------
        movScale = rawSinWave / 2f + 0.5f;
        //This Range Is Set (-1 , 1) In Normal, If We Divided rawSinWave Value By 2f, Which Is New Value = - 0.5f, 0.5f
        //Then Adding 0.5f, The Final Value Will Be In That Range (0 to 1)*/

        Vector3 offset = movVector * movScale; //Creating Offset
        transform.position = startingPos + offset; //Movement
    }

    //To Set Power Color
    private void OnTriggerEnter(Collider other)
    {
        //Get Collided Object Color
        Color collidedColor = other.gameObject.GetComponent<Renderer>().material.color;

        switch (other.gameObject.tag)
        {
            case "Green":
                powerText.color = collidedColor;
                tempPower = 50;
                break;
            case "LightGreen":
                powerText.color = collidedColor;
                tempPower = 35;
                break;
            case "Yellow":
                powerText.color = collidedColor;
                tempPower = 25;
                break;
            case "Orange":
                powerText.color = collidedColor;
                tempPower = 15;
                break;
            case "Red":
                powerText.color = collidedColor;
                tempPower = 5;
                break;
        }
    }
}