using UnityEngine;
using System.Collections.Generic;
using Farol;
using Farol.Models;
using System;

public class Example_FarolControllerUse : MonoBehaviour {

    User user;

    public void onCompleteDeviceL(Device[] s)
    {
        if (s.Length > 0)
            Debug.Log("DEVICES: "+s[0]._id);
    }
    public void onCompleteLocationL(Location[] s)
    {
        if(s.Length > 0)
            Debug.Log("LOCATION: " + s[0].city);
    }
    public void onCompleteTriggerL(Trigger[] s)
    {
        if (s.Length > 0)
            Debug.Log("TRIGGER TYPE: " + s[0].type);

        FarolController.CheckIn(s[0], user, onSuccessCheckIn, onFailureCheckIn);
        FarolController.CheckOut(s[0], user, onSuccessCheckOut, onFailureCheckIn);
    }

    public void onCompleteApp(User user)
    {
        Debug.Log("USER");
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

        // Inicialização da singleton
        FarolController.Instantiate("YOUR_APP_TOKEN_HERE");

        // Pegando lista de dispositivos, localizações e gatilhos
        //FarolController.DeviceList(onCompleteDeviceL);
        //FarolController.LocationList(onCompleteLocationL);
        //FarolController.TriggerList(onCompleteTriggerL);

        // Cria um novo usuário | A callback onComplete recebe o usuário criado (OBS: só recebe se ele não existir)
        FarolController.AppUser("Nome", "Sobrenome", "test@test.com", onCompleteApp);
        
    }
}
