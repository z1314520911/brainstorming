using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Transactions;
using System.Dynamic;
using System.IO;

using Commons;
using Model;
using BLL;
using My.Engine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Web.Areas.WebHome
{
    public class IdeaController : XcxControllerBase
    {

        #region 记录

        public void RecordList()
        {
            T_User user = base.GetUser();
            if (user.Id > 0)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();

                bool next = true; //是否有下一页
                DataTable dt = new DataTable();
                PagerModel pageModel = new PagerModel();
                pageModel.PageNo = Convert.ToString(Request.QueryString["page"]);
                pageModel.CurrentRow = 10;
                int TotalRow = 0;

                pageModel.SelectWhere = "UserId=" + user.Id + " or Id in(select VoteId from " + Config.DatabasePrefix("T_VoteUser") + " where UserId=" + user.Id + ")";

                dt = ModelBLL.GetAll(pageModel.CurrentPage, pageModel.CurrentRow, "Id,Name,Room,AddTime", "T_Vote", pageModel.SelectWhere, "Id desc", ref TotalRow);

                next = true;
                if ((pageModel.TotalRow * 1.0f / pageModel.CurrentRow) <= pageModel.CurrentPage)
                {
                    next = false;
                }


                dict.Add("dataList", dt);
                dict.Add("next", next);

                ReJson(dict, 2);
            }
        }

        #endregion


        public void SaveTheme()
        {
            T_User user = GetUser();
            if (user.Id > 0)
            {
                Result result = new Result();
                try
                {
                    T_Vote model = new T_Vote();

                    model.UserId = user.Id;

                    model.Id = RequestEx("id", 0);

                    if (model.Id == 0)
                    {
                        model.Name = RequestEx("Name");
                        model.Room = RequestEx("Room");
                        if (string.IsNullOrEmpty(model.Name))
                        {
                            result.Message = "请输入问题!";
                        }
                        else if (string.IsNullOrEmpty(model.Room))
                        {
                            result.Message = "请输入房间号!";
                        }
                        else if (model.Room.Length != 4)
                        {
                            result.Message = "请输入四位房间号!";
                        }
                        else
                        {
                            model.AddTime = DateTime.Now;
                            model.Id = ModelBLL.InsertOne(model);
                        }
                    }
                    else
                    {
                        //ModelBLL.Update(model);
                    }
                    result.Code = model.Id;
                    result.Detail = JsonConvert.SerializeObject(model);

                    ReJson(result);
                }
                catch (Exception ex)
                {
                    ReJson(new Result(-1, ex.Message));
                }
            }
        }

        //public void DeleteAddress()
        //{
        //    int id = RequestEx("id", 0);
        //    int count = ModelBLL.Delete("T_Address", id)
        //}


        public void GetTheme()
        {
            T_User user = base.GetUser();
            if (user.Id > 0)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dynamic dUser = new ExpandoObject();
                int id = RequestEx("id", 0);

                T_Vote vote = ModelBLL.QueryByID<T_Vote>(id);

                dict.Add("data", vote);

                T_User voteUser = new T_User();
                DataTable dtVoteUser = ModelBLL.getPage(0, "a.Id,a.NickName,a.Avatar", "T_VoteUser", " z left join " + Config.DatabasePrefix("T_User") + " a on a.Id=z.UserId", " z.VoteId=" + vote.Id, "");

                bool IsJoin = false;

                if (user.Id == vote.UserId)
                {
                    voteUser = user;
                }
                else
                {
                    voteUser = ModelBLL.QueryByID<T_User>(vote.UserId);
                }
                DataRow[] rowVoteUser = dtVoteUser.Select("Id=" + user.Id);
                IsJoin = rowVoteUser.Length > 0 || vote.UserId == user.Id ? true : false;

                dict.Add("NickName", voteUser.NickName);

                dict.Add("IsOwn", voteUser.Id > 0 && user.Id > 0 && voteUser.Id == user.Id ? true : false);
                dict.Add("IsJoin", IsJoin);
                dict.Add("IsFinish", vote.Status > 0 ? true : false);

                //DataTable dtVoteLike = new DataTable();
                //if (vote.Status > 0)
                //{
                DataTable dtVoteLike = ModelBLL.getPage(0, "Id,ViewId,UserId", "T_VoteLike", "VoteId=" + vote.Id, "");
                //}

                List<dynamic> list = new List<dynamic>();
                foreach (DataRow dr in dtVoteUser.Rows)
                {
                    dynamic dyn = new ExpandoObject();
                    dyn.Id = dr["Id"];
                    dyn.NickName = dr["NickName"];
                    dyn.Avatar = dr["Avatar"];
                    dyn.Count = 0;
                    if (vote.IsStart)
                    {
                        DataTable dtVoteView = ModelBLL.getPage(3, "Id,Idea,0 as IsLike,0 as Num", "T_VoteView", "VoteId=" + vote.Id + " and UserId=" + dr["Id"], "");

                        if (dtVoteView.Rows.Count < 3)
                        {
                            int count = 3 - dtVoteView.Rows.Count;
                            for (int i = 0; i < count; i++)
                            {
                                DataRow row = dtVoteView.NewRow();
                                row["Id"] = 0;
                                row["Idea"] = "-";
                                row["IsLike"] = 0;
                                row["Num"] = 0;
                                dtVoteView.Rows.Add(row);
                            }
                        }
                        //if (vote.Status > 0)
                        //{
                        foreach (DataRow row in dtVoteView.Rows)
                        {
                            if (Common.IsInt(row["Id"]) > 0)
                            {
                                DataRow[] r1 = dtVoteLike.Select("ViewId=" + row["Id"]);
                                DataRow[] r2 = dtVoteLike.Select("ViewId=" + row["Id"] + " and UserId=" + user.Id);
                                row["Num"] = r1.Length;
                                row["IsLike"] = r2.Length;
                                dyn.Count += r1.Length;
                            }
                        }
                        //}

                        dyn.Item = dtVoteView;
                    }
                    list.Add(dyn);
                }
                dict.Add("ItemList", list);

                ReJson(dict, 2);
            }
        }


        public void JoinTheme()
        {
            Result result = new Result();
            result.Code = -1;
            T_User user = GetUser();
            if (user.Id > 0)
            {
                string room = RequestEx("room");

                T_Vote vote = ModelBLL.QueryBySelectWhereAndOrderBy<T_Vote>(Common.IsEqual("Room") + " and Status<2", "Id desc", Dict.T_Vote_Room(room));

                //if (vote.UserId != user.Id)
                if (vote.Id > 0)
                {
                    bool isJoin = ModelBLL.CheckInFo("T_VoteUser", "VoteId=" + vote.Id + " and UserId=" + user.Id);
                    if (!isJoin)
                    {
                        T_VoteUser voteUser = new T_VoteUser();
                        voteUser.VoteId = vote.Id;
                        voteUser.UserId = user.Id;
                        voteUser.TypeId = 0;
                        int n = ModelBLL.Insert<T_VoteUser>(voteUser);
                    }
                }
                result.Code = vote.Id;

            }
            ReJson(result);
        }

        public void StartTheme()
        {
            Result result = new Result();
            result.Code = -1;
            T_User user = GetUser();
            if (user.Id > 0)
            {
                int id = RequestEx("id", 0);

                T_Vote vote = ModelBLL.QueryByID<T_Vote>(id);

                if (vote.Id > 0 && user.Id == vote.UserId && vote.IsStart == false)
                {
                    int n = ModelBLL.UpdateInfo("T_Vote", vote.Id, "IsStart=1");
                    result.Code = 1;
                }

            }
            ReJson(result);
        }

        public void FinishTheme()
        {
            Result result = new Result();
            result.Code = -1;
            T_User user = GetUser();
            if (user.Id > 0)
            {
                int id = RequestEx("id", 0);

                T_Vote vote = ModelBLL.QueryByID<T_Vote>(id);

                if (vote.Id > 0 && user.Id == vote.UserId && vote.IsStart == true && vote.Status == 0)
                {
                    int n = ModelBLL.UpdateInfo("T_Vote", vote.Id, "Status=1");
                    result.Code = 1;
                }

            }
            ReJson(result);
        }

        public void LastTheme()
        {
            Result result = new Result();
            result.Code = -1;
            T_User user = GetUser();
            if (user.Id > 0)
            {
                int id = RequestEx("id", 0);

                T_Vote vote = ModelBLL.QueryByID<T_Vote>(id);

                if (vote.Id > 0 && user.Id == vote.UserId && vote.IsStart == true && vote.Status == 1)
                {
                    int n = ModelBLL.UpdateInfo("T_Vote", vote.Id, "Status=2");
                    result.Code = 1;
                }

            }
            ReJson(result);
        }

        public void GetIdeaTheme()
        {
            T_User user = GetUser();
            if (user.Id > 0)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();

                int id = RequestEx("id", 0);
                if (id > 0)
                {
                    T_Vote vote = ModelBLL.QueryByID<T_Vote>(id);

                    dict.Add("Name", vote.Name);

                    T_VoteUser voteUser = ModelBLL.QueryBySelectWhere<T_VoteUser>("VoteId=" + vote.Id + " and UserId=" + user.Id);
                    if (voteUser.Id > 0)
                    {
                        DataTable dtVoteView = ModelBLL.getPage(3, "Idea", "T_VoteView", "VoteId=" + vote.Id + " and VoteUserId=" + voteUser.Id, "");

                        for (int i = 0; i < dtVoteView.Rows.Count; i++)
                        {
                            dict.Add("Idea" + i, dtVoteView.Rows[i]["Idea"]);
                        }
                    }
                }
                ReJson(dict, 2);
            }
        }


        public void SaveIdeaTheme()
        {
            T_User user = GetUser();
            if (user.Id > 0)
            {
                Result result = new Result();
                try
                {
                    int id = RequestEx("id", 0);
                    T_Vote vote = ModelBLL.QueryByID<T_Vote>(id);

                    T_VoteUser voteUser = ModelBLL.QueryBySelectWhere<T_VoteUser>("VoteId=" + vote.Id + " and UserId=" + user.Id);
                    if (voteUser.Id == 0 && vote.UserId == user.Id)
                    {
                        voteUser.VoteId = vote.Id;
                        voteUser.UserId = user.Id;
                        voteUser.TypeId = 0;
                        voteUser.Id = ModelBLL.Insert<T_VoteUser>(voteUser);
                    }
                    if (voteUser.Id > 0)
                    {
                        DataTable dtVoteView = ModelBLL.getPage(3, "Id", "T_VoteView", "VoteId=" + vote.Id + " and UserId=" + user.Id, "");
                        if (dtVoteView.Rows.Count < 3)
                        {
                            int count = 3 - dtVoteView.Rows.Count;
                            for (int i = 0; i < count; i++)
                            {
                                DataRow row = dtVoteView.NewRow();
                                row["Id"] = 0;
                                dtVoteView.Rows.Add(row);
                            }
                        }
                        for (int i = 0; i < dtVoteView.Rows.Count; i++)
                        {
                            int idVoteView = Common.IsInt(dtVoteView.Rows[i]["Id"]);
                            if (idVoteView > 0)
                            {
                                int n = ModelBLL.UpdateInfo("T_VoteView", idVoteView, Common.IsEqual("Idea"), Dict.T_VoteView_Idea(RequestEx("Idea" + i)));
                            }
                            else
                            {
                                T_VoteView voteView = new T_VoteView();
                                voteView.VoteId = vote.Id;
                                voteView.UserId = user.Id;
                                voteView.VoteUserId = voteUser.Id;
                                voteView.Idea = RequestEx("Idea" + i);
                                int n = ModelBLL.Insert<T_VoteView>(voteView);
                            }
                        }
                    }
                    result.Code = 1;

                    ReJson(result);
                }
                catch (Exception ex)
                {
                    ReJson(new Result(-1, ex.Message));
                }
            }
        }


        public void LikeIdeaTheme()
        {

            Result result = new Result();
            result.Code = -1;
            T_User user = GetUser();
            if (user.Id > 0)
            {
                int id = RequestEx("id", 0);

                T_VoteView voteView = ModelBLL.QueryByID<T_VoteView>(id);
                T_Vote vote = ModelBLL.QueryByID<T_Vote>(voteView.VoteId);

                if (vote.Id > 0) //&& vote.Status == 1
                {
                    T_VoteUser voteUser = ModelBLL.QueryBySelectWhere<T_VoteUser>("VoteId=" + vote.Id + " and UserId=" + user.Id);
                    if (voteUser.Id > 0 || vote.UserId == user.Id)
                    {
                        DataTable dtVoteLike = ModelBLL.getPage(0, "Id,ViewId", "T_VoteLike", "VoteId=" + vote.Id + "  and UserId=" + user.Id, "");

                        DataRow[] row = dtVoteLike.Select("ViewId=" + voteView.Id);
                        if (row.Length > 0)
                        {
                            int n = ModelBLL.Delete("T_VoteLike", Common.IsInt(row[0]["Id"]));
                        }
                        else
                        {
                            DataTable dtVoteUser = ModelBLL.getPage(0, "UserId", "T_VoteUser", "VoteId=" + vote.Id, "");

                            int count = dtVoteUser.Rows.Count;
                            DataRow[] row2 = dtVoteUser.Select("UserId=" + vote.UserId);
                            if (row2.Length == 0)
                            {
                                count += 1;
                            }

                            if (dtVoteLike.Rows.Count < count)
                            {
                                T_VoteLike voteLike = new T_VoteLike();
                                voteLike.VoteId = vote.Id;
                                voteLike.ViewId = voteView.Id;
                                voteLike.UserId = user.Id;
                                int n = ModelBLL.Insert<T_VoteLike>(voteLike);
                            }
                        }
                        result.Code = 1;
                    }
                }

                ReJson(result);
            }
        }

        //public void GetTheme()
        //{
        //    T_User user = GetUser();
        //    if (user.Id > 0)
        //    {
        //        int id = RequestEx("id", 0);
        //        if (id > 0)
        //        {
        //            T_Vote model = ModelBLL.QueryByID<T_Vote>(id);

        //            ReJson(model);
        //        }
        //    }
        //}
    }
}
