using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMovements : MonoBehaviour
{
    public AudioSource SwordOnIce;
    public float CubeSize = 0.01f;
    public int CubesInFLow = 5;
    float CubesPivotDistance;
    Vector3 CubesPivot;
    public float ExplosionForce = 10;
    public float ExplosionRadius = 4;
    public float ExplosionUpward = 0.04f;
    bool colRiseUp = false;
    GameObject Column;
    GameObject CenterColumn;
    GameObject Kikongi;
    Vector3 destinationKikongi;
    bool SetKikongiColumnParent = true;
    float swordOutTheRock = 0.018f;
    bool swordIsInRock = true;

    // Start is called before the first frame update
    void Start()
    {
        Column = GameObject.FindGameObjectWithTag(Names.COLUMN);
        CenterColumn = GameObject.FindGameObjectWithTag(Names.CENTERCOLUMN);
        Kikongi = GameObject.FindGameObjectWithTag(Names.KIKONGI);
        CubesPivotDistance = CubeSize * CubesInFLow / 2;
        CubesPivot = new Vector3(CubesPivotDistance, CubesPivotDistance, CubesPivotDistance);
        destinationKikongi = new Vector3(1f, 0.005f, 0f);    
    }

    // Update is called once per frame
    void Update()
    {
        if (colRiseUp)
        {
            ColumnRiseUp();
        }

        if (swordIsInRock)
        {
            SwordOutTheRock();
        }
    }

    private void SwordOutTheRock()
    {
        if (this.transform.position.y > swordOutTheRock)
        {
            var rigidbody = this.GetComponent<Rigidbody>();
            rigidbody.constraints = RigidbodyConstraints.None;
            swordIsInRock = false;
        }        
    }

    private void ColumnRiseUp()
    {
        var y = Column.transform.position.y;
        if (y < 0)
        {
            Column.transform.Translate(new Vector3(0, Time.deltaTime, 0));
        }
        else
        {
            var kikongiTransform = Kikongi.transform;

            if (SetKikongiColumnParent)
            {
                kikongiTransform.SetParent(null);
                SetKikongiColumnParent = false;
            }
                        
            kikongiTransform.position = Vector3.MoveTowards(kikongiTransform.position, CenterColumn.transform.position, Time.deltaTime);

            if (kikongiTransform.position.Equals(CenterColumn.transform.position))
            {
                colRiseUp = false;
                this.gameObject.SetActive(false);
            }
        }
    }
        
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(Names.ICECUBE))
        {
            SwordOnIce.PlayOneShot(SwordOnIce.clip);
            Explode(other.gameObject);
            colRiseUp = true;
        }

        if (other.tag.Equals(Names.ROCK) && !swordIsInRock)
        {
            this.transform.Translate(new Vector3(0.01f, 0.01f, 0.01f));
        }
    }

    private void Explode(GameObject cube)
    {
        cube.SetActive(false);

        for (int x = 0; x < CubesInFLow; x++)
            for (int y = 0; y < CubesInFLow; y++)
                for (int z = 0; z < CubesInFLow; z++)
                    CreatePiece(cube, x, y, z);

        Vector3 explosionPos = cube.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, ExplosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(ExplosionForce, explosionPos, ExplosionRadius, ExplosionUpward);
            }
        }
    }

    private void CreatePiece(GameObject cube, int x, int y, int z)
    {
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        piece.transform.position = cube.transform.position + new Vector3(CubeSize * x, CubeSize * y, CubeSize * z) - CubesPivot;
        piece.transform.localScale = new Vector3(CubeSize, CubeSize, CubeSize);
        piece.tag = Names.ICECUBE;
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = CubeSize;
        piece.GetComponent<Renderer>().material = cube.GetComponent<Renderer>().material;
    }
}
