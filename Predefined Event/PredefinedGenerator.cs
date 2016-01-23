using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using ProtoBuf;
using PredefinedProto;

public class PredefinedGenerator : EventGenerator {

	void Start() {
		base.AddListener(EventCollection.OnEventTrigger);
	}

    public override byte[] SelfSerialize(UserEvent.EventType type, object content)
	{
		byte[] ret = null;
		switch (type) {
		case UserEvent.EventType.GA:
		{
			var proto = content as GA;
			using (var stream = new MemoryStream()) {
				Serializer.Serialize<GA>(stream,proto);
				ret = stream.ToArray();
			}
			break;
		}
		case UserEvent.EventType.GR:
		{
			var proto = content as GR;
			using (var stream = new MemoryStream()) {
				Serializer.Serialize<GR>(stream,proto);
				ret = stream.ToArray();
			}
			break;
		}
		case UserEvent.EventType.LA:
		{
			var proto = content as LA;
			using (var stream = new MemoryStream()) {
				Serializer.Serialize<LA>(stream,proto);
				ret = stream.ToArray();
			}
			break;
		}
		}

		return ret;
    }

	private void Test() {
		var e = new UserEvent ();
		var ga = new GA ();
		ga.GOID = "tank1";
		e.sponsorId = "0";
		e.targetIdList = World.GetInstance ().GetAllGOIds ();
		e.type = UserEvent.EventType.GA;
		e.rawContent = ga as object;
		base.BroadCastEvent (e);
	}

    public override void GenerateEvent(object boxed)
    {
		if (!(boxed is UserEvent)) {
			Test();
			return;
		}
		var e = boxed as UserEvent;
		e.sponsorId = "simulator";
		e.targetIdList = World.GetInstance ().GetAllGOIds ();
		base.BroadCastEvent (e);
    }
	
}
