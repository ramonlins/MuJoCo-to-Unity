using UnityEngine;
using System.Xml;

public class MuJoCoSceneParser : MonoBehaviour
{
    public TextAsset mujocoXmlFile; // Reference to the MuJoCo XML file

    public GameObject planePrefab;
    public GameObject ellipsoidPrefab;
    public GameObject spherePrefab;
    public GameObject cubePrefab;
    public GameObject cylinderPrefab;

    void Start()
    {
        ParseMuJoCoScene();

    }

    void ParseMuJoCoScene()
    {
        if (mujocoXmlFile == null)
        {
            Debug.LogError("No MuJoCo XML file assigned!");
            return;
        }

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(mujocoXmlFile.text);

        XmlNodeList geomNodes = xmlDoc.SelectNodes("mujoco/worldbody//geom");

        /* TODO: It is interessant to get the position from body since geom are at origin point
                and do not represent the position of objects created in blender.
        */
        foreach (XmlNode geomNode in geomNodes)
        {
            if (geomNode.Attributes["type"] != null)
            {

                string geomType = geomNode.Attributes["type"].Value;

                if (geomType == "plane")
                {
                    Vector3 size = ParseSize(geomNode.Attributes["size"], geomType);
                    Vector3 position = ParsePosition(geomNode.Attributes["pos"], geomType);
                    Quaternion rotation = ParseRotation(geomNode.Attributes["quat"], geomType);

                    GameObject plane = Instantiate(planePrefab, position, rotation);
                    plane.transform.localScale = size;
                }
                else if (geomType == "ellipsoid")
                {
                    Vector3 size = ParseSize(geomNode.Attributes["size"], geomType);
                    Vector3 position = ParsePosition(geomNode.Attributes["pos"], geomType);
                    Quaternion rotation = ParseRotation(geomNode.Attributes["quat"], geomType);

                    GameObject ellipsoid = Instantiate(ellipsoidPrefab, position, rotation);
                    ellipsoid.transform.localScale = size;
                }
                else if (geomType == "sphere")
                {
                    Vector3 size = ParseSize(geomNode.Attributes["size"], geomType);
                    float radius = size.z;

                    Vector3 position = ParsePosition(geomNode.Attributes["pos"], geomType);
                    Quaternion rotation = ParseRotation(geomNode.Attributes["quat"], geomType);

                    GameObject sphere = Instantiate(spherePrefab, position, rotation);
                    sphere.transform.localScale = new Vector3(radius, radius, radius);
                }

                else if (geomType == "box")
                {
                    Vector3 size = ParseSize(geomNode.Attributes["size"], geomType);
                    Vector3 position = ParsePosition(geomNode.Attributes["pos"], geomType);
                    Quaternion rotation = ParseRotation(geomNode.Attributes["quat"], geomType);

                    GameObject cube = Instantiate(cubePrefab, position, rotation);
                    cube.transform.localScale = size;
                }
                else if (geomType == "cylinder")
                {
                    Vector3 size = ParseSize(geomNode.Attributes["size"], geomType);
                    float radius = size.z * 0.5f;
                    float height = size.z;

                    Vector3 position = ParsePosition(geomNode.Attributes["pos"], geomType);
                    Quaternion rotation = ParseRotation(geomNode.Attributes["quat"], geomType);

                    GameObject cylinder = Instantiate(cylinderPrefab, position, rotation);
                    cylinder.transform.localScale = new Vector3(radius * 2f, height, radius * 2f);
                }
                    // Add more conditions for other geometry types if needed
            }
        }
    }

    Vector3 ParseSize(XmlAttribute sizeAttribute, string geomType)
    {
        Vector3 size;

        if (sizeAttribute == null)
        {
            Debug.LogWarning($"No 'size' attribute found in the 'geom' {geomType}");
            // Set a default size for the object
            size = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else
        {
            // Access the value of the "size" attribute and parse it to vector
            size = ParseVector3(sizeAttribute.Value);
        }

        return size;
    }

    Vector3 ParsePosition(XmlAttribute positionAttribute, string geomType)
    {
        Vector3 position;

        if (positionAttribute == null)
        {
            Debug.LogWarning($"No 'position' attribute found in the 'geom' {geomType}");
            // Set a default position for the object
            position = new Vector3(0.0f, 0.0f, 0.0f);
        }
        else
        {
            // Access the value of the "position" attribute and parse it to vector
            position = ParseVector3(positionAttribute.Value);
        }

        return position;
    }

    Quaternion ParseRotation(XmlAttribute rotationAttribute, string geomType)
    {
        Quaternion rotation;

        if (rotationAttribute == null)
        {
            Debug.LogWarning($"No 'rotation' attribute found in the 'geom' {geomType}");
            // Set a default rotation for the object
            rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        }
        else
        {
            // Access the value of the "rotation" attribute and parse it to quaternion
            rotation = ParseQuaternion(rotationAttribute.Value);
        }

        return rotation;
    }

    Vector3 ParseVector3(string vectorString)
    /*
        Mapping axis of MuJoCo to Unity:
        MuJoCo: m.x -> Unity: u.z
        MuJoCo: m.y -> Unity: u.x
        MuJoCo: m.z -> Unity: u.y

        Explanation:
        MuJoCo uses a right-handed coordinate system where:
        - m.x points forward
        - m.y points to the left
        - m.z points upward

        Unity uses a left-handed coordinate system where:
        - u.x points forward
        - u.y points upward
        - u.z points to the left

        So, to correctly map the axes from MuJoCo to Unity, we need to swap:
        - m.x with u.z
        - m.y with u.x
        - m.z with u.y
    */
    {
        string[] values = vectorString.Split(' ');

        if (values.Length == 1)
        {
            float z = float.Parse(values[0]);

            return new Vector3(0.0f, 0.0f, z);
        }
        else if (values.Length == 2)
        {
            float z = float.Parse(values[0]);
            float x = float.Parse(values[1]);

            return new Vector3(x, 0.0f, z);
        }
        else if (values.Length == 3)
        {
            float z = float.Parse(values[0]);
            float x = float.Parse(values[1]);
            float y = float.Parse(values[2]);

            return new Vector3(x, y, z);
        }
        else{
            Debug.LogError("Only 3D vectors permited!");
            return new Vector3(0.0f, 0.0f, 0.0f);
        }

    }

    Quaternion ParseQuaternion(string quaternionString)
    {
        string[] values = quaternionString.Split(' ');
        float x = float.Parse(values[0]);
        float y = float.Parse(values[1]);
        float z = float.Parse(values[2]);
        float w = float.Parse(values[3]);

        return new Quaternion(x, y, z, w);
    }
}
