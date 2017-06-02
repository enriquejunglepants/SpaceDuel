using UnityEngine;

public class createEnv : MonoBehaviour {

    public GameObject planetPrefab;
    public GameObject player;
    public int num_planets;
    public float radius;
	// Use this for initialization
	void Start () {

        for (int i = 0; i < num_planets; i++)
        {
            Instantiate(planetPrefab,
                        new Vector3(Random.Range(-radius, radius),
                                    Random.Range(-radius, radius),
                                    Random.Range(-radius, radius)),
                        Random.rotation);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
