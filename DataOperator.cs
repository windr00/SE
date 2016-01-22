using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Myproto;

public class DataOperator {

    public delegate void CommandReceived(List<MsgResponse> Command);

    private event CommandReceived OnCommandReceived;

    private Communication comInstance;
    public void AddCommandListener(CommandReceived call)
    {
        OnCommandReceived += call;
    }

    public static DataOperator GetInstance () {
        return _instance;
    }

    private static DataOperator _instance = null;
    private DataOperator() {
        _instance = this;
        comInstance = new Communication(Statics.netType, Statics.ServerIpAddress, Statics.ServerPort);
        comInstance.OnNetworkConnected += comInstance_OnNetworkConnected;
        comInstance.OnDataReceived += comInstance_OnDataReceived;
        comInstance.OnDataSent += comInstance_OnDataSent;
        comInstance.OnNetworkError += comInstance_OnNetworkError;
        comInstance.Connect();
    }

    private void comInstance_OnNetworkConnected()
    {
        comInstance.isNetworkAvailable = true;
        
        comInstance.Read();
    }

    private void comInstance_OnDataSent()
    {
        Debug.Log("Sent");
    }


    private void comInstance_OnDataReceived(object bytes)
    {
        var data = Serialize.Deserailizer<MsgResponse>(bytes as byte[], Statics.DeserializeMethod);
        if (OnCommandReceived != null)
        {
            OnCommandReceived(data);
        }
    }

    private void comInstance_OnNetworkError()
    {

    }

    private MsgRequest FormRequest()
    {
        MsgRequest request = new MsgRequest();
        Content content = new Content();
        var elist = EventReporter.ReportEvent();
        foreach (var e in elist)
        {
            var msg = new Msg();
            msg.type = Support.MsgTypeConverter(e.type);
            msg.body = World.GetInstance().GetGameObjectByClientId(e.sponsorId).GetComponent<EventGenerator>().SelfSerialize(e.type, e.rawContent);
            content.msg.Add(msg);
        }
        request.head.srcType = SRCType.SIM;
        request.content = content;
        request.head.srcID = comInstance.GetHostIP();
        return request;
    }

    public void SendMessage()
    {
        var req = FormRequest();
        var data = Serialize.Serailizer<MsgRequest>(req, Statics.SerializeMethod);
        comInstance.Send(data);
    }
}
