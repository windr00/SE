using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myproto;

public class EventDispatch
{

    public static void DispatchEvent(List<MsgResponse> command)
    {
        foreach (var c in command)
        {
            var e = new UserEvent();
            e.sponsorId = c.head.srcID;
            e.targetIdList = c.head.dstIDs;
            foreach (var m in c.content.msg)
            {
                e.type = Support.EventTypeConverter(m.type);
                switch (m.type)
                {
                    case MsgType.AssetLoad:
                    case MsgType.AssetUpload:
                    case MsgType.GameObjectAdd:
                    case MsgType.GameObjectRemove:
                        {
                            var prego = World.GetInstance().GetPredefiendGO();
                            var preHandler = prego.GetComponent<EventHandler>();
                            e.rawContent = preHandler.SelfDeserialize(e.type, m.body);
                            preHandler.Handle(e);
                            break;
                        }
                    case MsgType.Control:
                    case MsgType.Command:
                    case MsgType.Environment:
                    case MsgType.StateTransfer:
                        {
                            foreach (var item in e.targetIdList)
                            {
                                var go = World.GetInstance().GetGameObject(item);
                                var handler = go.GetComponent<EventHandler>();
                                e.rawContent = handler.SelfDeserialize(e.type, m.body);
                                handler.Handle(e);
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }
    }

    
}