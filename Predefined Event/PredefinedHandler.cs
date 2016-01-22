using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ProtoBuf;
using PredefinedProto;

public class PredefinedHandler : EventHandler
{
    public override object SelfDeserialize(UserEvent.EventType type, byte[] body)
    {
		object ret = null;
		using (var stream = new MemoryStream(body)) {
			switch (type) {
			case UserEvent.EventType.GA:
			{
				ret = Serializer.Deserialize<GA>(stream);			
				break;
			}
			case UserEvent.EventType.GR:
			{
				ret = Serializer.Deserialize<GR>(stream);
				break;
			}
			case UserEvent.EventType.LA:
			{
				ret = Serializer.Deserialize<LA>(stream);
				break;
			}
			}
		}
		return ret;
    }

    public override void Handle(UserEvent e)
    {
        switch (e.type)
        {
            case UserEvent.EventType.LA:
                {
                    GameObjectLoader.LoadGameObject((e.rawContent as LA).AssetName);
                    break;
                }
            case UserEvent.EventType.GA:
                {
					var proto = e.rawContent as GA;
					GOCollection.AddGameObject(proto.GOID,proto.AssetName);

                    break;
                }
            case UserEvent.EventType.GR:
                {
					GOCollection.DeleteGameObjectByGOId((e.rawContent as GR).GOID);
                    break;
                }
            default:
                {
                    return;
                }
        }
    }
}