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
        e.targetIdList = new List<string>() { base.gameObjectId};
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
        Debug.Log("detect movement");
		if (!state.position.Equals (gameObject.transform.position)) {
			var content = new Content();
            Debug.Log("position trans");
			content.state = StateEnum.POS;
            var value = new CubeEvent.Vector3();
            content.value = value;
			content.value.x = gameObject.transform.position.x;
			content.value.y = gameObject.transform.position.y;
			content.value.z = gameObject.transform.position.z;
			ste.content.Add(content);
            state.position = gameObject.transform.position;
		}
		if (!state.rotation.Equals (gameObject.transform.eulerAngles)) {
            Debug.Log("rotation trans");
			var content = new Content();
            var value = new CubeEvent.Vector3();
            content.value = value;
			content.state = StateEnum.ROT;
			content.value.x = transform.eulerAngles.x;
			content.value.y = transform.eulerAngles.y;
			content.value.z = transform.eulerAngles.z;
			ste.content.Add(content);
            state.rotation = gameObject.transform.eulerAngles;
		}
        e.type = UserEvent.EventType.ST;
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
