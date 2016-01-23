using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Myproto;
using UnityEngine;
using System.Reflection;
using ProtoBuf;
public class Driver : MonoBehaviour {

    void Awake()
    {
        DataOperator.GetInstance().AddCommandListener(ReceiveCommand);
    }

    private void ReceiveCommand(List<MsgResponse> command)
    {
        EventDispatch.DispatchEvent(command);
    }

	void FixedUpdate () 
    {
		DataOperator.GetInstance ().SendMessage ();
	}
}
