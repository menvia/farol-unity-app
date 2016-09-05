using UnityEngine;
using Farol;
using Farol.Models;

public class Example_FarolControllerUse : MonoBehaviour {
    string text;

    User user;

    public void onCompleteDeviceInfo(DeviceInfo[] s)
    {
        if (s.Length > 0)
        {
            Debug.Log("DEVICE INFO: " + s[0]._id);
            //text = s[0]._id;

            string str = "Device info: ";
            foreach (DeviceInfo d in s)
            {
                str = " { _id = " + d._id + ", ";
                str += "triggers[0]._id = " + d.triggers[0]._id + ", ";
                str += "macaddress = " + d.macaddress;

                str += " }, ";

                text += str;
            }
        }
    }
    public void onCompleteDeviceL(Device[] s)
    {
        if (s.Length > 0)
        {
            Debug.Log("DEVICES: " + s[0]._id);
            text = s[0]._id;
        }
    }
    public void onCompleteLocationL(Location[] s)
    {
        if(s.Length > 0)
            Debug.Log("LOCATIONS: " + s[0].city);
    }
    public void onCompleteTriggerL(Trigger[] s)
    {
        if (s.Length > 0)
        {
            Debug.Log("TRIGGER TYPES: " + s[0].type);

            if (user != null)
            {
                FarolController.CheckIn(s[0], user, onSuccessCheckIn, onFailureCheckIn);
                FarolController.CheckOut(s[0], user, onSuccessCheckOut, onFailureCheckIn);
            }
        }
        Debug.Log("Não retornou um trigger...");
    }

    public void onCompleteApp(User user)
    {
        Debug.Log("USER");
        GameObject o = GameObject.CreatePrimitive(PrimitiveType.Cube);
        o.transform.position = new Vector3();
        if (user != null)
        {
            Debug.Log(" created! ");

            this.user = user;
            FarolController.TriggerList(onCompleteTriggerL);
        }
        else
        {
            Debug.Log(" already exists!");
        }
    }

    public void onSuccessCheckIn(string s)
    {
        Debug.Log("CHECKIN SUCCESS!"+s);
    }
    public void onFailureCheckIn(string s)
    {
        Debug.Log("CHECKIN FAILURE!" + s);
    }
    public void onSuccessCheckOut(string s)
    {
        Debug.Log("CHECKIN SUCCESS!"+s);
    }
    public void onFailureCheckOut(string s)
    {
        Debug.Log("CHECKIN FAILURE! "+s);
    }

    // Use this for initialization
    void Start () {
        /*
         * NOTE: To use the FarolWS you will need to access through FarolController.
         * STEPS:
         * 1) Instantiate the class with your app token;
         * 2) Access the utility methods passing your callback's signature (That will be called when the service responds).
         * 3) Follow the examples below.
         */

        /// Inicialização da singleton
        FarolController.Instantiate("YOUR_APP_TOKEN_HERE");

        /// Pegando lista de dispositivos, localizações e gatilhos
        //FarolController.DeviceList(onCompleteDeviceL);
        //FarolController.LocationList(onCompleteLocationL);
        //FarolController.TriggerList(onCompleteTriggerL);
        FarolController.DeviceInfoList(onCompleteDeviceInfo);

        /// Cria um novo usuário | A callback onComplete recebe o usuário criado (OBS: só recebe se ele não existir)
        //FarolController.AppUser("Nome", "Sobrenome", "test@test.com", onCompleteApp);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0,0,Screen.width,Screen.height), ("Device id of device list: "+text));
    }
}
