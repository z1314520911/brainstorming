using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using DBHelper;
using Model;
using Commons;

namespace DAL
{
    public partial class ModelService_Sql
    {
        public int Insert(AdminUser model)
        {
            string sql = "Insert into " + Config.DatabasePrefix("AdminUser") + "([UserName],[PassWord],[Per],[Name],[Status],[AddTime]) values (@UserName,@PassWord,@Per,@Name,@Status,@AddTime)"; 
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(Dict.AdminUser_UserName(Common.IsString(model.UserName)));
            param.Add(Dict.AdminUser_PassWord(Common.IsString(model.PassWord)));
            param.Add(Dict.AdminUser_Per(Common.IsString(model.Per)));
            param.Add(Dict.AdminUser_Name(Common.IsString(model.Name)));
            param.Add(Dict.AdminUser_Status(Common.IsBool(model.Status)));
            param.Add(Dict.AdminUser_AddTime(Common.IsDateTime(model.AddTime)));
            return SqlHelper.ExecuteNonQuery(SqlHelper.connstr, CommandType.Text, sql, param.ToArray());
        }
        public int InsertOne(AdminUser model)
        {
            string sql = "Insert into " + Config.DatabasePrefix("AdminUser") + "([UserName],[PassWord],[Per],[Name],[Status],[AddTime]) values (@UserName,@PassWord,@Per,@Name,@Status,@AddTime);select @@identity as Id;";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(Dict.AdminUser_UserName(Common.IsString(model.UserName)));
            param.Add(Dict.AdminUser_PassWord(Common.IsString(model.PassWord)));
            param.Add(Dict.AdminUser_Per(Common.IsString(model.Per)));
            param.Add(Dict.AdminUser_Name(Common.IsString(model.Name)));
            param.Add(Dict.AdminUser_Status(Common.IsBool(model.Status)));
            param.Add(Dict.AdminUser_AddTime(Common.IsDateTime(model.AddTime)));
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.connstr, CommandType.Text, sql, param.ToArray());
            int n = 0;
            while (reader.Read())
            {
                n = Convert.ToInt32(reader["Id"]);
            }
            reader.Close();
            return n;
        }
        public int Update(AdminUser model, string prohibit = "")
        {
            string sql = "Update " + Config.DatabasePrefix("AdminUser") + " set "; 
            List<SqlParameter> param = new List<SqlParameter>();
            if (prohibit.IndexOf("UserName") == -1)
            {
                param.Add(Dict.AdminUser_UserName(Common.IsString(model.UserName)));
                sql += "[UserName]=@UserName,";
            }
            if (prohibit.IndexOf("PassWord") == -1)
            {
                param.Add(Dict.AdminUser_PassWord(Common.IsString(model.PassWord)));
                sql += "[PassWord]=@PassWord,";
            }
            if (prohibit.IndexOf("Per") == -1)
            {
                param.Add(Dict.AdminUser_Per(Common.IsString(model.Per)));
                sql += "[Per]=@Per,";
            }
            if (prohibit.IndexOf("Name") == -1)
            {
                param.Add(Dict.AdminUser_Name(Common.IsString(model.Name)));
                sql += "[Name]=@Name,";
            }
            if (prohibit.IndexOf("Status") == -1)
            {
                param.Add(Dict.AdminUser_Status(Common.IsBool(model.Status)));
                sql += "[Status]=@Status,";
            }
            if (prohibit.IndexOf("AddTime") == -1)
            {
                param.Add(Dict.AdminUser_AddTime(Common.IsDateTime(model.AddTime)));
                sql += "[AddTime]=@AddTime,";
            }
            sql = sql.TrimEnd(',') + " where Id=@Id";
            param.Add(Dict.AdminUser_Id(Common.IsInt(model.Id)));
            return SqlHelper.ExecuteNonQuery(SqlHelper.connstr, CommandType.Text, sql, param.ToArray());
        }
        public int Insert(T_User model)
        {
            string sql = "Insert into " + Config.DatabasePrefix("T_User") + "([UserId],[StoreId],[Name],[LoginId],[Password],[UnionID],[OpenId],[WxOpenId],[Guid],[NickName],[Phone],[EMail],[Money],[IsPayUser],[QRCode],[LevelId],[TypeId],[Avatar],[Sex],[Province],[City],[Area],[Address],[CarCard],[Intro],[TheTime],[TheIp],[LastTime],[LastIp],[Status],[Subscribe],[AddTime]) values (@UserId,@StoreId,@Name,@LoginId,@Password,@UnionID,@OpenId,@WxOpenId,@Guid,@NickName,@Phone,@EMail,@Money,@IsPayUser,@QRCode,@LevelId,@TypeId,@Avatar,@Sex,@Province,@City,@Area,@Address,@CarCard,@Intro,@TheTime,@TheIp,@LastTime,@LastIp,@Status,@Subscribe,@AddTime)"; 
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(Dict.T_User_UserId(Common.IsInt(model.UserId)));
            param.Add(Dict.T_User_StoreId(Common.IsInt(model.StoreId)));
            param.Add(Dict.T_User_Name(Common.IsString(model.Name)));
            param.Add(Dict.T_User_LoginId(Common.IsString(model.LoginId)));
            param.Add(Dict.T_User_Password(Common.IsString(model.Password)));
            param.Add(Dict.T_User_UnionID(Common.IsString(model.UnionID)));
            param.Add(Dict.T_User_OpenId(Common.IsString(model.OpenId)));
            param.Add(Dict.T_User_WxOpenId(Common.IsString(model.WxOpenId)));
            param.Add(Dict.T_User_Guid(Common.IsString(model.Guid)));
            param.Add(Dict.T_User_NickName(Common.IsString(model.NickName)));
            param.Add(Dict.T_User_Phone(Common.IsString(model.Phone)));
            param.Add(Dict.T_User_EMail(Common.IsString(model.EMail)));
            param.Add(Dict.T_User_Money(Common.IsDecimal(model.Money)));
            param.Add(Dict.T_User_IsPayUser(Common.IsBool(model.IsPayUser)));
            param.Add(Dict.T_User_QRCode(Common.IsString(model.QRCode)));
            param.Add(Dict.T_User_LevelId(Common.IsInt(model.LevelId)));
            param.Add(Dict.T_User_TypeId(Common.IsInt(model.TypeId)));
            param.Add(Dict.T_User_Avatar(Common.IsString(model.Avatar)));
            param.Add(Dict.T_User_Sex(Common.IsInt(model.Sex)));
            param.Add(Dict.T_User_Province(Common.IsInt(model.Province)));
            param.Add(Dict.T_User_City(Common.IsInt(model.City)));
            param.Add(Dict.T_User_Area(Common.IsInt(model.Area)));
            param.Add(Dict.T_User_Address(Common.IsString(model.Address)));
            param.Add(Dict.T_User_CarCard(Common.IsString(model.CarCard)));
            param.Add(Dict.T_User_Intro(Common.IsString(model.Intro)));
            param.Add(Dict.T_User_TheTime(Common.IsDateTime(model.TheTime)));
            param.Add(Dict.T_User_TheIp(Common.IsString(model.TheIp)));
            param.Add(Dict.T_User_LastTime(Common.IsDateTime(model.LastTime)));
            param.Add(Dict.T_User_LastIp(Common.IsString(model.LastIp)));
            param.Add(Dict.T_User_Status(Common.IsInt(model.Status)));
            param.Add(Dict.T_User_Subscribe(Common.IsBool(model.Subscribe)));
            param.Add(Dict.T_User_AddTime(Common.IsDateTime(model.AddTime)));
            return SqlHelper.ExecuteNonQuery(SqlHelper.connstr, CommandType.Text, sql, param.ToArray());
        }
        public int InsertOne(T_User model)
        {
            string sql = "Insert into " + Config.DatabasePrefix("T_User") + "([UserId],[StoreId],[Name],[LoginId],[Password],[UnionID],[OpenId],[WxOpenId],[Guid],[NickName],[Phone],[EMail],[Money],[IsPayUser],[QRCode],[LevelId],[TypeId],[Avatar],[Sex],[Province],[City],[Area],[Address],[CarCard],[Intro],[TheTime],[TheIp],[LastTime],[LastIp],[Status],[Subscribe],[AddTime]) values (@UserId,@StoreId,@Name,@LoginId,@Password,@UnionID,@OpenId,@WxOpenId,@Guid,@NickName,@Phone,@EMail,@Money,@IsPayUser,@QRCode,@LevelId,@TypeId,@Avatar,@Sex,@Province,@City,@Area,@Address,@CarCard,@Intro,@TheTime,@TheIp,@LastTime,@LastIp,@Status,@Subscribe,@AddTime);select @@identity as Id;";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(Dict.T_User_UserId(Common.IsInt(model.UserId)));
            param.Add(Dict.T_User_StoreId(Common.IsInt(model.StoreId)));
            param.Add(Dict.T_User_Name(Common.IsString(model.Name)));
            param.Add(Dict.T_User_LoginId(Common.IsString(model.LoginId)));
            param.Add(Dict.T_User_Password(Common.IsString(model.Password)));
            param.Add(Dict.T_User_UnionID(Common.IsString(model.UnionID)));
            param.Add(Dict.T_User_OpenId(Common.IsString(model.OpenId)));
            param.Add(Dict.T_User_WxOpenId(Common.IsString(model.WxOpenId)));
            param.Add(Dict.T_User_Guid(Common.IsString(model.Guid)));
            param.Add(Dict.T_User_NickName(Common.IsString(model.NickName)));
            param.Add(Dict.T_User_Phone(Common.IsString(model.Phone)));
            param.Add(Dict.T_User_EMail(Common.IsString(model.EMail)));
            param.Add(Dict.T_User_Money(Common.IsDecimal(model.Money)));
            param.Add(Dict.T_User_IsPayUser(Common.IsBool(model.IsPayUser)));
            param.Add(Dict.T_User_QRCode(Common.IsString(model.QRCode)));
            param.Add(Dict.T_User_LevelId(Common.IsInt(model.LevelId)));
            param.Add(Dict.T_User_TypeId(Common.IsInt(model.TypeId)));
            param.Add(Dict.T_User_Avatar(Common.IsString(model.Avatar)));
            param.Add(Dict.T_User_Sex(Common.IsInt(model.Sex)));
            param.Add(Dict.T_User_Province(Common.IsInt(model.Province)));
            param.Add(Dict.T_User_City(Common.IsInt(model.City)));
            param.Add(Dict.T_User_Area(Common.IsInt(model.Area)));
            param.Add(Dict.T_User_Address(Common.IsString(model.Address)));
            param.Add(Dict.T_User_CarCard(Common.IsString(model.CarCard)));
            param.Add(Dict.T_User_Intro(Common.IsString(model.Intro)));
            param.Add(Dict.T_User_TheTime(Common.IsDateTime(model.TheTime)));
            param.Add(Dict.T_User_TheIp(Common.IsString(model.TheIp)));
            param.Add(Dict.T_User_LastTime(Common.IsDateTime(model.LastTime)));
            param.Add(Dict.T_User_LastIp(Common.IsString(model.LastIp)));
            param.Add(Dict.T_User_Status(Common.IsInt(model.Status)));
            param.Add(Dict.T_User_Subscribe(Common.IsBool(model.Subscribe)));
            param.Add(Dict.T_User_AddTime(Common.IsDateTime(model.AddTime)));
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.connstr, CommandType.Text, sql, param.ToArray());
            int n = 0;
            while (reader.Read())
            {
                n = Convert.ToInt32(reader["Id"]);
            }
            reader.Close();
            return n;
        }
        public int Update(T_User model, string prohibit = "")
        {
            string sql = "Update " + Config.DatabasePrefix("T_User") + " set "; 
            List<SqlParameter> param = new List<SqlParameter>();
            if (prohibit.IndexOf("UserId") == -1)
            {
                param.Add(Dict.T_User_UserId(Common.IsInt(model.UserId)));
                sql += "[UserId]=@UserId,";
            }
            if (prohibit.IndexOf("StoreId") == -1)
            {
                param.Add(Dict.T_User_StoreId(Common.IsInt(model.StoreId)));
                sql += "[StoreId]=@StoreId,";
            }
            if (prohibit.IndexOf("Name") == -1)
            {
                param.Add(Dict.T_User_Name(Common.IsString(model.Name)));
                sql += "[Name]=@Name,";
            }
            if (prohibit.IndexOf("LoginId") == -1)
            {
                param.Add(Dict.T_User_LoginId(Common.IsString(model.LoginId)));
                sql += "[LoginId]=@LoginId,";
            }
            if (prohibit.IndexOf("Password") == -1)
            {
                param.Add(Dict.T_User_Password(Common.IsString(model.Password)));
                sql += "[Password]=@Password,";
            }
            if (prohibit.IndexOf("UnionID") == -1)
            {
                param.Add(Dict.T_User_UnionID(Common.IsString(model.UnionID)));
                sql += "[UnionID]=@UnionID,";
            }
            if (prohibit.IndexOf("OpenId") == -1)
            {
                param.Add(Dict.T_User_OpenId(Common.IsString(model.OpenId)));
                sql += "[OpenId]=@OpenId,";
            }
            if (prohibit.IndexOf("WxOpenId") == -1)
            {
                param.Add(Dict.T_User_WxOpenId(Common.IsString(model.WxOpenId)));
                sql += "[WxOpenId]=@WxOpenId,";
            }
            if (prohibit.IndexOf("Guid") == -1)
            {
                param.Add(Dict.T_User_Guid(Common.IsString(model.Guid)));
                sql += "[Guid]=@Guid,";
            }
            if (prohibit.IndexOf("NickName") == -1)
            {
                param.Add(Dict.T_User_NickName(Common.IsString(model.NickName)));
                sql += "[NickName]=@NickName,";
            }
            if (prohibit.IndexOf("Phone") == -1)
            {
                param.Add(Dict.T_User_Phone(Common.IsString(model.Phone)));
                sql += "[Phone]=@Phone,";
            }
            if (prohibit.IndexOf("EMail") == -1)
            {
                param.Add(Dict.T_User_EMail(Common.IsString(model.EMail)));
                sql += "[EMail]=@EMail,";
            }
            if (prohibit.IndexOf("Money") == -1)
            {
                param.Add(Dict.T_User_Money(Common.IsDecimal(model.Money)));
                sql += "[Money]=@Money,";
            }
            if (prohibit.IndexOf("IsPayUser") == -1)
            {
                param.Add(Dict.T_User_IsPayUser(Common.IsBool(model.IsPayUser)));
                sql += "[IsPayUser]=@IsPayUser,";
            }
            if (prohibit.IndexOf("QRCode") == -1)
            {
                param.Add(Dict.T_User_QRCode(Common.IsString(model.QRCode)));
                sql += "[QRCode]=@QRCode,";
            }
            if (prohibit.IndexOf("LevelId") == -1)
            {
                param.Add(Dict.T_User_LevelId(Common.IsInt(model.LevelId)));
                sql += "[LevelId]=@LevelId,";
            }
            if (prohibit.IndexOf("TypeId") == -1)
            {
                param.Add(Dict.T_User_TypeId(Common.IsInt(model.TypeId)));
                sql += "[TypeId]=@TypeId,";
            }
            if (prohibit.IndexOf("Avatar") == -1)
            {
                param.Add(Dict.T_User_Avatar(Common.IsString(model.Avatar)));
                sql += "[Avatar]=@Avatar,";
            }
            if (prohibit.IndexOf("Sex") == -1)
            {
                param.Add(Dict.T_User_Sex(Common.IsInt(model.Sex)));
                sql += "[Sex]=@Sex,";
            }
            if (prohibit.IndexOf("Province") == -1)
            {
                param.Add(Dict.T_User_Province(Common.IsInt(model.Province)));
                sql += "[Province]=@Province,";
            }
            if (prohibit.IndexOf("City") == -1)
            {
                param.Add(Dict.T_User_City(Common.IsInt(model.City)));
                sql += "[City]=@City,";
            }
            if (prohibit.IndexOf("Area") == -1)
            {
                param.Add(Dict.T_User_Area(Common.IsInt(model.Area)));
                sql += "[Area]=@Area,";
            }
            if (prohibit.IndexOf("Address") == -1)
            {
                param.Add(Dict.T_User_Address(Common.IsString(model.Address)));
                sql += "[Address]=@Address,";
            }
            if (prohibit.IndexOf("CarCard") == -1)
            {
                param.Add(Dict.T_User_CarCard(Common.IsString(model.CarCard)));
                sql += "[CarCard]=@CarCard,";
            }
            if (prohibit.IndexOf("Intro") == -1)
            {
                param.Add(Dict.T_User_Intro(Common.IsString(model.Intro)));
                sql += "[Intro]=@Intro,";
            }
            if (prohibit.IndexOf("TheTime") == -1)
            {
                param.Add(Dict.T_User_TheTime(Common.IsDateTime(model.TheTime)));
                sql += "[TheTime]=@TheTime,";
            }
            if (prohibit.IndexOf("TheIp") == -1)
            {
                param.Add(Dict.T_User_TheIp(Common.IsString(model.TheIp)));
                sql += "[TheIp]=@TheIp,";
            }
            if (prohibit.IndexOf("LastTime") == -1)
            {
                param.Add(Dict.T_User_LastTime(Common.IsDateTime(model.LastTime)));
                sql += "[LastTime]=@LastTime,";
            }
            if (prohibit.IndexOf("LastIp") == -1)
            {
                param.Add(Dict.T_User_LastIp(Common.IsString(model.LastIp)));
                sql += "[LastIp]=@LastIp,";
            }
            if (prohibit.IndexOf("Status") == -1)
            {
                param.Add(Dict.T_User_Status(Common.IsInt(model.Status)));
                sql += "[Status]=@Status,";
            }
            if (prohibit.IndexOf("Subscribe") == -1)
            {
                param.Add(Dict.T_User_Subscribe(Common.IsBool(model.Subscribe)));
                sql += "[Subscribe]=@Subscribe,";
            }
            if (prohibit.IndexOf("AddTime") == -1)
            {
                param.Add(Dict.T_User_AddTime(Common.IsDateTime(model.AddTime)));
                sql += "[AddTime]=@AddTime,";
            }
            sql = sql.TrimEnd(',') + " where Id=@Id";
            param.Add(Dict.T_User_Id(Common.IsInt(model.Id)));
            return SqlHelper.ExecuteNonQuery(SqlHelper.connstr, CommandType.Text, sql, param.ToArray());
        }
        public int Insert(T_Vote model)
        {
            string sql = "Insert into " + Config.DatabasePrefix("T_Vote") + "([Name],[UserId],[Room],[IsStart],[Status],[AddTime]) values (@Name,@UserId,@Room,@IsStart,@Status,@AddTime)"; 
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(Dict.T_Vote_Name(Common.IsString(model.Name)));
            param.Add(Dict.T_Vote_UserId(Common.IsInt(model.UserId)));
            param.Add(Dict.T_Vote_Room(Common.IsString(model.Room)));
            param.Add(Dict.T_Vote_IsStart(Common.IsBool(model.IsStart)));
            param.Add(Dict.T_Vote_Status(Common.IsInt(model.Status)));
            param.Add(Dict.T_Vote_AddTime(Common.IsDateTime(model.AddTime)));
            return SqlHelper.ExecuteNonQuery(SqlHelper.connstr, CommandType.Text, sql, param.ToArray());
        }
        public int InsertOne(T_Vote model)
        {
            string sql = "Insert into " + Config.DatabasePrefix("T_Vote") + "([Name],[UserId],[Room],[IsStart],[Status],[AddTime]) values (@Name,@UserId,@Room,@IsStart,@Status,@AddTime);select @@identity as Id;";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(Dict.T_Vote_Name(Common.IsString(model.Name)));
            param.Add(Dict.T_Vote_UserId(Common.IsInt(model.UserId)));
            param.Add(Dict.T_Vote_Room(Common.IsString(model.Room)));
            param.Add(Dict.T_Vote_IsStart(Common.IsBool(model.IsStart)));
            param.Add(Dict.T_Vote_Status(Common.IsInt(model.Status)));
            param.Add(Dict.T_Vote_AddTime(Common.IsDateTime(model.AddTime)));
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.connstr, CommandType.Text, sql, param.ToArray());
            int n = 0;
            while (reader.Read())
            {
                n = Convert.ToInt32(reader["Id"]);
            }
            reader.Close();
            return n;
        }
        public int Update(T_Vote model, string prohibit = "")
        {
            string sql = "Update " + Config.DatabasePrefix("T_Vote") + " set "; 
            List<SqlParameter> param = new List<SqlParameter>();
            if (prohibit.IndexOf("Name") == -1)
            {
                param.Add(Dict.T_Vote_Name(Common.IsString(model.Name)));
                sql += "[Name]=@Name,";
            }
            if (prohibit.IndexOf("UserId") == -1)
            {
                param.Add(Dict.T_Vote_UserId(Common.IsInt(model.UserId)));
                sql += "[UserId]=@UserId,";
            }
            if (prohibit.IndexOf("Room") == -1)
            {
                param.Add(Dict.T_Vote_Room(Common.IsString(model.Room)));
                sql += "[Room]=@Room,";
            }
            if (prohibit.IndexOf("IsStart") == -1)
            {
                param.Add(Dict.T_Vote_IsStart(Common.IsBool(model.IsStart)));
                sql += "[IsStart]=@IsStart,";
            }
            if (prohibit.IndexOf("Status") == -1)
            {
                param.Add(Dict.T_Vote_Status(Common.IsInt(model.Status)));
                sql += "[Status]=@Status,";
            }
            if (prohibit.IndexOf("AddTime") == -1)
            {
                param.Add(Dict.T_Vote_AddTime(Common.IsDateTime(model.AddTime)));
                sql += "[AddTime]=@AddTime,";
            }
            sql = sql.TrimEnd(',') + " where Id=@Id";
            param.Add(Dict.T_Vote_Id(Common.IsInt(model.Id)));
            return SqlHelper.ExecuteNonQuery(SqlHelper.connstr, CommandType.Text, sql, param.ToArray());
        }
        public int Insert(T_VoteUser model)
        {
            string sql = "Insert into " + Config.DatabasePrefix("T_VoteUser") + "([VoteId],[UserId],[TypeId],[AddTime]) values (@VoteId,@UserId,@TypeId,@AddTime)"; 
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(Dict.T_VoteUser_VoteId(Common.IsInt(model.VoteId)));
            param.Add(Dict.T_VoteUser_UserId(Common.IsInt(model.UserId)));
            param.Add(Dict.T_VoteUser_TypeId(Common.IsInt(model.TypeId)));
            param.Add(Dict.T_VoteUser_AddTime(Common.IsDateTime(model.AddTime)));
            return SqlHelper.ExecuteNonQuery(SqlHelper.connstr, CommandType.Text, sql, param.ToArray());
        }
        public int InsertOne(T_VoteUser model)
        {
            string sql = "Insert into " + Config.DatabasePrefix("T_VoteUser") + "([VoteId],[UserId],[TypeId],[AddTime]) values (@VoteId,@UserId,@TypeId,@AddTime);select @@identity as Id;";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(Dict.T_VoteUser_VoteId(Common.IsInt(model.VoteId)));
            param.Add(Dict.T_VoteUser_UserId(Common.IsInt(model.UserId)));
            param.Add(Dict.T_VoteUser_TypeId(Common.IsInt(model.TypeId)));
            param.Add(Dict.T_VoteUser_AddTime(Common.IsDateTime(model.AddTime)));
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.connstr, CommandType.Text, sql, param.ToArray());
            int n = 0;
            while (reader.Read())
            {
                n = Convert.ToInt32(reader["Id"]);
            }
            reader.Close();
            return n;
        }
        public int Update(T_VoteUser model, string prohibit = "")
        {
            string sql = "Update " + Config.DatabasePrefix("T_VoteUser") + " set "; 
            List<SqlParameter> param = new List<SqlParameter>();
            if (prohibit.IndexOf("VoteId") == -1)
            {
                param.Add(Dict.T_VoteUser_VoteId(Common.IsInt(model.VoteId)));
                sql += "[VoteId]=@VoteId,";
            }
            if (prohibit.IndexOf("UserId") == -1)
            {
                param.Add(Dict.T_VoteUser_UserId(Common.IsInt(model.UserId)));
                sql += "[UserId]=@UserId,";
            }
            if (prohibit.IndexOf("TypeId") == -1)
            {
                param.Add(Dict.T_VoteUser_TypeId(Common.IsInt(model.TypeId)));
                sql += "[TypeId]=@TypeId,";
            }
            if (prohibit.IndexOf("AddTime") == -1)
            {
                param.Add(Dict.T_VoteUser_AddTime(Common.IsDateTime(model.AddTime)));
                sql += "[AddTime]=@AddTime,";
            }
            sql = sql.TrimEnd(',') + " where Id=@Id";
            param.Add(Dict.T_VoteUser_Id(Common.IsInt(model.Id)));
            return SqlHelper.ExecuteNonQuery(SqlHelper.connstr, CommandType.Text, sql, param.ToArray());
        }
        public int Insert(T_VoteView model)
        {
            string sql = "Insert into " + Config.DatabasePrefix("T_VoteView") + "([VoteId],[UserId],[VoteUserId],[Idea],[AddTime]) values (@VoteId,@UserId,@VoteUserId,@Idea,@AddTime)"; 
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(Dict.T_VoteView_VoteId(Common.IsInt(model.VoteId)));
            param.Add(Dict.T_VoteView_UserId(Common.IsInt(model.UserId)));
            param.Add(Dict.T_VoteView_VoteUserId(Common.IsInt(model.VoteUserId)));
            param.Add(Dict.T_VoteView_Idea(Common.IsString(model.Idea)));
            param.Add(Dict.T_VoteView_AddTime(Common.IsDateTime(model.AddTime)));
            return SqlHelper.ExecuteNonQuery(SqlHelper.connstr, CommandType.Text, sql, param.ToArray());
        }
        public int InsertOne(T_VoteView model)
        {
            string sql = "Insert into " + Config.DatabasePrefix("T_VoteView") + "([VoteId],[UserId],[VoteUserId],[Idea],[AddTime]) values (@VoteId,@UserId,@VoteUserId,@Idea,@AddTime);select @@identity as Id;";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(Dict.T_VoteView_VoteId(Common.IsInt(model.VoteId)));
            param.Add(Dict.T_VoteView_UserId(Common.IsInt(model.UserId)));
            param.Add(Dict.T_VoteView_VoteUserId(Common.IsInt(model.VoteUserId)));
            param.Add(Dict.T_VoteView_Idea(Common.IsString(model.Idea)));
            param.Add(Dict.T_VoteView_AddTime(Common.IsDateTime(model.AddTime)));
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.connstr, CommandType.Text, sql, param.ToArray());
            int n = 0;
            while (reader.Read())
            {
                n = Convert.ToInt32(reader["Id"]);
            }
            reader.Close();
            return n;
        }
        public int Update(T_VoteView model, string prohibit = "")
        {
            string sql = "Update " + Config.DatabasePrefix("T_VoteView") + " set "; 
            List<SqlParameter> param = new List<SqlParameter>();
            if (prohibit.IndexOf("VoteId") == -1)
            {
                param.Add(Dict.T_VoteView_VoteId(Common.IsInt(model.VoteId)));
                sql += "[VoteId]=@VoteId,";
            }
            if (prohibit.IndexOf("UserId") == -1)
            {
                param.Add(Dict.T_VoteView_UserId(Common.IsInt(model.UserId)));
                sql += "[UserId]=@UserId,";
            }
            if (prohibit.IndexOf("VoteUserId") == -1)
            {
                param.Add(Dict.T_VoteView_VoteUserId(Common.IsInt(model.VoteUserId)));
                sql += "[VoteUserId]=@VoteUserId,";
            }
            if (prohibit.IndexOf("Idea") == -1)
            {
                param.Add(Dict.T_VoteView_Idea(Common.IsString(model.Idea)));
                sql += "[Idea]=@Idea,";
            }
            if (prohibit.IndexOf("AddTime") == -1)
            {
                param.Add(Dict.T_VoteView_AddTime(Common.IsDateTime(model.AddTime)));
                sql += "[AddTime]=@AddTime,";
            }
            sql = sql.TrimEnd(',') + " where Id=@Id";
            param.Add(Dict.T_VoteView_Id(Common.IsInt(model.Id)));
            return SqlHelper.ExecuteNonQuery(SqlHelper.connstr, CommandType.Text, sql, param.ToArray());
        }
        public int Insert(T_VoteLike model)
        {
            string sql = "Insert into " + Config.DatabasePrefix("T_VoteLike") + "([VoteId],[ViewId],[UserId],[AddTime]) values (@VoteId,@ViewId,@UserId,@AddTime)"; 
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(Dict.T_VoteLike_VoteId(Common.IsInt(model.VoteId)));
            param.Add(Dict.T_VoteLike_ViewId(Common.IsInt(model.ViewId)));
            param.Add(Dict.T_VoteLike_UserId(Common.IsInt(model.UserId)));
            param.Add(Dict.T_VoteLike_AddTime(Common.IsDateTime(model.AddTime)));
            return SqlHelper.ExecuteNonQuery(SqlHelper.connstr, CommandType.Text, sql, param.ToArray());
        }
        public int InsertOne(T_VoteLike model)
        {
            string sql = "Insert into " + Config.DatabasePrefix("T_VoteLike") + "([VoteId],[ViewId],[UserId],[AddTime]) values (@VoteId,@ViewId,@UserId,@AddTime);select @@identity as Id;";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(Dict.T_VoteLike_VoteId(Common.IsInt(model.VoteId)));
            param.Add(Dict.T_VoteLike_ViewId(Common.IsInt(model.ViewId)));
            param.Add(Dict.T_VoteLike_UserId(Common.IsInt(model.UserId)));
            param.Add(Dict.T_VoteLike_AddTime(Common.IsDateTime(model.AddTime)));
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.connstr, CommandType.Text, sql, param.ToArray());
            int n = 0;
            while (reader.Read())
            {
                n = Convert.ToInt32(reader["Id"]);
            }
            reader.Close();
            return n;
        }
        public int Update(T_VoteLike model, string prohibit = "")
        {
            string sql = "Update " + Config.DatabasePrefix("T_VoteLike") + " set "; 
            List<SqlParameter> param = new List<SqlParameter>();
            if (prohibit.IndexOf("VoteId") == -1)
            {
                param.Add(Dict.T_VoteLike_VoteId(Common.IsInt(model.VoteId)));
                sql += "[VoteId]=@VoteId,";
            }
            if (prohibit.IndexOf("ViewId") == -1)
            {
                param.Add(Dict.T_VoteLike_ViewId(Common.IsInt(model.ViewId)));
                sql += "[ViewId]=@ViewId,";
            }
            if (prohibit.IndexOf("UserId") == -1)
            {
                param.Add(Dict.T_VoteLike_UserId(Common.IsInt(model.UserId)));
                sql += "[UserId]=@UserId,";
            }
            if (prohibit.IndexOf("AddTime") == -1)
            {
                param.Add(Dict.T_VoteLike_AddTime(Common.IsDateTime(model.AddTime)));
                sql += "[AddTime]=@AddTime,";
            }
            sql = sql.TrimEnd(',') + " where Id=@Id";
            param.Add(Dict.T_VoteLike_Id(Common.IsInt(model.Id)));
            return SqlHelper.ExecuteNonQuery(SqlHelper.connstr, CommandType.Text, sql, param.ToArray());
        }
    }
}
