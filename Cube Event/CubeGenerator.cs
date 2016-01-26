using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using CubeEvent;
using ProtoBuf;

public class CubeGenerator : EventGenerator {

	private void DetectUserInput() {
		var e = new UserEvent ();
		e.type = UserEvent.EventType.CTR;
		e.sponsorId = base.gameObjectId;
		e.targetIdList.Add (base.gameObjectId);
		var ce = new CubeCE ();
		if (Input.GetKey (KeyCode.W)) {
			ce.key.Add("W");
		}
		if (Input.GetKey (KeyCode.S)) {
			ce.key.Add("S");
		}
		if (Input.GetKey (KeyCode.A)) {
			ce.key.Add("A");
		}
		if (Input.GetKey (KeyCode.D)) {
			ce.key.Add("D");
		}
		if (ce.key.Count == 0) {
			return ;
		}

		base.BroadCastEvent (e);
	}

	private void DetectStateTransfer() {
		var state = gameObject.GetComponent<CubeState> ();
		var e = new UserEvent ();
		e.sponsorId = base.gameObjectId;
		e.targetIdList = World.GetInstance ().GetAllGOIds ();
		var ste = new CubeSTE ();
		if (!state.transform.position.Equals (gameObject.transform.position)) {
			var content = new Content();
			content.state = StateEnum.POS;
			content.value.x = gameObject.transform.position.x;
			content.value.y = gameObject.transform.position.y;
			content.value.z = gameObject.transform.position.z;
			ste.content.Add(content);
		}
		if (!state.transform.eulerAngles.Equals (gameObject.transform.eulerAngles)) {
			var content = new Content();
			content.state = StateEnum.ROT;
			content.value.x = transform.eulerAngles.x;
			content.value.y = transform.eulerAngles.y;
			content.value.z = transform.eulerAngles.z;
			ste.content.Add(content);
		}
		e.rawContent = ste as object;
		if (ste.content.Count == 0) {
			return ;
		}

		base.BroadCastEvent (e);
	}

	public override void GenerateEvent (object boxed)
	{
		DetectUserInput ();
		if (gameObject.GetComponent<State> ().isInSimulator) {
			DetectStateTransfer ();
		}
	}

	public override byte[] SelfSerialize (UserEvent.EventType type, object content)
	{
		byte[] ret = null;
		using (var stream = new MemoryStream()) {
			switch(type) {
			case UserEvent.EventType.CTR:
			{
				Serializer.Serialize<CubeCE>(stream, content as CubeCE);
				ret = stream.ToArray();
				break;
			}
			case UserEvent.EventType.ST:
			{
				Serializer.Serialize<CubeSTE>(stream, content as CubeSTE);
				ret = stream .ToArray();
				break;
			}
			}
		}

		return ret;
	
	}
	
}
