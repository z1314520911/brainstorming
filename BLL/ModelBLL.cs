using DAL;
using Model;

namespace BLL
{
    public partial class ModelBLL
    {
        private static ModelService_Sql cs = new ModelService_Sql();
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Insert<T>(T model)
        {
             System.Type type = model.GetType();
             int count = 0;
             switch (type.Name){
                case "AdminUser": count = cs.Insert(model as AdminUser); break;
                case "T_User": count = cs.Insert(model as T_User); break;
                case "T_Vote": count = cs.Insert(model as T_Vote); break;
                case "T_VoteUser": count = cs.Insert(model as T_VoteUser); break;
                case "T_VoteView": count = cs.Insert(model as T_VoteView); break;
                case "T_VoteLike": count = cs.Insert(model as T_VoteLike); break;
                 default: count = -1; break;
             }
         return count;
        }
        /// <summary>
        /// 插入数据,返回ID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int InsertOne<T>(T model)
        {
             System.Type type = model.GetType();
             int count = 0;
             switch (type.Name){
                case "AdminUser": count = cs.InsertOne(model as AdminUser); break;
                case "T_User": count = cs.InsertOne(model as T_User); break;
                case "T_Vote": count = cs.InsertOne(model as T_Vote); break;
                case "T_VoteUser": count = cs.InsertOne(model as T_VoteUser); break;
                case "T_VoteView": count = cs.InsertOne(model as T_VoteView); break;
                case "T_VoteLike": count = cs.InsertOne(model as T_VoteLike); break;
                 default: count = -1; break;
             }
         return count;
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Update<T>(T model)
        {
             System.Type type = model.GetType();
             int count = 0;
             switch (type.Name){
                case "AdminUser": count = cs.Update(model as AdminUser); break;
                case "T_User": count = cs.Update(model as T_User); break;
                case "T_Vote": count = cs.Update(model as T_Vote); break;
                case "T_VoteUser": count = cs.Update(model as T_VoteUser); break;
                case "T_VoteView": count = cs.Update(model as T_VoteView); break;
                case "T_VoteLike": count = cs.Update(model as T_VoteLike); break;
                 default: count = -1; break;
             }
         return count;
        }
    }
}
