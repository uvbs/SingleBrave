﻿//  FriendAcceptApplyHandle.cs
//  Author: Cheng Xia
//  2013-1-13

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;

/// <summary>
/// 好友申请列表句柄
/// </summary>
public class FriendAcceptApplyHandle
{
    /// <summary>
    /// 获取action
    /// </summary>
    /// <returns></returns>
    public static string GetAction()
    {
        return PACKET_DEFINE.FRIEND_ACCEPTAPPLY_REQ;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static void Excute(HTTPPacketAck packet)
    {
        FriendAcceptApplyPktAck ack = (FriendAcceptApplyPktAck)packet;

        GUIFriendApply gui_friendApply = (GUIFriendApply)GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_FRIENDAPPLY);

        GUI_FUNCTION.LOADING_HIDEN();

        if (ack.header.code != 0)
        {
            GUI_FUNCTION.MESSAGEL(null, ack.header.desc);
            
        }

        //加入我的好友列表
        Friend friend = ack.m_cNewFriend.GetFriend();
        Role.role.GetFriendProperty().AddFriend(friend);

        Role.role.GetFriendProperty().RemoveFriendApply(gui_friendApply.m_cFirend.m_iID);

        GUIBackFrameBottom tmpb = GameManager.GetInstance().GetGUIManager().GetGUI(GUI_DEFINE.GUIID_BACKFRAMEBOTTOM) as GUIBackFrameBottom;

        if (Role.role.GetFriendProperty().GetAllApply() != null)
        {
            Role.role.GetBaseProperty().m_iFriendApplyCount = Role.role.GetFriendProperty().GetAllApply().Count(q => { return q.m_iState != 0; }); 
        }
        
        if (tmpb.IsShow())
        {
            tmpb.SetFriendApllyNumber(Role.role.GetBaseProperty().m_iFriendApplyCount + Role.role.GetBaseProperty().m_iFriendGiftCount);
        }

        gui_friendApply.Show();

        
    }
}
