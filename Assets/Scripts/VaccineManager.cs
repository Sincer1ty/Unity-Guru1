using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaccineManager : MonoBehaviour
{
    public static VaccineManager instance;

    public GameObject vaccinePrefab;
    public int initialVaccines = 10;

    List<GameObject> vaccines = new List<GameObject>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        MakeVaccines();
    }

    void MakeVaccines()
    {
        for(int i=0;i<initialVaccines;i++)
        {
            GameObject tempVaccine = Instantiate(vaccinePrefab) as GameObject;

            // 새로 생성된 코인들이 오브젝트 매니저의 자식 오브젝트로 들어감
            tempVaccine.transform.parent = transform;

            tempVaccine.SetActive(false);
            vaccines.Add(tempVaccine);
        }
    }

    public void DropVaccineToPosition(Vector3 pos, int VaccineValue)
    {
        GameObject reusedVaccine = null;
        for (int i = 0; i < vaccines.Count; i++)
        {
            if(vaccines[i].activeSelf==false)
            {
                reusedVaccine = vaccines[i];
                break;
            }
        }
        if(reusedVaccine==null)
        {
            GameObject newVaccine = Instantiate(vaccinePrefab) as GameObject;
            vaccines.Add(newVaccine);
            reusedVaccine = newVaccine;
        }
        reusedVaccine.SetActive(true);
        reusedVaccine.GetComponent<Vaccine>().SetScoreValue(VaccineValue); 
        reusedVaccine.transform.position = new Vector3(pos.x, reusedVaccine.transform.position.y, pos.z);
    }
    
}
