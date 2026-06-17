using UnityEngine;

public class SettingFruit : MonoBehaviour
{
    public GameObject[] fruits;

    // Start is called before the first frame update
    void Start()
    {
        SpawnFruit(Random.Range(1, 4));
    }

    void SpawnFruit(int type)
    {
        switch (type)
        {
            case 1:
                Instantiate(fruits[0], new Vector3(-2, -3, 0), Quaternion.identity);
                Instantiate(fruits[1], new Vector3(-1, -3, 0), Quaternion.identity);
                Instantiate(fruits[2], new Vector3(1, -3, 0), Quaternion.identity);
                Instantiate(fruits[3], new Vector3(3, 2, 0), Quaternion.identity);
                break;
            case 2:
                Instantiate(fruits[3], new Vector3(-2, -3, 0), Quaternion.identity);
                Instantiate(fruits[2], new Vector3(-1, -3, 0), Quaternion.identity);
                Instantiate(fruits[1], new Vector3(1, -3, 0), Quaternion.identity);
                Instantiate(fruits[0], new Vector3(3, 2, 0), Quaternion.identity);
                break;
            case 3:
                Instantiate(fruits[0], new Vector3(0, -3, 0), Quaternion.identity);
                Instantiate(fruits[1], new Vector3(1, -3, 0), Quaternion.identity);
                Instantiate(fruits[2], new Vector3(3, -2, 0), Quaternion.identity);
                Instantiate(fruits[3], new Vector3(-3, -1, 0), Quaternion.identity);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
