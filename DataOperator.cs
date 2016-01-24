using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Myproto;

public class DataOperator {

    public delegate void CommandReceived(List<MsgResponse> Command);

    private event CommandReceived OnCommandReceived;

	private ManualResetEvent waitSend ;
	private Communication comInstance;
    public void AddCommandListener(CommandReceived call)
    {
        OnCommandReceived += call;
    }

    public static DataOperator GetInstance () {
		lock (_instance) {
			if (_instance == null) {
				_instance = new DataOperator ();
			}
		}
        return _instance;
    }

    private static DataOperator _instance = null;
    private DataOperator() {
		comInstance = Communication.GetInstance ();

        comInstance.Initial(Statics.netType, Statics.ServerIpAddress, Statics.ServerPort);

		comInstance.AddNetworkListeners (comInstance_OnNetworkConnected, 
		                                 comInstance_OnDataSent,
		                                 comInstance_OnDataReceived,
		                                 comInstance_OnNetworkError);
        comInstance.Connect();
    }

    private void comInstance_OnNetworkConnected()
    {
        comInstance.isNetworkAvailable = true;
        
        comInstance.Read();
    }

    private void comInstance_OnDataSent()
    {
		waitSend.Set ();
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

    private void comInstance_OnNetworkError(Exception e)
    {

    }

    private List<MsgRequest> FormRequest()
    {
		List<MsgRequest> ret = new List<MsgRequest> ();
		//if (elist.Count == 0) {
		//	return null;
		//}
		var elist = EventReporter.ReportEvent ();
        foreach (var e in elist)
        {
			var req = new MsgRequest();
			var head = new Head();
			var content = new Content();
			var msg = new Msg();
			head.srcID = e.sponsorId;
			head.srcType = SRCType.SIM;
			head.dstIDs.InsertRange(0,e.targetIdList);
			msg.type = Support.MsgTypeConverter(e.type);
			msg.body = World.GetInstance().GetGameObject(e.sponsorId).GetComponent<EventGenerator>().SelfSerialize(e.type,e.rawContent);
			content.msg.Add(msg);
			req.content = content;
			req.head = head;
			ret.Add(req);
        }
		return ret;
	}

    public void SendMessage()
    {
        var req = FormRequest();
		Debug.Log ("formed request count: " + req.Count);
		foreach (var i in req) {
			var data = Serialize.Serailizer<MsgRequest> (i, Statics.SerializeMethod);
			Debug.Log("Sending " + BitConverter.ToString(data));
			comInstance.Send (data);
			waitSend.WaitOne();
		}
    }
}
