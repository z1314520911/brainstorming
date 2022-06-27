using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Commons
{
    public class Dict
    {
        public static SqlParameter AdminUser_Id(object Value)
        {
            return new SqlParameter("@Id", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter AdminUser_UserName(object Value)
        {
            return new SqlParameter("@UserName", SqlDbType.NVarChar, 255) { Value = Common.IsString(Value) };
        }
        public static SqlParameter AdminUser_PassWord(object Value)
        {
            return new SqlParameter("@PassWord", SqlDbType.NVarChar, 255) { Value = Common.IsString(Value) };
        }
        public static SqlParameter AdminUser_Per(object Value)
        {
            return new SqlParameter("@Per", SqlDbType.NText, 4000) { Value = Common.IsString(Value) };
        }
        public static SqlParameter AdminUser_Name(object Value)
        {
            return new SqlParameter("@Name", SqlDbType.NVarChar, 255) { Value = Common.IsString(Value) };
        }
        public static SqlParameter AdminUser_Status(object Value)
        {
            return new SqlParameter("@Status", SqlDbType.Bit, 0) { Value = Common.IsBool(Value) };
        }
        public static SqlParameter AdminUser_AddTime(object Value)
        {
            return new SqlParameter("@AddTime", SqlDbType.DateTime, 0) { Value = Common.IsDateTime(Value) };
        }
        public static SqlParameter T_User_Id(object Value)
        {
            return new SqlParameter("@Id", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_User_UserId(object Value)
        {
            return new SqlParameter("@UserId", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_User_StoreId(object Value)
        {
            return new SqlParameter("@StoreId", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_User_Name(object Value)
        {
            return new SqlParameter("@Name", SqlDbType.NVarChar, 60) { Value = Common.IsString(Value) };
        }
        public static SqlParameter T_User_LoginId(object Value)
        {
            return new SqlParameter("@LoginId", SqlDbType.NVarChar, 60) { Value = Common.IsString(Value) };
        }
        public static SqlParameter T_User_Password(object Value)
        {
            return new SqlParameter("@Password", SqlDbType.NVarChar, 60) { Value = Common.IsString(Value) };
        }
        public static SqlParameter T_User_UnionID(object Value)
        {
            return new SqlParameter("@UnionID", SqlDbType.NVarChar, 40) { Value = Common.IsString(Value) };
        }
        public static SqlParameter T_User_OpenId(object Value)
        {
            return new SqlParameter("@OpenId", SqlDbType.NVarChar, 60) { Value = Common.IsString(Value) };
        }
        public static SqlParameter T_User_WxOpenId(object Value)
        {
            return new SqlParameter("@WxOpenId", SqlDbType.NVarChar, 40) { Value = Common.IsString(Value) };
        }
        public static SqlParameter T_User_Guid(object Value)
        {
            return new SqlParameter("@Guid", SqlDbType.NVarChar, 50) { Value = Common.IsString(Value) };
        }
        public static SqlParameter T_User_NickName(object Value)
        {
            return new SqlParameter("@NickName", SqlDbType.NVarChar, 50) { Value = Common.IsString(Value) };
        }
        public static SqlParameter T_User_Phone(object Value)
        {
            return new SqlParameter("@Phone", SqlDbType.NVarChar, 11) { Value = Common.IsString(Value) };
        }
        public static SqlParameter T_User_EMail(object Value)
        {
            return new SqlParameter("@EMail", SqlDbType.NVarChar, 60) { Value = Common.IsString(Value) };
        }
        public static SqlParameter T_User_Money(object Value)
        {
            return new SqlParameter("@Money", SqlDbType.Decimal, 0) { Value = Common.IsDecimal(Value) };
        }
        public static SqlParameter T_User_IsPayUser(object Value)
        {
            return new SqlParameter("@IsPayUser", SqlDbType.Bit, 0) { Value = Common.IsBool(Value) };
        }
        public static SqlParameter T_User_QRCode(object Value)
        {
            return new SqlParameter("@QRCode", SqlDbType.NVarChar, 50) { Value = Common.IsString(Value) };
        }
        public static SqlParameter T_User_LevelId(object Value)
        {
            return new SqlParameter("@LevelId", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_User_TypeId(object Value)
        {
            return new SqlParameter("@TypeId", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_User_Avatar(object Value)
        {
            return new SqlParameter("@Avatar", SqlDbType.NVarChar, 300) { Value = Common.IsString(Value) };
        }
        public static SqlParameter T_User_Sex(object Value)
        {
            return new SqlParameter("@Sex", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_User_Province(object Value)
        {
            return new SqlParameter("@Province", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_User_City(object Value)
        {
            return new SqlParameter("@City", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_User_Area(object Value)
        {
            return new SqlParameter("@Area", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_User_Address(object Value)
        {
            return new SqlParameter("@Address", SqlDbType.NVarChar, 500) { Value = Common.IsString(Value) };
        }
        public static SqlParameter T_User_CarCard(object Value)
        {
            return new SqlParameter("@CarCard", SqlDbType.NVarChar, 300) { Value = Common.IsString(Value) };
        }
        public static SqlParameter T_User_Intro(object Value)
        {
            return new SqlParameter("@Intro", SqlDbType.NText, 4000) { Value = Common.IsString(Value) };
        }
        public static SqlParameter T_User_TheTime(object Value)
        {
            return new SqlParameter("@TheTime", SqlDbType.DateTime, 0) { Value = Common.IsDateTime(Value) };
        }
        public static SqlParameter T_User_TheIp(object Value)
        {
            return new SqlParameter("@TheIp", SqlDbType.NVarChar, 60) { Value = Common.IsString(Value) };
        }
        public static SqlParameter T_User_LastTime(object Value)
        {
            return new SqlParameter("@LastTime", SqlDbType.DateTime, 0) { Value = Common.IsDateTime(Value) };
        }
        public static SqlParameter T_User_LastIp(object Value)
        {
            return new SqlParameter("@LastIp", SqlDbType.NVarChar, 60) { Value = Common.IsString(Value) };
        }
        public static SqlParameter T_User_Status(object Value)
        {
            return new SqlParameter("@Status", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_User_Subscribe(object Value)
        {
            return new SqlParameter("@Subscribe", SqlDbType.Bit, 0) { Value = Common.IsBool(Value) };
        }
        public static SqlParameter T_User_AddTime(object Value)
        {
            return new SqlParameter("@AddTime", SqlDbType.DateTime, 0) { Value = Common.IsDateTime(Value) };
        }
        public static SqlParameter T_Vote_Id(object Value)
        {
            return new SqlParameter("@Id", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_Vote_Name(object Value)
        {
            return new SqlParameter("@Name", SqlDbType.NVarChar, 500) { Value = Common.IsString(Value) };
        }
        public static SqlParameter T_Vote_UserId(object Value)
        {
            return new SqlParameter("@UserId", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_Vote_Room(object Value)
        {
            return new SqlParameter("@Room", SqlDbType.NVarChar, 10) { Value = Common.IsString(Value) };
        }
        public static SqlParameter T_Vote_IsStart(object Value)
        {
            return new SqlParameter("@IsStart", SqlDbType.Bit, 0) { Value = Common.IsBool(Value) };
        }
        public static SqlParameter T_Vote_Status(object Value)
        {
            return new SqlParameter("@Status", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_Vote_AddTime(object Value)
        {
            return new SqlParameter("@AddTime", SqlDbType.DateTime, 0) { Value = Common.IsDateTime(Value) };
        }
        public static SqlParameter T_VoteUser_Id(object Value)
        {
            return new SqlParameter("@Id", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_VoteUser_VoteId(object Value)
        {
            return new SqlParameter("@VoteId", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_VoteUser_UserId(object Value)
        {
            return new SqlParameter("@UserId", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_VoteUser_TypeId(object Value)
        {
            return new SqlParameter("@TypeId", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_VoteUser_AddTime(object Value)
        {
            return new SqlParameter("@AddTime", SqlDbType.DateTime, 0) { Value = Common.IsDateTime(Value) };
        }
        public static SqlParameter T_VoteView_Id(object Value)
        {
            return new SqlParameter("@Id", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_VoteView_VoteId(object Value)
        {
            return new SqlParameter("@VoteId", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_VoteView_UserId(object Value)
        {
            return new SqlParameter("@UserId", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_VoteView_VoteUserId(object Value)
        {
            return new SqlParameter("@VoteUserId", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_VoteView_Idea(object Value)
        {
            return new SqlParameter("@Idea", SqlDbType.NVarChar, -1) { Value = Common.IsString(Value) };
        }
        public static SqlParameter T_VoteView_AddTime(object Value)
        {
            return new SqlParameter("@AddTime", SqlDbType.DateTime, 0) { Value = Common.IsDateTime(Value) };
        }
        public static SqlParameter T_VoteLike_Id(object Value)
        {
            return new SqlParameter("@Id", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_VoteLike_VoteId(object Value)
        {
            return new SqlParameter("@VoteId", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_VoteLike_ViewId(object Value)
        {
            return new SqlParameter("@ViewId", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_VoteLike_UserId(object Value)
        {
            return new SqlParameter("@UserId", SqlDbType.Int, 0) { Value = Common.IsInt(Value) };
        }
        public static SqlParameter T_VoteLike_AddTime(object Value)
        {
            return new SqlParameter("@AddTime", SqlDbType.DateTime, 0) { Value = Common.IsDateTime(Value) };
        }

    }
}
